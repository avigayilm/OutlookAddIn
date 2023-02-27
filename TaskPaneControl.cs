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
      //  Encrypt Instance = new Encrypt();


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
            Encrypt Instance = new Encrypt();

            Outlook.MailItem mailItem = Globals.ThisAddIn.Application.ActiveInspector().CurrentItem as Outlook.MailItem;
            string senderEmail = Globals.ThisAddIn.Application.ActiveInspector().CurrentItem.SenderEmailAddress;
            string body = "";
            if (mailItem != null)
                body = mailItem.Body;
            if (body.StartsWith("@#@"))  // this means that is is an encrypted message
            {
                string data = body.Substring(3);// takes the string without the @#@
                Instance.VerifySignature(data, false);//foe encrypt and verify
            }
            else
                Instance.VerifySignature(body, true);//foe encrypt and verify
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
            Outlook.MailItem mailItem = Globals.ThisAddIn.Application.ActiveInspector().CurrentItem as Outlook.MailItem;
            string body = "";
            if (mailItem != null)
                body = mailItem.Body;
            if (body.StartsWith("@#@"))  // this means that is is an encrypted message
            {
                string data = body.Substring(3);// takes the string without the @#@
                string senderEmail = Globals.ThisAddIn.Application.ActiveInspector().CurrentItem.SenderEmailAddress;
                Instance.DecryptAndVerify(data, senderEmail);

            }
            else
                MessageBox.Show("Your email is not encrypted");

        }
    }
}