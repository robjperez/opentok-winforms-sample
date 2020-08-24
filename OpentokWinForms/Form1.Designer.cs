using OpenTok;

namespace OpentokWinForms
{
    partial class Form1
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
            this.wpfPublisherHost = new System.Windows.Forms.Integration.ElementHost();
            this.Connect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.wpfSubscriberHost = new System.Windows.Forms.Integration.ElementHost();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // wpfPublisherHost
            // 
            this.wpfPublisherHost.Location = new System.Drawing.Point(12, 12);
            this.wpfPublisherHost.Name = "wpfPublisherHost";
            this.wpfPublisherHost.Size = new System.Drawing.Size(427, 300);
            this.wpfPublisherHost.TabIndex = 0;
            this.wpfPublisherHost.Text = "wpfPublisherHost";
            this.wpfPublisherHost.Child = null;
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(355, 390);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(165, 30);
            this.Connect.TabIndex = 1;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(196, 315);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Publisher";
            // 
            // wpfSubscriberHost
            // 
            this.wpfSubscriberHost.Location = new System.Drawing.Point(445, 12);
            this.wpfSubscriberHost.Name = "wpfSubscriberHost";
            this.wpfSubscriberHost.Size = new System.Drawing.Size(427, 300);
            this.wpfSubscriberHost.TabIndex = 3;
            this.wpfSubscriberHost.Text = "wpfSubscriberHost";
            this.wpfSubscriberHost.Child = null;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(624, 315);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Subscriber";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 434);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.wpfSubscriberHost);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.wpfPublisherHost);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost wpfPublisherHost;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Integration.ElementHost wpfSubscriberHost;
        private System.Windows.Forms.Label label2;
    }
}

