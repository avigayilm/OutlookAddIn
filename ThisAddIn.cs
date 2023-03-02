using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;

namespace OutlookAddIn
{
    /// <summary>
    /// code for a Microsoft Outlook add-in 
    /// that adds a custom task pane to the Outlook inspector window 
    /// when a new email message is opened
    /// </summary>
    public class InspectorWrapper
    {
      


        //used to manage the custom task pane and handle events related to the inspector window
        private Outlook.Inspector inspector;
        private CustomTaskPane taskPane;
        /// <summary>
        /// this constructor initializes the InspectorWrapper object with the Inspector object passed as an argument and creates a custom task pane
        /// associated with the inspector. It also registers event handlers for the inspector and task pane events.
        /// </summary>
        /// <param name="Inspector"></param>
        public InspectorWrapper(Outlook.Inspector Inspector)
        {
            inspector = Inspector;
            ((Outlook.InspectorEvents_Event)inspector).Close +=
                new Outlook.InspectorEvents_CloseEventHandler(InspectorWrapper_Close);

            taskPane = Globals.ThisAddIn.CustomTaskPanes.Add(
                new TaskPaneControl(), "My task pane", inspector);
            taskPane.VisibleChanged += new EventHandler(TaskPane_VisibleChanged);
        }


        void TaskPane_VisibleChanged(object sender, EventArgs e)
        {
            Globals.Ribbons[inspector].Ribbon1.toggleButton1.Checked =
                taskPane.Visible;
        }

        /// <summary>
        ///  removes the task pane from the custom task panes collection and performs cleanup when the inspector window is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InspectorWrapper_Close()
        {
            if (taskPane != null)
            {
                Globals.ThisAddIn.CustomTaskPanes.Remove(taskPane);
            }

            taskPane = null;
            Globals.ThisAddIn.InspectorWrappers.Remove(inspector);
            ((Outlook.InspectorEvents_Event)inspector).Close -=
                new Outlook.InspectorEvents_CloseEventHandler(InspectorWrapper_Close);
            inspector = null;
        }

        public CustomTaskPane CustomTaskPane
        {
            get
            {
                return taskPane;
            }
        }
    }

    /// <summary>
    /// The main class of this outlook project
    /// </summary>
    public partial class ThisAddIn
    {
        //dictiornary to keep all the emails that need to be encrypted till he gets the public keys
        public static Dictionary<string, List<string>> emailsToSend = new Dictionary<string, List<string>>();

        private Dictionary<Outlook.Inspector, InspectorWrapper> inspectorWrappersValue =
         new Dictionary<Outlook.Inspector, InspectorWrapper>();
        private Outlook.Inspectors inspectors;
        Encrypt Instance = new Encrypt();


        public Dictionary<Outlook.Inspector, InspectorWrapper> InspectorWrappers
        {
            get
            {
                return inspectorWrappersValue;
            }
        }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            Application.ItemSend += new Outlook.ApplicationEvents_11_ItemSendEventHandler(Application_ItemSend);

           



            var inspectors = this.Application.Inspectors;
            inspectors.NewInspector +=
                new Outlook.InspectorsEvents_NewInspectorEventHandler(Inspectors_NewInspector);

            foreach (Outlook.Inspector inspector in inspectors)
            {
                Inspectors_NewInspector(inspector);
            }
        }




        // if I get an email where the subject is Exchange I want to send my public key as a result.
        // and store the senders pK in the dictionary
        public void keyExchangeReceive(string senderEmail, string myEmail, byte[] senderpK)
        {
            Outlook.MailItem mailItem = Globals.ThisAddIn.Application.CreateItem(Outlook.OlItemType.olMailItem);
            mailItem.Subject = "KEYEXCHANGE";
            mailItem.To = senderEmail;
            byte[] myPk = Instance.KeyExchange(senderEmail, senderpK,myEmail);
            string pkString = Convert.ToBase64String(myPk);
            // now I want to send back to the sender my public key
            keyExchangeSend(pkString, senderEmail);
            if(emailsToSend.ContainsKey(senderEmail)) // see if I have email that haven't been send yet because I didn't have the public key
            {
                foreach (var body in emailsToSend[senderEmail])
                {
                    

                    Outlook.MailItem mailItemtosend = Globals.ThisAddIn.Application.CreateItem(Outlook.OlItemType.olMailItem);
                    mailItemtosend.Subject = "This message has been encrypted";
                    Tuple<bool, string> encrypted_msg = Instance.EncrypteMsgAndSign_byte(body, senderEmail);
                    if (encrypted_msg.Item1 == true)
                    {
                        mailItemtosend.Body = encrypted_msg.Item2;
                        mailItemtosend.To = senderEmail;
                        //remove the eventhandler so it won't go to the funciton of the sending the email
                        Application.ItemSend -= new Outlook.ApplicationEvents_11_ItemSendEventHandler(Application_ItemSend);
                        mailItem.Send();

                        //return the event handler for the next email that is being sent
                        Application.ItemSend += new Outlook.ApplicationEvents_11_ItemSendEventHandler(Application_ItemSend);
                    }
                    return;
                }
            }

            // Send the mail item without displaying the outlook send dialog
            //mailItem.Send();
        }
        /// <summary>
        /// When teh user send an email, the user can choose, to encrypt or sign the email
        /// the email will then be sent accordingly.
        /// </summary>
        /// <param name="Item= the mail"></param>
        /// <param name="Cancel= type of sending"></param>
        void Application_ItemSend(object Item, ref bool Cancel)
        {
            Outlook.MailItem mailItem = Item as Outlook.MailItem;
            var body = mailItem.Body;
            var subject = mailItem.Subject;
            var senderEmail = mailItem.SenderEmailAddress;
            var receiverEmail=mailItem.Recipients[1].Address;

            SendForm form = new SendForm(receiverEmail, subject, body, "The message is ready for delivery.\nfor sending please press send:", "");
            form.ShowDialog();

            // 0 is for canceling the sending
            if (SendForm.Send() == 0)
                Cancel = true;
            // 1 is for send and sign
            else if (SendForm.Send() == 1)
            {
                mailItem.Subject = "Message has been signed." + subject;
                string signed_msg = Instance.SignMsg(body);
                mailItem.Body = body + "signature"  + signed_msg;
                bool isValid = Instance.Verify_byte(body, signed_msg, senderEmail);
                Cancel = false;
            }
            // for send, encrypt and sign
            else
            {
                Tuple<bool,string> encrypted_msg= Instance.EncrypteMsgAndSign_byte(body, receiverEmail);
                //for the email body:
                if (encrypted_msg.Item1 ==false )
                {
                    MessageBox.Show("The person you are trying to send a message to is not part of our platform. The encrypted message will be send once the keyexchange has happened");
                    // the key exchange will send the public key to the receiver
                    keyExchangeSend(encrypted_msg.Item2,receiverEmail);
                    if (emailsToSend.ContainsKey(senderEmail))
                        emailsToSend[senderEmail].Add(body);
                    else
                        emailsToSend.Add(senderEmail, new List<string>() { body });
                   Cancel = true;
                    // mailItem.Subject = subject;
                    // mailItem.Body = body;
                }
                else
                {
                    mailItem.Subject = "Message has been encrypted and signed." + subject;
                    mailItem.Body = "Encrypted" + encrypted_msg.Item2;
                    Cancel = false;
                }
                
            }
        }

        //send the public key as an email
        void keyExchangeSend(string pk,string receiverEmail)
        {
            Outlook.MailItem mailItem = Globals.ThisAddIn.Application.CreateItem(Outlook.OlItemType.olMailItem);
            mailItem.Subject = "KEYEXCHANGE";
            mailItem.Body = pk;
            mailItem.To = receiverEmail;

            // Send the mail item without displaying the outlook send dialog

            //remove the eventhandler so it won't go to the funciton of the sending the email
            Application.ItemSend -= new Outlook.ApplicationEvents_11_ItemSendEventHandler(Application_ItemSend);
            mailItem.Send();

            //return the event handler for the next email that is being sent
            Application.ItemSend += new Outlook.ApplicationEvents_11_ItemSendEventHandler(Application_ItemSend);
        }



        /// <summary>
        /// for add-in when opening an email
        /// </summary>
        /// <param name="Inspector"></param>
        void Inspectors_NewInspector(Outlook.Inspector Inspector)
        {
            if (Inspector.CurrentItem is Outlook.MailItem)
            {
                inspectorWrappersValue.Add(Inspector, new InspectorWrapper(Inspector));

                string subject = Inspector.CurrentItem.Subject;
                if (subject == "KEYEXCHANGE")
                {
                    string senderEmail = Inspector.CurrentItem.SenderEmailAddress;// email adress that was being send
                    string myEmail = Inspector.CurrentItem.To; // my emailadress
                    string senderPk = Inspector.CurrentItem.Body;
                    byte[] bytes;

                    try
                    {
                        byte[] senderPkByte = Convert.FromBase64String(senderPk);
                        keyExchangeReceive(senderEmail, myEmail, senderPkByte);
                        MessageBox.Show("Your key is being exchanged");
                        // the conversion was successful
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Not a valid Public Key, so Can't exchange keys");
                    }

                }
                else
                    return;

            }

           
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {

            inspectors.NewInspector -=
    new Outlook.InspectorsEvents_NewInspectorEventHandler(
    Inspectors_NewInspector);
            inspectors = null;
            inspectorWrappersValue = null;


            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}