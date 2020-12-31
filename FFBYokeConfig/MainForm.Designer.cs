namespace FFBYokeConfig
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numUD_DELAYS = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCALIBRATION = new System.Windows.Forms.Button();
            this.btnSaveDefault = new System.Windows.Forms.Button();
            this.btnLoadEEP = new System.Windows.Forms.Button();
            this.btnSaveEEP = new System.Windows.Forms.Button();
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.buttonWriteYoke = new System.Windows.Forms.Button();
            this.btnReadYoke = new System.Windows.Forms.Button();
            this.listBoxSerialPorts = new System.Windows.Forms.ComboBox();
            this.chkBoxAutoSave = new System.Windows.Forms.CheckBox();
            this.listBoxInfoTrace = new System.Windows.Forms.ListBox();
            this.gbGains = new System.Windows.Forms.GroupBox();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.labelInfo = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkBoxAutoCalibration = new System.Windows.Forms.CheckBox();
            this.chkBoxMotorInv_Y = new System.Windows.Forms.CheckBox();
            this.chkBoxMotorInv_X = new System.Windows.Forms.CheckBox();
            this.chkBoxSwapXYfoces = new System.Windows.Forms.CheckBox();
            this.gbPID = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_DELAYS)).BeginInit();
            this.gbGains.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numUD_DELAYS);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnCALIBRATION);
            this.groupBox1.Controls.Add(this.btnSaveDefault);
            this.groupBox1.Controls.Add(this.btnLoadEEP);
            this.groupBox1.Controls.Add(this.btnSaveEEP);
            this.groupBox1.Controls.Add(this.btnOpenPort);
            this.groupBox1.Controls.Add(this.buttonWriteYoke);
            this.groupBox1.Controls.Add(this.btnReadYoke);
            this.groupBox1.Controls.Add(this.listBoxSerialPorts);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 150);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control";
            // 
            // numUD_DELAYS
            // 
            this.numUD_DELAYS.Location = new System.Drawing.Point(178, 26);
            this.numUD_DELAYS.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUD_DELAYS.Name = "numUD_DELAYS";
            this.numUD_DELAYS.Size = new System.Drawing.Size(100, 20);
            this.numUD_DELAYS.TabIndex = 2;
            this.numUD_DELAYS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numUD_DELAYS.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(175, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Delays: (ms)";
            // 
            // btnCALIBRATION
            // 
            this.btnCALIBRATION.Enabled = false;
            this.btnCALIBRATION.Location = new System.Drawing.Point(6, 114);
            this.btnCALIBRATION.Name = "btnCALIBRATION";
            this.btnCALIBRATION.Size = new System.Drawing.Size(128, 24);
            this.btnCALIBRATION.TabIndex = 7;
            this.btnCALIBRATION.Text = "CALIBRATION";
            this.btnCALIBRATION.UseVisualStyleBackColor = true;
            // 
            // btnSaveDefault
            // 
            this.btnSaveDefault.Enabled = false;
            this.btnSaveDefault.Location = new System.Drawing.Point(150, 114);
            this.btnSaveDefault.Name = "btnSaveDefault";
            this.btnSaveDefault.Size = new System.Drawing.Size(128, 24);
            this.btnSaveDefault.TabIndex = 8;
            this.btnSaveDefault.Text = "RESET DEFAULT";
            this.btnSaveDefault.UseVisualStyleBackColor = true;
            this.btnSaveDefault.Click += new System.EventHandler(this.btn_RESET_DEFAULT_Click);
            // 
            // btnLoadEEP
            // 
            this.btnLoadEEP.Enabled = false;
            this.btnLoadEEP.Location = new System.Drawing.Point(6, 84);
            this.btnLoadEEP.Name = "btnLoadEEP";
            this.btnLoadEEP.Size = new System.Drawing.Size(128, 24);
            this.btnLoadEEP.TabIndex = 5;
            this.btnLoadEEP.Text = "LOAD EEP";
            this.btnLoadEEP.UseVisualStyleBackColor = true;
            this.btnLoadEEP.Click += new System.EventHandler(this.btn_LOAD_EEPROM_Click);
            // 
            // btnSaveEEP
            // 
            this.btnSaveEEP.Enabled = false;
            this.btnSaveEEP.Location = new System.Drawing.Point(150, 84);
            this.btnSaveEEP.Name = "btnSaveEEP";
            this.btnSaveEEP.Size = new System.Drawing.Size(128, 24);
            this.btnSaveEEP.TabIndex = 6;
            this.btnSaveEEP.Text = "SAVE EEP";
            this.btnSaveEEP.UseVisualStyleBackColor = true;
            this.btnSaveEEP.Click += new System.EventHandler(this.btn_SAVE_EEPROM_Click);
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.Location = new System.Drawing.Point(6, 22);
            this.btnOpenPort.Name = "btnOpenPort";
            this.btnOpenPort.Size = new System.Drawing.Size(80, 24);
            this.btnOpenPort.TabIndex = 0;
            this.btnOpenPort.Text = "OPEN PORT";
            this.btnOpenPort.UseVisualStyleBackColor = true;
            this.btnOpenPort.Click += new System.EventHandler(this.btn_OPEN_COMPORT_Click);
            // 
            // buttonWriteYoke
            // 
            this.buttonWriteYoke.Location = new System.Drawing.Point(150, 53);
            this.buttonWriteYoke.Name = "buttonWriteYoke";
            this.buttonWriteYoke.Size = new System.Drawing.Size(128, 24);
            this.buttonWriteYoke.TabIndex = 4;
            this.buttonWriteYoke.Text = "WRITE YOKE";
            this.buttonWriteYoke.UseVisualStyleBackColor = true;
            this.buttonWriteYoke.Click += new System.EventHandler(this.btn_WRITE_YOKE_Click);
            // 
            // btnReadYoke
            // 
            this.btnReadYoke.Location = new System.Drawing.Point(6, 53);
            this.btnReadYoke.Name = "btnReadYoke";
            this.btnReadYoke.Size = new System.Drawing.Size(128, 24);
            this.btnReadYoke.TabIndex = 3;
            this.btnReadYoke.Text = "READ YOKE";
            this.btnReadYoke.UseVisualStyleBackColor = true;
            this.btnReadYoke.Click += new System.EventHandler(this.btn_READ_YOKE_Click);
            // 
            // listBoxSerialPorts
            // 
            this.listBoxSerialPorts.AllowDrop = true;
            this.listBoxSerialPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listBoxSerialPorts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxSerialPorts.FormattingEnabled = true;
            this.listBoxSerialPorts.Location = new System.Drawing.Point(92, 22);
            this.listBoxSerialPorts.Name = "listBoxSerialPorts";
            this.listBoxSerialPorts.Size = new System.Drawing.Size(80, 24);
            this.listBoxSerialPorts.TabIndex = 1;
            // 
            // chkBoxAutoSave
            // 
            this.chkBoxAutoSave.AutoSize = true;
            this.chkBoxAutoSave.Location = new System.Drawing.Point(150, 15);
            this.chkBoxAutoSave.Name = "chkBoxAutoSave";
            this.chkBoxAutoSave.Size = new System.Drawing.Size(76, 17);
            this.chkBoxAutoSave.TabIndex = 12;
            this.chkBoxAutoSave.Text = "Auto Save";
            this.chkBoxAutoSave.UseVisualStyleBackColor = true;
            this.chkBoxAutoSave.CheckedChanged += new System.EventHandler(this.chkBoxAutoSave_CheckedChanged);
            // 
            // listBoxInfoTrace
            // 
            this.listBoxInfoTrace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxInfoTrace.FormattingEnabled = true;
            this.listBoxInfoTrace.Location = new System.Drawing.Point(12, 450);
            this.listBoxInfoTrace.Name = "listBoxInfoTrace";
            this.listBoxInfoTrace.ScrollAlwaysVisible = true;
            this.listBoxInfoTrace.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxInfoTrace.Size = new System.Drawing.Size(710, 121);
            this.listBoxInfoTrace.TabIndex = 47;
            // 
            // gbGains
            // 
            this.gbGains.Controls.Add(this.linkLabel);
            this.gbGains.Controls.Add(this.labelInfo);
            this.gbGains.Location = new System.Drawing.Point(302, 12);
            this.gbGains.Name = "gbGains";
            this.gbGains.Size = new System.Drawing.Size(420, 432);
            this.gbGains.TabIndex = 48;
            this.gbGains.TabStop = false;
            this.gbGains.Text = "Gains";
            // 
            // linkLabel
            // 
            this.linkLabel.AutoSize = true;
            this.linkLabel.Location = new System.Drawing.Point(108, 408);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(199, 13);
            this.linkLabel.TabIndex = 1;
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "https://github.com/billydragon/FFBYoke";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(114, 390);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(187, 13);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "FFBYoke Configuration by Billydragon.";
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkBoxAutoCalibration);
            this.groupBox3.Controls.Add(this.chkBoxMotorInv_Y);
            this.groupBox3.Controls.Add(this.chkBoxAutoSave);
            this.groupBox3.Controls.Add(this.chkBoxMotorInv_X);
            this.groupBox3.Controls.Add(this.chkBoxSwapXYfoces);
            this.groupBox3.Location = new System.Drawing.Point(12, 168);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(284, 84);
            this.groupBox3.TabIndex = 49;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Option";
            // 
            // chkBoxAutoCalibration
            // 
            this.chkBoxAutoCalibration.AutoSize = true;
            this.chkBoxAutoCalibration.Location = new System.Drawing.Point(150, 38);
            this.chkBoxAutoCalibration.Name = "chkBoxAutoCalibration";
            this.chkBoxAutoCalibration.Size = new System.Drawing.Size(100, 17);
            this.chkBoxAutoCalibration.TabIndex = 13;
            this.chkBoxAutoCalibration.Text = "Auto Calibration";
            this.chkBoxAutoCalibration.UseVisualStyleBackColor = true;
            this.chkBoxAutoCalibration.CheckedChanged += new System.EventHandler(this.chkBoxAutoCalibration_CheckedChanged);
            // 
            // chkBoxMotorInv_Y
            // 
            this.chkBoxMotorInv_Y.AutoSize = true;
            this.chkBoxMotorInv_Y.Location = new System.Drawing.Point(6, 38);
            this.chkBoxMotorInv_Y.Name = "chkBoxMotorInv_Y";
            this.chkBoxMotorInv_Y.Size = new System.Drawing.Size(93, 17);
            this.chkBoxMotorInv_Y.TabIndex = 10;
            this.chkBoxMotorInv_Y.Text = "Motor Y Invert";
            this.chkBoxMotorInv_Y.UseVisualStyleBackColor = true;
            this.chkBoxMotorInv_Y.CheckedChanged += new System.EventHandler(this.chkBoxMotorInv_Y_CheckedChanged);
            // 
            // chkBoxMotorInv_X
            // 
            this.chkBoxMotorInv_X.AutoSize = true;
            this.chkBoxMotorInv_X.Location = new System.Drawing.Point(6, 15);
            this.chkBoxMotorInv_X.Name = "chkBoxMotorInv_X";
            this.chkBoxMotorInv_X.Size = new System.Drawing.Size(93, 17);
            this.chkBoxMotorInv_X.TabIndex = 9;
            this.chkBoxMotorInv_X.Text = "Motor X Invert";
            this.chkBoxMotorInv_X.UseVisualStyleBackColor = true;
            this.chkBoxMotorInv_X.CheckedChanged += new System.EventHandler(this.chkBoxMotorInv_X_CheckedChanged);
            // 
            // chkBoxSwapXYfoces
            // 
            this.chkBoxSwapXYfoces.AutoSize = true;
            this.chkBoxSwapXYfoces.Location = new System.Drawing.Point(6, 61);
            this.chkBoxSwapXYfoces.Name = "chkBoxSwapXYfoces";
            this.chkBoxSwapXYfoces.Size = new System.Drawing.Size(105, 17);
            this.chkBoxSwapXYfoces.TabIndex = 11;
            this.chkBoxSwapXYfoces.Text = "Swap XY_forces";
            this.chkBoxSwapXYfoces.UseVisualStyleBackColor = true;
            this.chkBoxSwapXYfoces.CheckedChanged += new System.EventHandler(this.chkBoxSwapAxis_CheckedChanged);
            // 
            // gbPID
            // 
            this.gbPID.Location = new System.Drawing.Point(12, 258);
            this.gbPID.Name = "gbPID";
            this.gbPID.Size = new System.Drawing.Size(284, 186);
            this.gbPID.TabIndex = 50;
            this.gbPID.TabStop = false;
            this.gbPID.Text = "Gains & PIDs";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 583);
            this.Controls.Add(this.gbPID);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbGains);
            this.Controls.Add(this.listBoxInfoTrace);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FFBYoke Config v1.0.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_DELAYS)).EndInit();
            this.gbGains.ResumeLayout(false);
            this.gbGains.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox listBoxSerialPorts;
        private System.Windows.Forms.Button buttonWriteYoke;
        private System.Windows.Forms.Button btnReadYoke;
        private System.Windows.Forms.ListBox listBoxInfoTrace;
        private System.Windows.Forms.Button btnOpenPort;
        private System.Windows.Forms.GroupBox gbGains;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkBoxSwapXYfoces;
        private System.Windows.Forms.Button btnSaveEEP;
        private System.Windows.Forms.CheckBox chkBoxMotorInv_Y;
        private System.Windows.Forms.CheckBox chkBoxMotorInv_X;
        private System.Windows.Forms.Button btnSaveDefault;
        private System.Windows.Forms.Button btnLoadEEP;
        private System.Windows.Forms.GroupBox gbPID;
        private System.Windows.Forms.CheckBox chkBoxAutoSave;
        private System.Windows.Forms.Button btnCALIBRATION;
        private System.Windows.Forms.LinkLabel linkLabel;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.NumericUpDown numUD_DELAYS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkBoxAutoCalibration;
    }
}
