namespace DotRas.Samples.GetActiveConnectionIPAddress
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
            this.GetAddressButton = new System.Windows.Forms.Button();
            this.ConnectionsComboBox = new System.Windows.Forms.ComboBox();
            this.ConnectionsLabel = new System.Windows.Forms.Label();
            this.ClientAddressLabel = new System.Windows.Forms.Label();
            this.ClientAddressTextBox = new System.Windows.Forms.TextBox();
            this.ServerAddressTextBox = new System.Windows.Forms.TextBox();
            this.ServerAddressLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GetAddressButton
            // 
            this.GetAddressButton.Enabled = false;
            this.GetAddressButton.Location = new System.Drawing.Point(178, 229);
            this.GetAddressButton.Name = "GetAddressButton";
            this.GetAddressButton.Size = new System.Drawing.Size(94, 23);
            this.GetAddressButton.TabIndex = 0;
            this.GetAddressButton.Text = "Get Address";
            this.GetAddressButton.UseVisualStyleBackColor = true;
            this.GetAddressButton.Click += new System.EventHandler(this.GetAddressButton_Click);
            // 
            // ConnectionsComboBox
            // 
            this.ConnectionsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ConnectionsComboBox.FormattingEnabled = true;
            this.ConnectionsComboBox.Location = new System.Drawing.Point(12, 34);
            this.ConnectionsComboBox.Name = "ConnectionsComboBox";
            this.ConnectionsComboBox.Size = new System.Drawing.Size(260, 21);
            this.ConnectionsComboBox.TabIndex = 1;
            this.ConnectionsComboBox.SelectedIndexChanged += new System.EventHandler(this.ConnectionsComboBox_SelectedIndexChanged);
            // 
            // ConnectionsLabel
            // 
            this.ConnectionsLabel.AutoSize = true;
            this.ConnectionsLabel.Location = new System.Drawing.Point(12, 18);
            this.ConnectionsLabel.Name = "ConnectionsLabel";
            this.ConnectionsLabel.Size = new System.Drawing.Size(69, 13);
            this.ConnectionsLabel.TabIndex = 2;
            this.ConnectionsLabel.Text = "Connections:";
            // 
            // ClientAddressLabel
            // 
            this.ClientAddressLabel.AutoSize = true;
            this.ClientAddressLabel.Location = new System.Drawing.Point(12, 96);
            this.ClientAddressLabel.Name = "ClientAddressLabel";
            this.ClientAddressLabel.Size = new System.Drawing.Size(90, 13);
            this.ClientAddressLabel.TabIndex = 3;
            this.ClientAddressLabel.Text = "Client IP Address:";
            // 
            // ClientAddressTextBox
            // 
            this.ClientAddressTextBox.Location = new System.Drawing.Point(12, 112);
            this.ClientAddressTextBox.Name = "ClientAddressTextBox";
            this.ClientAddressTextBox.ReadOnly = true;
            this.ClientAddressTextBox.Size = new System.Drawing.Size(260, 20);
            this.ClientAddressTextBox.TabIndex = 4;
            // 
            // ServerAddressTextBox
            // 
            this.ServerAddressTextBox.Location = new System.Drawing.Point(12, 158);
            this.ServerAddressTextBox.Name = "ServerAddressTextBox";
            this.ServerAddressTextBox.ReadOnly = true;
            this.ServerAddressTextBox.Size = new System.Drawing.Size(260, 20);
            this.ServerAddressTextBox.TabIndex = 6;
            // 
            // ServerAddressLabel
            // 
            this.ServerAddressLabel.AutoSize = true;
            this.ServerAddressLabel.Location = new System.Drawing.Point(12, 142);
            this.ServerAddressLabel.Name = "ServerAddressLabel";
            this.ServerAddressLabel.Size = new System.Drawing.Size(95, 13);
            this.ServerAddressLabel.TabIndex = 5;
            this.ServerAddressLabel.Text = "Server IP Address:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Controls.Add(this.ServerAddressTextBox);
            this.Controls.Add(this.ServerAddressLabel);
            this.Controls.Add(this.ClientAddressTextBox);
            this.Controls.Add(this.ClientAddressLabel);
            this.Controls.Add(this.ConnectionsLabel);
            this.Controls.Add(this.ConnectionsComboBox);
            this.Controls.Add(this.GetAddressButton);
            this.Name = "MainForm";
            this.Text = "Get IP Addresses Example";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GetAddressButton;
        private System.Windows.Forms.ComboBox ConnectionsComboBox;
        private System.Windows.Forms.Label ConnectionsLabel;
        private System.Windows.Forms.Label ClientAddressLabel;
        private System.Windows.Forms.TextBox ClientAddressTextBox;
        private System.Windows.Forms.TextBox ServerAddressTextBox;
        private System.Windows.Forms.Label ServerAddressLabel;
    }
}

