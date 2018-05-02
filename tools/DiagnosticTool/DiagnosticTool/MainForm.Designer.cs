namespace DiagnosticTool
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
            this.DialButton = new System.Windows.Forms.Button();
            this.StatusTextBox = new System.Windows.Forms.TextBox();
            this.Dialer = new DotRas.RasDialer(this.components);
            this.BrowseButton = new System.Windows.Forms.Button();
            this.PhoneBookTextBox = new System.Windows.Forms.TextBox();
            this.EntryNamesComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.CredentialPromptCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // DialButton
            // 
            this.DialButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DialButton.Enabled = false;
            this.DialButton.Location = new System.Drawing.Point(251, 103);
            this.DialButton.Name = "DialButton";
            this.DialButton.Size = new System.Drawing.Size(75, 23);
            this.DialButton.TabIndex = 0;
            this.DialButton.Text = "Dial";
            this.DialButton.UseVisualStyleBackColor = true;
            this.DialButton.Click += new System.EventHandler(this.DialButton_Click);
            // 
            // StatusTextBox
            // 
            this.StatusTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.StatusTextBox.Location = new System.Drawing.Point(12, 192);
            this.StatusTextBox.Multiline = true;
            this.StatusTextBox.Name = "StatusTextBox";
            this.StatusTextBox.ReadOnly = true;
            this.StatusTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.StatusTextBox.Size = new System.Drawing.Size(395, 181);
            this.StatusTextBox.TabIndex = 1;
            // 
            // Dialer
            // 
            this.Dialer.AllowUseStoredCredentials = true;
            // TODO: Code generation for 'this.Dialer.AuthenticationCookie' failed because of Exception 'Invalid Primitive Type: System.IntPtr. Consider using CodeObjectCreateExpression.'.
            // TODO: Code generation for 'this.Dialer.CallbackId' failed because of Exception 'Invalid Primitive Type: System.IntPtr. Consider using CodeObjectCreateExpression.'.
            this.Dialer.Credentials = null;
            this.Dialer.EapOptions = new DotRas.RasEapOptions(false, false, false);
            this.Dialer.HangUpPollingInterval = 0;
            this.Dialer.Options = new DotRas.RasDialOptions(false, false, false, false, false, false, false, false, false, false, false);
            this.Dialer.SynchronizingObject = this;
            this.Dialer.StateChanged += new System.EventHandler<DotRas.StateChangedEventArgs>(this.Dialer_StateChanged);
            this.Dialer.DialCompleted += new System.EventHandler<DotRas.DialCompletedEventArgs>(this.Dialer_DialCompleted);
            this.Dialer.Error += new System.EventHandler<System.IO.ErrorEventArgs>(this.Dialer_Error);
            // 
            // BrowseButton
            // 
            this.BrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowseButton.Location = new System.Drawing.Point(332, 23);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseButton.TabIndex = 2;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // PhoneBookTextBox
            // 
            this.PhoneBookTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PhoneBookTextBox.Location = new System.Drawing.Point(12, 25);
            this.PhoneBookTextBox.Name = "PhoneBookTextBox";
            this.PhoneBookTextBox.ReadOnly = true;
            this.PhoneBookTextBox.Size = new System.Drawing.Size(314, 20);
            this.PhoneBookTextBox.TabIndex = 3;
            // 
            // EntryNamesComboBox
            // 
            this.EntryNamesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.EntryNamesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EntryNamesComboBox.Enabled = false;
            this.EntryNamesComboBox.FormattingEnabled = true;
            this.EntryNamesComboBox.Location = new System.Drawing.Point(52, 51);
            this.EntryNamesComboBox.Name = "EntryNamesComboBox";
            this.EntryNamesComboBox.Size = new System.Drawing.Size(274, 21);
            this.EntryNamesComboBox.TabIndex = 4;
            this.EntryNamesComboBox.SelectedIndexChanged += new System.EventHandler(this.EntryNamesComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Entry:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Connection Status";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Phonebook:";
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Enabled = false;
            this.CancelButton.Location = new System.Drawing.Point(332, 103);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 8;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // CredentialPromptCheckBox
            // 
            this.CredentialPromptCheckBox.AutoSize = true;
            this.CredentialPromptCheckBox.Checked = true;
            this.CredentialPromptCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CredentialPromptCheckBox.Location = new System.Drawing.Point(12, 107);
            this.CredentialPromptCheckBox.Name = "CredentialPromptCheckBox";
            this.CredentialPromptCheckBox.Size = new System.Drawing.Size(128, 17);
            this.CredentialPromptCheckBox.TabIndex = 9;
            this.CredentialPromptCheckBox.Text = "Prompt for credentials";
            this.CredentialPromptCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 385);
            this.Controls.Add(this.CredentialPromptCheckBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EntryNamesComboBox);
            this.Controls.Add(this.PhoneBookTextBox);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.StatusTextBox);
            this.Controls.Add(this.DialButton);
            this.Name = "MainForm";
            this.Text = "DotRas Diagnostics Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DialButton;
        private System.Windows.Forms.TextBox StatusTextBox;
        private DotRas.RasDialer Dialer;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.TextBox PhoneBookTextBox;
        private System.Windows.Forms.ComboBox EntryNamesComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.CheckBox CredentialPromptCheckBox;
    }
}

