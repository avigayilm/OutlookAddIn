using System;

namespace OutlookAddIn
{
    partial class TaskPaneControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkSignBtn = new System.Windows.Forms.Button();
            this.verifyBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkSignBtn
            // 
            this.checkSignBtn.Location = new System.Drawing.Point(0, 21);
            this.checkSignBtn.Name = "checkSignBtn";
            this.checkSignBtn.Size = new System.Drawing.Size(165, 42);
            this.checkSignBtn.TabIndex = 0;
            this.checkSignBtn.Text = "Decrypt";
            this.checkSignBtn.UseVisualStyleBackColor = true;
            this.checkSignBtn.Click += new System.EventHandler(this.DecryptBtn_Click);
            // 
            // verifyBtn
            // 
            this.verifyBtn.Location = new System.Drawing.Point(2, 69);
            this.verifyBtn.Name = "verifyBtn";
            this.verifyBtn.Size = new System.Drawing.Size(163, 44);
            this.verifyBtn.TabIndex = 1;
            this.verifyBtn.Text = "Verify Signature";
            this.verifyBtn.UseVisualStyleBackColor = true;
            this.verifyBtn.Click += new System.EventHandler(this.VerifyBtn_Click);
            // 
            // TaskPaneControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.verifyBtn);
            this.Controls.Add(this.checkSignBtn);
            this.Name = "TaskPaneControl";
            this.Size = new System.Drawing.Size(168, 188);
            this.ResumeLayout(false);

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        #endregion

        private System.Windows.Forms.Button checkSignBtn;
        private System.Windows.Forms.Button verifyBtn;
    }
}