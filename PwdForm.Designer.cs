namespace CobaltWinTracker
{
    partial class PwdForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PwdForm));
            this.OKButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.PwdLabel = new System.Windows.Forms.Label();
            this.PwdBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            //
            // OKButton
            //
            this.OKButton.Location = new System.Drawing.Point(215, 97);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 1;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            //
            // CloseButton
            //
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(296, 97);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.Text = "Cancel";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CancelButton_Click);
            //
            // PwdLabel
            //
            this.PwdLabel.AutoSize = true;
            this.PwdLabel.Location = new System.Drawing.Point(12, 36);
            this.PwdLabel.Name = "PwdLabel";
            this.PwdLabel.Size = new System.Drawing.Size(53, 13);
            this.PwdLabel.TabIndex = 2;
            this.PwdLabel.Text = "Password";
            //
            // PwdBox
            //
            this.PwdBox.Location = new System.Drawing.Point(12, 52);
            this.PwdBox.Name = "PwdBox";
            this.PwdBox.Size = new System.Drawing.Size(359, 20);
            this.PwdBox.TabIndex = 1;
            this.PwdBox.UseSystemPasswordChar = true;
            //
            // PwdForm
            //
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 130);
            this.Controls.Add(this.PwdBox);
            this.Controls.Add(this.PwdLabel);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.OKButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PwdForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Authorization";
            this.Shown += new System.EventHandler(this.PwdForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label PwdLabel;
        private System.Windows.Forms.TextBox PwdBox;
    }
}
