namespace OutlookAddIn
{
    partial class SendForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.EmailSendLable = new System.Windows.Forms.Label();
            this.EmailContentLable = new System.Windows.Forms.Label();
            this.subgectLable = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BodyEncrypteLable = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Location = new System.Drawing.Point(586, 409);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(210, 57);
            this.button1.TabIndex = 0;
            this.button1.Text = "Send and Encrypt";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EmailSendLable
            // 
            this.EmailSendLable.AutoSize = true;
            this.EmailSendLable.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.EmailSendLable.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmailSendLable.Location = new System.Drawing.Point(150, 12);
            this.EmailSendLable.Name = "EmailSendLable";
            this.EmailSendLable.Size = new System.Drawing.Size(59, 20);
            this.EmailSendLable.TabIndex = 1;
            this.EmailSendLable.Text = "Email";
            // 
            // EmailContentLable
            // 
            this.EmailContentLable.AutoSize = true;
            this.EmailContentLable.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.EmailContentLable.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmailContentLable.Location = new System.Drawing.Point(15, 162);
            this.EmailContentLable.Name = "EmailContentLable";
            this.EmailContentLable.Size = new System.Drawing.Size(79, 20);
            this.EmailContentLable.TabIndex = 2;
            this.EmailContentLable.Text = "Message";
            // 
            // subgectLable
            // 
            this.subgectLable.AutoSize = true;
            this.subgectLable.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.subgectLable.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subgectLable.Location = new System.Drawing.Point(150, 50);
            this.subgectLable.Name = "subgectLable";
            this.subgectLable.Size = new System.Drawing.Size(79, 20);
            this.subgectLable.TabIndex = 3;
            this.subgectLable.Text = "Subject";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(15, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Message unencrypted:";
            // 
            // BodyEncrypteLable
            // 
            this.BodyEncrypteLable.AutoSize = true;
            this.BodyEncrypteLable.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.BodyEncrypteLable.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BodyEncrypteLable.Location = new System.Drawing.Point(30, 312);
            this.BodyEncrypteLable.Name = "BodyEncrypteLable";
            this.BodyEncrypteLable.Size = new System.Drawing.Size(49, 20);
            this.BodyEncrypteLable.TabIndex = 6;
            this.BodyEncrypteLable.Text = "Body";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "To:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 50);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Subject:";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button2.Location = new System.Drawing.Point(322, 409);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(210, 57);
            this.button2.TabIndex = 12;
            this.button2.Text = "Send and Sign";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(939, 479);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BodyEncrypteLable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.subgectLable);
            this.Controls.Add(this.EmailContentLable);
            this.Controls.Add(this.EmailSendLable);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SendForm";
            this.Text = "Sent Message";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label EmailSendLable;
        private System.Windows.Forms.Label EmailContentLable;
        private System.Windows.Forms.Label subgectLable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label BodyEncrypteLable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
    }
}