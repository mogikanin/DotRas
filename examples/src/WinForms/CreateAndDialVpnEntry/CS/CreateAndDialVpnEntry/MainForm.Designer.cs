namespace DotRas.Samples.CreateAndDialVpnEntry
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.AllUsersPhoneBook = new DotRas.RasPhoneBook(this.components);
            this.CreateEntryButton = new System.Windows.Forms.Button();
            this.DialButton = new System.Windows.Forms.Button();
            this.Dialer = new DotRas.RasDialer(this.components);
            this.StatusTextBox = new System.Windows.Forms.TextBox();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CreateEntryButton
            // 
            this.CreateEntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateEntryButton.Location = new System.Drawing.Point(258, 294);
            this.CreateEntryButton.Name = "CreateEntryButton";
            this.CreateEntryButton.Size = new System.Drawing.Size(75, 23);
            this.CreateEntryButton.TabIndex = 0;
            this.CreateEntryButton.Text = "&Create Entry";
            this.CreateEntryButton.UseVisualStyleBackColor = true;
            this.CreateEntryButton.Click += new System.EventHandler(this.CreateEntryButton_Click);
            // 
            // DialButton
            // 
            this.DialButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DialButton.Location = new System.Drawing.Point(177, 294);
            this.DialButton.Name = "DialButton";
            this.DialButton.Size = new System.Drawing.Size(75, 23);
            this.DialButton.TabIndex = 1;
            this.DialButton.Text = "&Dial";
            this.DialButton.UseVisualStyleBackColor = true;
            this.DialButton.Click += new System.EventHandler(this.DialButton_Click);
            // 
            // Dialer
            // 
            this.Dialer.Credentials = null;
            this.Dialer.EapOptions = new DotRas.RasEapOptions(false, false, false);
            this.Dialer.HangUpPollingInterval = 0;
            this.Dialer.Options = new DotRas.RasDialOptions(false, false, false, false, false, false, false, false, false, false);
            this.Dialer.SynchronizingObject = this;
            this.Dialer.StateChanged += new System.EventHandler<DotRas.StateChangedEventArgs>(this.Dialer_StateChanged);
            this.Dialer.DialCompleted += new System.EventHandler<DotRas.DialCompletedEventArgs>(this.Dialer_DialCompleted);
            // 
            // StatusTextBox
            // 
            this.StatusTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusTextBox.Location = new System.Drawing.Point(12, 12);
            this.StatusTextBox.Multiline = true;
            this.StatusTextBox.Name = "StatusTextBox";
            this.StatusTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.StatusTextBox.Size = new System.Drawing.Size(321, 220);
            this.StatusTextBox.TabIndex = 2;
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DisconnectButton.Enabled = false;
            this.DisconnectButton.Location = new System.Drawing.Point(12, 294);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(75, 23);
            this.DisconnectButton.TabIndex = 3;
            this.DisconnectButton.Text = "D&isconnect";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 329);
            this.Controls.Add(this.DisconnectButton);
            this.Controls.Add(this.StatusTextBox);
            this.Controls.Add(this.DialButton);
            this.Controls.Add(this.CreateEntryButton);
            this.Name = "MainForm";
            this.Text = "Create and Dial VPN Entry Example";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DotRas.RasPhoneBook AllUsersPhoneBook;
        private System.Windows.Forms.Button CreateEntryButton;
        private System.Windows.Forms.Button DialButton;
        private DotRas.RasDialer Dialer;
        private System.Windows.Forms.TextBox StatusTextBox;
        private System.Windows.Forms.Button DisconnectButton;
    }
}

