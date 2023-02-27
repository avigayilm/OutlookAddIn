using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace OutlookAddIn
{
    /// <summary>
    /// client that communicates with the applethost(server)
    /// </summary>
    class Encrypt
    {
        //Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


        public enum CHOICE { SIGN_ENCRYPT, VERIFY_DECRYPT, SIGN, VERIFY }

        #region initializing
        string seperator = "#*#*#*#*";

        public Encrypt()
        {
            //// Connect to the server.
            //try
            //{
            //    client.Connect(new IPEndPoint(IPAddress.Loopback, 8080));

            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.ToString());
            //}
        }

        #endregion

        #region send email
        public string EncrypteMsgAndSign(string body, string receiverEmail)
        {
            string input = ((int)CHOICE.SIGN_ENCRYPT).ToString();
            //send to the socket this:
            string data = input + seperator + receiverEmail + seperator + body;
            return "@#@"+send_msg_socket(data);

        }
        public string SignMsg(string body)
        {
            string input = ((int)CHOICE.SIGN).ToString();
            //send to the socket the following
            string data = (input + seperator + " " + seperator + body);
            return send_msg_socket(data);

        }
        #endregion

        #region verify
        /// <summary>
        /// decrypts and verifies the email by sending it to the function of verify
        /// if the signature is valid the decrypted message will show in the ReceiveForm
        /// </summary>
        /// <param name="body"></param>
        /// <param name="sender_email"></param>
        public void DecryptAndVerify(string body, string sender_email)
        {
            if (VerifySignature(body, false).Item1)
            {
                ReceiveForm receive = new ReceiveForm(sender_email, "decrypted message", VerifySignature(body, false).Item2);
                //    //Display the ReceiveForm dialog box
                receive.ShowDialog();

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="verify"></param>
        /// <returns></returns>
        public Tuple<bool, string> VerifySignature(string body, bool verify)
        {
            if (verify == false)// verify+decrypt
            {
                string input = ((int)CHOICE.VERIFY_DECRYPT).ToString()+seperator + body;
                string rslt = send_msg_socket(input);
                if (rslt == "false")
                {
                    MessageBox.Show("Your signature is not validated");
                    return Tuple.Create(false, "");
                }
                else
                {
                    MessageBox.Show("Your signature is validated");
                    return Tuple.Create(true, ""); ;
                }

            }
            else
            {
                string input = ((int)CHOICE.VERIFY).ToString() + body;
                //if returned value;
                string rslt = send_msg_socket(input);
                if (rslt == "false")
                {
                    MessageBox.Show("Your signature is not validated");
                    return null;
                }
                else
                {
                    MessageBox.Show("Your signature is validated");
                    return null;
                }
            }
        }
        #endregion

        #region connect to socket
        string send_msg_socket(string input)
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



            // Send a message to the server.
            byte[] message = Encoding.ASCII.GetBytes(input);
            //byte[] message= Encoding.UTF8.GetBytes(input);
            client.Send(message);
            Console.WriteLine("send a msg");
            // Receive the response from the server.
            byte[] buffer = new byte[1024];
            int bytesReceived = client.Receive(buffer);
            Console.WriteLine("receive a msg");
            //string response = Convert.ToBase64String(buffer);
            //string response = BitConverter.ToString(buffer);
            string response = Encoding.ASCII.GetString(buffer, 0, bytesReceived);
            byte[] check= Encoding.ASCII.GetBytes(response);
            byte[] check1 = Encoding.ASCII.GetBytes("1" + response);
            //Console.WriteLine("Received message: {0}", response);


            // Close the socket.
            client.Shutdown(SocketShutdown.Both);
            client.Close();




            return response;

           

        }
        #endregion

        /// <summary>
        /// destructor to disconnect the client
        /// </summary>
        ~Encrypt()
        {
            //// Close the socket.
            //client.Shutdown(SocketShutdown.Both);
            //client.Close();
        }

    }
}