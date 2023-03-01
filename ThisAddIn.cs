using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools;
using System.Windows.Forms;



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

            SendForm form = new SendForm(senderEmail, subject, body, "The message is ready for delivery.\nfor sending please press send:", "");
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
                string encrypted_msg= Instance.EncrypteMsgAndSign_byte(body, receiverEmail);
                //for the email body:
                if (encrypted_msg == "false")
                {
                    MessageBox.Show("The person you are trying to send a message to is not part of our platform. Your email will be send without encryption.");
                    mailItem.Subject = subject;
                    mailItem.Body = body;
                }
                else
                {
                    mailItem.Subject = "Message has been encrypted and signed." + subject;
                    mailItem.Body = "Encrypted" + encrypted_msg;
                }
                Cancel = false;
            }
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