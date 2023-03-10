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
    public partial class ReceiveForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BodyContent = new System.Windows.Forms.TextBox();
            this.BosyEmail = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Subject:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.label2.Location = new System.Drawing.Point(150, 49);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 28);
            this.label2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "From:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.label4.Location = new System.Drawing.Point(150, 12);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 28);
            this.label4.TabIndex = 6;
            // 
            // BodyContent
            // 
            this.BodyContent.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BodyContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BodyContent.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BodyContent.ForeColor = System.Drawing.SystemColors.MenuText;
            this.BodyContent.Location = new System.Drawing.Point(90, 106);
            this.BodyContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BodyContent.Multiline = true;
            this.BodyContent.Name = "BodyContent";
            this.BodyContent.ReadOnly = true;
            this.BodyContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.BodyContent.Size = new System.Drawing.Size(698, 194);
            this.BodyContent.TabIndex = 2;
            this.BodyContent.TextChanged += new System.EventHandler(this.BodyContent_TextChanged);
            // 
            // BosyEmail
            // 
            this.BosyEmail.AutoSize = true;
            this.BosyEmail.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.BosyEmail.Location = new System.Drawing.Point(15, 97);
            this.BosyEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.BosyEmail.Name = "BosyEmail";
            this.BosyEmail.Size = new System.Drawing.Size(0, 28);
            this.BosyEmail.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(350, 328);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 37);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun-ExtB", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(86, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "Decrypted message ";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // ReceiveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(872, 391);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BosyEmail);
            this.Controls.Add(this.BodyContent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ReceiveForm";
            this.Text = "Receive Message";
            this.Load += new System.EventHandler(this.ReceiveForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox BodyContent;
        private System.Windows.Forms.Label BosyEmail;
        private Button button1;
        private Label label5;
    }
}