using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OutlookAddIn
{
    public partial class SendForm : Form
    {
        enum STATUS { Cancel, Send_sign, Send_encrypt };
        private static STATUS s_send = STATUS.Cancel;
        public SendForm(string i_email, string i_subject, string i_content, string i_encrypted, string i_signed)
        {
            InitializeComponent();
            s_send = STATUS.Cancel;
            this.EmailContentLable.Text = i_content;
            this.EmailSendLable.Text = i_email;
            this.subgectLable.Text = i_subject;
            this.BodyEncrypteLable.Text = i_encrypted;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            s_send = STATUS.Send_encrypt;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            s_send = STATUS.Send_sign;
            this.Close();
        }
        public static int Send()
        {
            return (int)s_send;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

    }
}
