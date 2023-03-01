using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace OutlookAddIn
{
    public partial class TaskPaneControl : UserControl
    {

        public TaskPaneControl()
        {
            InitializeComponent();

        }


        /// <summary>
        /// verified signature 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void VerifyBtn_Click(object sender, EventArgs e)
        {
            //Outlook.MailItem mailItem = Globals.ThisAddIn.Application.ActiveInspector().CurrentItem as Outlook.MailItem;
            ////string senderEmail = Globals.ThisAddIn.Application.ActiveInspector().CurrentItem.SenderEmailAddress;
            // Get the mail item
            Outlook.MailItem mailItem = Globals.ThisAddIn.Application.ActiveInspector().CurrentItem;

            // Get the first recipient's email address
            string receiverEmail = mailItem.Recipients[1].Address;

            Encrypt Instance = new Encrypt();

   
            string body = "";
            if (mailItem != null)
                body = mailItem.Body;
            string[] split_data = body.Split(new[] { "signature" }, StringSplitOptions.None);
            if (body.StartsWith("Encrypted"))  // this means that is is an encrypted message
            {
                string data = split_data[0].Substring(9);
                //Decrypt
                string decrypted_msg = Instance.Decrypt_byte(data);
                //Verify
                bool is_Valid = Instance.Verify_byte(decrypted_msg, split_data[1], receiverEmail);
                if (is_Valid)
                {
                    MessageBox.Show("The signature is valid");
                }
                else
                    MessageBox.Show("The signature isn't valid");
            }
            else
            {
                if(split_data.Length!=2)
                {
                    MessageBox.Show("This email is not part of our platform");
                    return;
                }
                string org_data = split_data[0].Substring(0, split_data[0].Length - 3); // // automatically a space and \r\n is added, we need to take off the space.
                org_data+="\r\n";
                bool is_Valid = Instance.Verify_byte(org_data, split_data[1], receiverEmail);//foe encrypt and verify
                if (is_Valid)
                {
                    MessageBox.Show("The signature is valid");
                }
                else
                    MessageBox.Show("The signature isn't valid");
            }
        }

        /// <summary>
        /// when pressing on the decrypt button a window form will pop up with the decrypted message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecryptBtn_Click(object sender, EventArgs e)
        {
            Encrypt Instance = new Encrypt();
            // Read the content of the email's attachments
            // Outlook.MailItem mailItem = Globals.ThisAddIn.Application.ActiveInspector().CurrentItem as Outlook.MailItem;
            // Get the mail item
            Outlook.MailItem mailItem = Globals.ThisAddIn.Application.ActiveInspector().CurrentItem;

            // Get the first recipient's email address
            string receiverEmail = mailItem.Recipients[1].Address;
            string body = "";
            if (mailItem != null)
                body = mailItem.Body;
            if (body.StartsWith("Encrypted"))  // this means that is is an encrypted message
            {
                string data = body.Substring(9);// takes the string without the Encrypted:
                //string senderEmail = Globals.ThisAddIn.Application.ActiveInspector().CurrentItem.SenderEmailAddress;
                Instance.DecryptAndVerify_byte(data, receiverEmail);
            }
            else
                 MessageBox.Show("The message is not encrypted");

        }
    }
}