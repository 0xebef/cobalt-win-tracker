namespace CobaltWinTracker
{
    partial class ConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.labelIntervalSec = new System.Windows.Forms.Label();
            this.numericIntervalSec = new System.Windows.Forms.NumericUpDown();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelHWID = new System.Windows.Forms.Label();
            this.textBoxHWID = new System.Windows.Forms.TextBox();
            this.checkBoxUseCell = new System.Windows.Forms.CheckBox();
            this.checkBoxUseWiFi = new System.Windows.Forms.CheckBox();
            this.checkBoxUseGPS = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericIntervalSec)).BeginInit();
            this.SuspendLayout();
            //
            // labelIntervalSec
            //
            this.labelIntervalSec.AutoSize = true;
            this.labelIntervalSec.Location = new System.Drawing.Point(35, 50);
            this.labelIntervalSec.Name = "labelIntervalSec";
            this.labelIntervalSec.Size = new System.Drawing.Size(98, 13);
            this.labelIntervalSec.TabIndex = 0;
            this.labelIntervalSec.Text = "Interval in Seconds";
            //
            // numericIntervalSec
            //
            this.numericIntervalSec.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericIntervalSec.Location = new System.Drawing.Point(152, 48);
            this.numericIntervalSec.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.numericIntervalSec.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericIntervalSec.Name = "numericIntervalSec";
            this.numericIntervalSec.Size = new System.Drawing.Size(144, 20);
            this.numericIntervalSec.TabIndex = 1;
            this.numericIntervalSec.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            //
            // buttonOK
            //
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(140, 187);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            //
            // buttonClose
            //
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(221, 187);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 6;
            this.buttonClose.Text = "Cancel";
            this.buttonClose.UseVisualStyleBackColor = true;
            //
            // labelHWID
            //
            this.labelHWID.AutoSize = true;
            this.labelHWID.Location = new System.Drawing.Point(35, 25);
            this.labelHWID.Name = "labelHWID";
            this.labelHWID.Size = new System.Drawing.Size(67, 13);
            this.labelHWID.TabIndex = 6;
            this.labelHWID.Text = "Hardware ID";
            //
            // textBoxHWID
            //
            this.textBoxHWID.Location = new System.Drawing.Point(152, 22);
            this.textBoxHWID.Name = "textBoxHWID";
            this.textBoxHWID.ReadOnly = true;
            this.textBoxHWID.Size = new System.Drawing.Size(144, 20);
            this.textBoxHWID.TabIndex = 0;
            //
            // checkBoxUseCell
            //
            this.checkBoxUseCell.AutoSize = true;
            this.checkBoxUseCell.Checked = true;
            this.checkBoxUseCell.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseCell.Location = new System.Drawing.Point(38, 114);
            this.checkBoxUseCell.Name = "checkBoxUseCell";
            this.checkBoxUseCell.Size = new System.Drawing.Size(191, 17);
            this.checkBoxUseCell.TabIndex = 3;
            this.checkBoxUseCell.Text = "Use Cell Towers for Geopositioning";
            this.checkBoxUseCell.UseVisualStyleBackColor = true;
            //
            // checkBoxUseWiFi
            //
            this.checkBoxUseWiFi.AutoSize = true;
            this.checkBoxUseWiFi.Checked = true;
            this.checkBoxUseWiFi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseWiFi.Location = new System.Drawing.Point(38, 146);
            this.checkBoxUseWiFi.Name = "checkBoxUseWiFi";
            this.checkBoxUseWiFi.Size = new System.Drawing.Size(207, 17);
            this.checkBoxUseWiFi.TabIndex = 4;
            this.checkBoxUseWiFi.Text = "Use WiFi Hot Spots for Geopositioning";
            this.checkBoxUseWiFi.UseVisualStyleBackColor = true;
            //
            // checkBoxUseGPS
            //
            this.checkBoxUseGPS.AutoSize = true;
            this.checkBoxUseGPS.Checked = true;
            this.checkBoxUseGPS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseGPS.Enabled = false;
            this.checkBoxUseGPS.Location = new System.Drawing.Point(38, 83);
            this.checkBoxUseGPS.Name = "checkBoxUseGPS";
            this.checkBoxUseGPS.Size = new System.Drawing.Size(158, 17);
            this.checkBoxUseGPS.TabIndex = 2;
            this.checkBoxUseGPS.TabStop = false;
            this.checkBoxUseGPS.Text = "Use GPS for Geopositioning";
            this.checkBoxUseGPS.UseVisualStyleBackColor = true;
            //
            // ConfigForm
            //
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(308, 222);
            this.Controls.Add(this.checkBoxUseGPS);
            this.Controls.Add(this.checkBoxUseWiFi);
            this.Controls.Add(this.checkBoxUseCell);
            this.Controls.Add(this.textBoxHWID);
            this.Controls.Add(this.labelHWID);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.numericIntervalSec);
            this.Controls.Add(this.labelIntervalSec);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ConfigForm";
            this.Text = "CobaltWinTracker";
            this.Shown += new System.EventHandler(this.ConfigForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numericIntervalSec)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelIntervalSec;
        private System.Windows.Forms.NumericUpDown numericIntervalSec;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelHWID;
        private System.Windows.Forms.TextBox textBoxHWID;
        private System.Windows.Forms.CheckBox checkBoxUseCell;
        private System.Windows.Forms.CheckBox checkBoxUseWiFi;
        private System.Windows.Forms.CheckBox checkBoxUseGPS;
    }
}

