using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace OutlookAddIn
{
    /// <summary>
    /// client that communicates with the applethost(server)
    /// </summary>
    class Encrypt
    {

        public enum CHOICE { ENCRYPT, DECRYPT, SIGN, GET_KEY,GEN_KEY }
        string seperator = "+/+/+/+/";
        public Encrypt() { }

        #region send email


        public string EncrypteMsgAndSign_byte(string body, string receiverEmail)
        {
            //SIGN
            string signature_str = SignMsg(body);
            //ENCRYPT
            string data = receiverEmail + seperator + body;
            string encryption_str = EncryptMsg(data);
            if (encryption_str == "false")
                return "false";
            return encryption_str + "signature" + signature_str;
        }

        public string EncryptMsg(string body)
        {

            byte[] message = Encoding.UTF8.GetBytes(body);
            byte byteToAdd = (byte)CHOICE.ENCRYPT;

            // the message with the action at the beginning
            byte[] newArray = new byte[] { byteToAdd }.Concat(message).ToArray();

            byte[] encryption = send_msg_socket_byte(newArray);
            string temp = Encoding.UTF8.GetString(encryption);
            if (temp == "false")
                return "false";
            string encryption_str = Convert.ToBase64String(encryption);
            return encryption_str;

        }

        public string SignMsg(string body)
        {

            byte[] message = Encoding.UTF8.GetBytes(body);
            byte byteToAdd = (byte)CHOICE.SIGN;
            // the message with the action at the beginning
            byte[] newArray = new byte[] { byteToAdd }.Concat(message).ToArray();
            byte[] signature = send_msg_socket_byte(newArray);
            string signature_str = Convert.ToBase64String(signature);
            return signature_str;

        }
        #endregion

        #region verify
        public void DecryptAndVerify_byte(string body, string receiver_email)
        {
            string[] split_data = body.Split(new[] { "signature" }, StringSplitOptions.None);
            //Decrypt
            string decrypted_msg = Decrypt_byte(split_data[0]);
            //Verify
            bool is_Valid = Verify_byte(decrypted_msg,split_data[1] , receiver_email);
            if (is_Valid)
            {
                ReceiveForm receive = new ReceiveForm(receiver_email, "decrypted message", decrypted_msg);
                //    //Display the ReceiveForm dialog box
                receive.ShowDialog();
            }
            else
                MessageBox.Show("This email can not be decrypted");
        }

        public bool Verify_byte(string body_encrypted,string signature ,string receiverEmail)
        {
            byte[] receiverEmail_UTF8 = System.Text.Encoding.UTF8.GetBytes(receiverEmail);
            //asking for the public key
            byte byteToAdd = (byte)CHOICE.GET_KEY;

            // the message with the action at the beginning
            byte[] newArray = new byte[] { byteToAdd }.Concat(receiverEmail_UTF8).ToArray();
            byte[] pk = send_msg_socket_byte(newArray);

            string publicKey = System.Text.Encoding.UTF8.GetString(pk);
            if (publicKey=="false")
            {
                return false;
            }
            byte[]signature_UTF8= Convert.FromBase64String(signature);
            byte[] body_UTF8 = Encoding.UTF8.GetBytes(body_encrypted);

            //for verifing the signature
            var mod_exp = GetModExp(pk);
            Org.BouncyCastle.Math.BigInteger mod = new Org.BouncyCastle.Math.BigInteger(1, mod_exp.Item1);
            Org.BouncyCastle.Math.BigInteger exp = new Org.BouncyCastle.Math.BigInteger(1, mod_exp.Item2);
            RsaKeyParameters param = new RsaKeyParameters(false, mod, exp);
            ISigner signClientSide = SignerUtilities.GetSigner(PkcsObjectIdentifiers.Sha256WithRsaEncryption.Id);
            signClientSide.Init(false, param);
            signClientSide.BlockUpdate(body_UTF8, 0, body_UTF8.Length);
            return signClientSide.VerifySignature(signature_UTF8);
        }

        /// <summary>
        /// Seperates the public key into mod and exp.
        /// </summary>
        /// <param name="pk"></param>
        /// <returns>mod, exp</returns>
        public Tuple<byte[], byte[]> GetModExp(byte[] pk)
        {
            byte[] modulus = new byte[256];
            Buffer.BlockCopy(pk, 0, modulus, 0, 256);
            byte[] exponent = new byte[4];
            Buffer.BlockCopy(pk, 256, exponent, 0, 4);
            return Tuple.Create(modulus, exponent);
        }

        public string Decrypt_byte(string body)
        {
            //check if the message is encrypted


            byte[] message = Convert.FromBase64String(body);
            
            // add the command to the beginning of the byte-array
            byte byteToAdd = (byte)CHOICE.DECRYPT;
            byte[] newArray = new byte[] { byteToAdd }.Concat(message).ToArray();

            byte[] rslt = send_msg_socket_byte(newArray);

            string decoded_msg = System.Text.Encoding.UTF8.GetString(rslt);
            return decoded_msg;
        }

        #endregion

        #region connect to socket

        byte[] send_msg_socket_byte(byte[] input)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Connect to the server.
            try
            {
                client.Connect(new IPEndPoint(IPAddress.Loopback, 8080));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


            client.Send(input);

            Console.WriteLine("send a msg");
            // Receive the response from the server.
            byte[] buffer = new byte[1024];
            int bytesReceived = client.Receive(buffer);
            byte[] receivedData = new byte[bytesReceived];
            Buffer.BlockCopy(buffer, 0, receivedData, 0, bytesReceived);
            Console.WriteLine("receive a msg");
            string response = Convert.ToBase64String(buffer);

            // Close the socket.
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            return receivedData;



        }

        #endregion


    }
}