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
    public partial class ReceiveForm : Form
    {
        private string sender_email;
        private string content;
        private string subject;

        public ReceiveForm()
        {
            InitializeComponent();
        }

        public ReceiveForm(string sender_email, string subject, string content)
        {
            this.sender_email = sender_email;
            this.subject = subject;
            this.content = content;
            InitializeComponent();
            this.label4.Text = sender_email;
            this.label2.Text = subject;
            this.BodyContent.Text = content;
            // this.BodyContent.AutoSize = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReceiveForm_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void BodyContent_TextChanged(object sender, EventArgs e)
        {

        }
    }
}