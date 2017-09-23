namespace TmdDesign
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabInputData = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnCancelCalculations = new System.Windows.Forms.Button();
            this.chBoxSaveResultsToExcel = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.radioFiniteDifferenceMethod = new System.Windows.Forms.RadioButton();
            this.radioNewmarkMethod = new System.Windows.Forms.RadioButton();
            this.btnRunCalculations = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtTimeInterval = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFinalFrequency = new System.Windows.Forms.TextBox();
            this.txtStartFrequency = new System.Windows.Forms.TextBox();
            this.btnCalculateExcitationFrequency = new System.Windows.Forms.Button();
            this.txtExcitationFrequencyInterval = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtExcitationForce = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chBoxIncludeStrDamping = new System.Windows.Forms.CheckBox();
            this.txtStrDampingRatio = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStrNaturalFrequency = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStrModalMass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chBoxEnterOwnParameters = new System.Windows.Forms.CheckBox();
            this.btnCalculateTmdParameters = new System.Windows.Forms.Button();
            this.txtOptimumTmdDapingRatio = new System.Windows.Forms.TextBox();
            this.txtTmdFrequency = new System.Windows.Forms.TextBox();
            this.txtOptimumTmdFrequency = new System.Windows.Forms.TextBox();
            this.txtTmdMass = new System.Windows.Forms.TextBox();
            this.txtAssumedTmdToStructureMassRatio = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabResults = new System.Windows.Forms.TabPage();
            this.lblMaxValue = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radioTMD = new System.Windows.Forms.RadioButton();
            this.radioStructureWithTMD = new System.Windows.Forms.RadioButton();
            this.radioStructureWithoutTMD = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioDisplacement = new System.Windows.Forms.RadioButton();
            this.radioAcceleration = new System.Windows.Forms.RadioButton();
            this.chartResults = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabInputData.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabResults.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartResults)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabInputData);
            this.tabControl.Controls.Add(this.tabResults);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(748, 615);
            this.tabControl.TabIndex = 1;
            // 
            // tabInputData
            // 
            this.tabInputData.BackColor = System.Drawing.SystemColors.Control;
            this.tabInputData.Controls.Add(this.groupBox7);
            this.tabInputData.Controls.Add(this.groupBox4);
            this.tabInputData.Controls.Add(this.groupBox3);
            this.tabInputData.Controls.Add(this.pictureBox1);
            this.tabInputData.Controls.Add(this.groupBox2);
            this.tabInputData.Controls.Add(this.groupBox1);
            this.tabInputData.Location = new System.Drawing.Point(4, 22);
            this.tabInputData.Name = "tabInputData";
            this.tabInputData.Padding = new System.Windows.Forms.Padding(3);
            this.tabInputData.Size = new System.Drawing.Size(740, 589);
            this.tabInputData.TabIndex = 0;
            this.tabInputData.Text = "Input Data";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnCancelCalculations);
            this.groupBox7.Controls.Add(this.chBoxSaveResultsToExcel);
            this.groupBox7.Controls.Add(this.groupBox8);
            this.groupBox7.Controls.Add(this.btnRunCalculations);
            this.groupBox7.Location = new System.Drawing.Point(327, 426);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(407, 157);
            this.groupBox7.TabIndex = 22;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Calculations";
            // 
            // btnCancelCalculations
            // 
            this.btnCancelCalculations.Enabled = false;
            this.btnCancelCalculations.Location = new System.Drawing.Point(215, 110);
            this.btnCancelCalculations.Name = "btnCancelCalculations";
            this.btnCancelCalculations.Size = new System.Drawing.Size(186, 41);
            this.btnCancelCalculations.TabIndex = 26;
            this.btnCancelCalculations.Text = "Cancel calculations";
            this.btnCancelCalculations.UseVisualStyleBackColor = true;
            this.btnCancelCalculations.Click += new System.EventHandler(this.btnCancelCalculations_Click);
            // 
            // chBoxSaveResultsToExcel
            // 
            this.chBoxSaveResultsToExcel.AutoSize = true;
            this.chBoxSaveResultsToExcel.Location = new System.Drawing.Point(12, 82);
            this.chBoxSaveResultsToExcel.Name = "chBoxSaveResultsToExcel";
            this.chBoxSaveResultsToExcel.Size = new System.Drawing.Size(156, 17);
            this.chBoxSaveResultsToExcel.TabIndex = 25;
            this.chBoxSaveResultsToExcel.Text = "Save time history Excel files";
            this.chBoxSaveResultsToExcel.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.radioFiniteDifferenceMethod);
            this.groupBox8.Controls.Add(this.radioNewmarkMethod);
            this.groupBox8.Location = new System.Drawing.Point(6, 19);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(395, 50);
            this.groupBox8.TabIndex = 24;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Solving method:";
            // 
            // radioFiniteDifferenceMethod
            // 
            this.radioFiniteDifferenceMethod.AutoSize = true;
            this.radioFiniteDifferenceMethod.Location = new System.Drawing.Point(174, 19);
            this.radioFiniteDifferenceMethod.Name = "radioFiniteDifferenceMethod";
            this.radioFiniteDifferenceMethod.Size = new System.Drawing.Size(138, 17);
            this.radioFiniteDifferenceMethod.TabIndex = 1;
            this.radioFiniteDifferenceMethod.Text = "Finite difference method";
            this.radioFiniteDifferenceMethod.UseVisualStyleBackColor = true;
            // 
            // radioNewmarkMethod
            // 
            this.radioNewmarkMethod.AutoSize = true;
            this.radioNewmarkMethod.Checked = true;
            this.radioNewmarkMethod.Location = new System.Drawing.Point(6, 19);
            this.radioNewmarkMethod.Name = "radioNewmarkMethod";
            this.radioNewmarkMethod.Size = new System.Drawing.Size(109, 17);
            this.radioNewmarkMethod.TabIndex = 0;
            this.radioNewmarkMethod.TabStop = true;
            this.radioNewmarkMethod.Text = "Newmark Method";
            this.radioNewmarkMethod.UseVisualStyleBackColor = true;
            // 
            // btnRunCalculations
            // 
            this.btnRunCalculations.Location = new System.Drawing.Point(12, 110);
            this.btnRunCalculations.Name = "btnRunCalculations";
            this.btnRunCalculations.Size = new System.Drawing.Size(181, 41);
            this.btnRunCalculations.TabIndex = 23;
            this.btnRunCalculations.Text = "Run calculations";
            this.btnRunCalculations.UseVisualStyleBackColor = true;
            this.btnRunCalculations.Click += new System.EventHandler(this.btnRunCalculations_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtTimeInterval);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Location = new System.Drawing.Point(6, 357);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(310, 46);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Time parameters";
            // 
            // txtTimeInterval
            // 
            this.txtTimeInterval.Location = new System.Drawing.Point(185, 19);
            this.txtTimeInterval.Name = "txtTimeInterval";
            this.txtTimeInterval.Size = new System.Drawing.Size(119, 20);
            this.txtTimeInterval.TabIndex = 15;
            this.txtTimeInterval.Text = "0.01";
            this.txtTimeInterval.Validating += new System.ComponentModel.CancelEventHandler(this.txtValidating);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Time interval [s]";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtFinalFrequency);
            this.groupBox3.Controls.Add(this.txtStartFrequency);
            this.groupBox3.Controls.Add(this.btnCalculateExcitationFrequency);
            this.groupBox3.Controls.Add(this.txtExcitationFrequencyInterval);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtExcitationForce);
            this.groupBox3.Location = new System.Drawing.Point(6, 409);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(310, 174);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Force Parameters";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 99);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "Final frequency [Hz]";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Start frequency [Hz]";
            // 
            // txtFinalFrequency
            // 
            this.txtFinalFrequency.Location = new System.Drawing.Point(185, 96);
            this.txtFinalFrequency.Name = "txtFinalFrequency";
            this.txtFinalFrequency.Size = new System.Drawing.Size(119, 20);
            this.txtFinalFrequency.TabIndex = 20;
            this.txtFinalFrequency.Text = "4";
            this.txtFinalFrequency.Validating += new System.ComponentModel.CancelEventHandler(this.txtValidating);
            // 
            // txtStartFrequency
            // 
            this.txtStartFrequency.Location = new System.Drawing.Point(185, 70);
            this.txtStartFrequency.Name = "txtStartFrequency";
            this.txtStartFrequency.Size = new System.Drawing.Size(119, 20);
            this.txtStartFrequency.TabIndex = 19;
            this.txtStartFrequency.Text = "0.1";
            this.txtStartFrequency.Validating += new System.ComponentModel.CancelEventHandler(this.txtValidating);
            // 
            // btnCalculateExcitationFrequency
            // 
            this.btnCalculateExcitationFrequency.Location = new System.Drawing.Point(185, 127);
            this.btnCalculateExcitationFrequency.Name = "btnCalculateExcitationFrequency";
            this.btnCalculateExcitationFrequency.Size = new System.Drawing.Size(119, 41);
            this.btnCalculateExcitationFrequency.TabIndex = 21;
            this.btnCalculateExcitationFrequency.Text = "Calculate Excitation Force";
            this.btnCalculateExcitationFrequency.UseVisualStyleBackColor = true;
            this.btnCalculateExcitationFrequency.Click += new System.EventHandler(this.btnCalculateExcitationFrequency_Click);
            // 
            // txtExcitationFrequencyInterval
            // 
            this.txtExcitationFrequencyInterval.Location = new System.Drawing.Point(185, 44);
            this.txtExcitationFrequencyInterval.Name = "txtExcitationFrequencyInterval";
            this.txtExcitationFrequencyInterval.Size = new System.Drawing.Size(119, 20);
            this.txtExcitationFrequencyInterval.TabIndex = 18;
            this.txtExcitationFrequencyInterval.Text = "0.1";
            this.txtExcitationFrequencyInterval.Validating += new System.ComponentModel.CancelEventHandler(this.txtValidating);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(162, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Excitation frequency interval [Hz]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Excitation Force [N]";
            // 
            // txtExcitationForce
            // 
            this.txtExcitationForce.Location = new System.Drawing.Point(185, 18);
            this.txtExcitationForce.Name = "txtExcitationForce";
            this.txtExcitationForce.Size = new System.Drawing.Size(119, 20);
            this.txtExcitationForce.TabIndex = 17;
            this.txtExcitationForce.Text = "1000000";
            this.txtExcitationForce.Validating += new System.ComponentModel.CancelEventHandler(this.txtValidating);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TmdDesign.Properties.Resources.Drawing_Model;
            this.pictureBox1.Location = new System.Drawing.Point(327, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(407, 405);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chBoxIncludeStrDamping);
            this.groupBox2.Controls.Add(this.txtStrDampingRatio);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtStrNaturalFrequency);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtStrModalMass);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 223);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(310, 128);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Structure Parameters";
            // 
            // chBoxIncludeStrDamping
            // 
            this.chBoxIncludeStrDamping.AutoSize = true;
            this.chBoxIncludeStrDamping.Location = new System.Drawing.Point(10, 105);
            this.chBoxIncludeStrDamping.Name = "chBoxIncludeStrDamping";
            this.chBoxIncludeStrDamping.Size = new System.Drawing.Size(149, 17);
            this.chBoxIncludeStrDamping.TabIndex = 13;
            this.chBoxIncludeStrDamping.Text = "Ignore Structural Damping";
            this.chBoxIncludeStrDamping.UseVisualStyleBackColor = true;
            // 
            // txtStrDampingRatio
            // 
            this.txtStrDampingRatio.Location = new System.Drawing.Point(185, 71);
            this.txtStrDampingRatio.Name = "txtStrDampingRatio";
            this.txtStrDampingRatio.Size = new System.Drawing.Size(119, 20);
            this.txtStrDampingRatio.TabIndex = 12;
            this.txtStrDampingRatio.Text = "0.5";
            this.txtStrDampingRatio.Validating += new System.ComponentModel.CancelEventHandler(this.txtValidating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Damping ratio [%]";
            // 
            // txtStrNaturalFrequency
            // 
            this.txtStrNaturalFrequency.Location = new System.Drawing.Point(185, 45);
            this.txtStrNaturalFrequency.Name = "txtStrNaturalFrequency";
            this.txtStrNaturalFrequency.Size = new System.Drawing.Size(119, 20);
            this.txtStrNaturalFrequency.TabIndex = 11;
            this.txtStrNaturalFrequency.Text = "1.5";
            this.txtStrNaturalFrequency.Validating += new System.ComponentModel.CancelEventHandler(this.txtValidating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Frequency [Hz]";
            // 
            // txtStrModalMass
            // 
            this.txtStrModalMass.Location = new System.Drawing.Point(185, 22);
            this.txtStrModalMass.Name = "txtStrModalMass";
            this.txtStrModalMass.Size = new System.Drawing.Size(119, 20);
            this.txtStrModalMass.TabIndex = 10;
            this.txtStrModalMass.Text = "2000000";
            this.txtStrModalMass.Validating += new System.ComponentModel.CancelEventHandler(this.txtValidating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Modal mass [kg]";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chBoxEnterOwnParameters);
            this.groupBox1.Controls.Add(this.btnCalculateTmdParameters);
            this.groupBox1.Controls.Add(this.txtOptimumTmdDapingRatio);
            this.groupBox1.Controls.Add(this.txtTmdFrequency);
            this.groupBox1.Controls.Add(this.txtOptimumTmdFrequency);
            this.groupBox1.Controls.Add(this.txtTmdMass);
            this.groupBox1.Controls.Add(this.txtAssumedTmdToStructureMassRatio);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(6, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 209);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TMD Parameters";
            // 
            // chBoxEnterOwnParameters
            // 
            this.chBoxEnterOwnParameters.AutoSize = true;
            this.chBoxEnterOwnParameters.Location = new System.Drawing.Point(10, 169);
            this.chBoxEnterOwnParameters.Name = "chBoxEnterOwnParameters";
            this.chBoxEnterOwnParameters.Size = new System.Drawing.Size(129, 17);
            this.chBoxEnterOwnParameters.TabIndex = 7;
            this.chBoxEnterOwnParameters.Text = "Enter own parameters";
            this.chBoxEnterOwnParameters.UseVisualStyleBackColor = true;
            this.chBoxEnterOwnParameters.CheckedChanged += new System.EventHandler(this.chBoxEnterOwnParameters_CheckedChanged);
            // 
            // btnCalculateTmdParameters
            // 
            this.btnCalculateTmdParameters.Location = new System.Drawing.Point(185, 155);
            this.btnCalculateTmdParameters.Name = "btnCalculateTmdParameters";
            this.btnCalculateTmdParameters.Size = new System.Drawing.Size(119, 43);
            this.btnCalculateTmdParameters.TabIndex = 8;
            this.btnCalculateTmdParameters.Text = "Calculate TMD Parameters";
            this.btnCalculateTmdParameters.UseVisualStyleBackColor = true;
            this.btnCalculateTmdParameters.Click += new System.EventHandler(this.btnCalculateTmdParameters_Click);
            // 
            // txtOptimumTmdDapingRatio
            // 
            this.txtOptimumTmdDapingRatio.Location = new System.Drawing.Point(185, 129);
            this.txtOptimumTmdDapingRatio.Name = "txtOptimumTmdDapingRatio";
            this.txtOptimumTmdDapingRatio.ReadOnly = true;
            this.txtOptimumTmdDapingRatio.Size = new System.Drawing.Size(119, 20);
            this.txtOptimumTmdDapingRatio.TabIndex = 6;
            this.txtOptimumTmdDapingRatio.TabStop = false;
            this.txtOptimumTmdDapingRatio.Text = "12.727";
            this.txtOptimumTmdDapingRatio.Validating += new System.ComponentModel.CancelEventHandler(this.txtValidating);
            // 
            // txtTmdFrequency
            // 
            this.txtTmdFrequency.Location = new System.Drawing.Point(185, 103);
            this.txtTmdFrequency.Name = "txtTmdFrequency";
            this.txtTmdFrequency.ReadOnly = true;
            this.txtTmdFrequency.Size = new System.Drawing.Size(119, 20);
            this.txtTmdFrequency.TabIndex = 5;
            this.txtTmdFrequency.TabStop = false;
            this.txtTmdFrequency.Text = "2.857";
            this.txtTmdFrequency.Validating += new System.ComponentModel.CancelEventHandler(this.txtValidating);
            // 
            // txtOptimumTmdFrequency
            // 
            this.txtOptimumTmdFrequency.Location = new System.Drawing.Point(185, 77);
            this.txtOptimumTmdFrequency.Name = "txtOptimumTmdFrequency";
            this.txtOptimumTmdFrequency.ReadOnly = true;
            this.txtOptimumTmdFrequency.Size = new System.Drawing.Size(119, 20);
            this.txtOptimumTmdFrequency.TabIndex = 4;
            this.txtOptimumTmdFrequency.TabStop = false;
            this.txtOptimumTmdFrequency.Text = "95.2";
            this.txtOptimumTmdFrequency.Validating += new System.ComponentModel.CancelEventHandler(this.txtValidating);
            // 
            // txtTmdMass
            // 
            this.txtTmdMass.Location = new System.Drawing.Point(185, 51);
            this.txtTmdMass.Name = "txtTmdMass";
            this.txtTmdMass.ReadOnly = true;
            this.txtTmdMass.Size = new System.Drawing.Size(119, 20);
            this.txtTmdMass.TabIndex = 3;
            this.txtTmdMass.TabStop = false;
            this.txtTmdMass.Text = "300";
            this.txtTmdMass.Validating += new System.ComponentModel.CancelEventHandler(this.txtValidating);
            // 
            // txtAssumedTmdToStructureMassRatio
            // 
            this.txtAssumedTmdToStructureMassRatio.Location = new System.Drawing.Point(185, 25);
            this.txtAssumedTmdToStructureMassRatio.Name = "txtAssumedTmdToStructureMassRatio";
            this.txtAssumedTmdToStructureMassRatio.Size = new System.Drawing.Size(119, 20);
            this.txtAssumedTmdToStructureMassRatio.TabIndex = 2;
            this.txtAssumedTmdToStructureMassRatio.Text = "5";
            this.txtAssumedTmdToStructureMassRatio.Validating += new System.ComponentModel.CancelEventHandler(this.txtValidating);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(7, 132);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(158, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Optimum TMD damping ratio [%]";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(7, 106);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "TMD frequency [Hz]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(7, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(165, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Optimum TMD frequency ratio [%]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 26);
            this.label6.TabIndex = 1;
            this.label6.Text = "Assumed ratio of TMD mass\r\nto structure modal mass [%]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(7, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "TMD mass [kg]";
            // 
            // tabResults
            // 
            this.tabResults.BackColor = System.Drawing.SystemColors.Control;
            this.tabResults.Controls.Add(this.lblMaxValue);
            this.tabResults.Controls.Add(this.groupBox6);
            this.tabResults.Controls.Add(this.groupBox5);
            this.tabResults.Controls.Add(this.chartResults);
            this.tabResults.Location = new System.Drawing.Point(4, 22);
            this.tabResults.Name = "tabResults";
            this.tabResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabResults.Size = new System.Drawing.Size(740, 589);
            this.tabResults.TabIndex = 1;
            this.tabResults.Text = "Results";
            // 
            // lblMaxValue
            // 
            this.lblMaxValue.AutoSize = true;
            this.lblMaxValue.Location = new System.Drawing.Point(9, 71);
            this.lblMaxValue.Name = "lblMaxValue";
            this.lblMaxValue.Size = new System.Drawing.Size(0, 13);
            this.lblMaxValue.TabIndex = 4;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.radioTMD);
            this.groupBox6.Controls.Add(this.radioStructureWithTMD);
            this.groupBox6.Controls.Add(this.radioStructureWithoutTMD);
            this.groupBox6.Location = new System.Drawing.Point(6, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(320, 52);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Chart for";
            // 
            // radioTMD
            // 
            this.radioTMD.AutoSize = true;
            this.radioTMD.Location = new System.Drawing.Point(267, 19);
            this.radioTMD.Name = "radioTMD";
            this.radioTMD.Size = new System.Drawing.Size(49, 17);
            this.radioTMD.TabIndex = 2;
            this.radioTMD.Text = "TMD";
            this.radioTMD.UseVisualStyleBackColor = true;
            this.radioTMD.CheckedChanged += new System.EventHandler(this.radioParameter_CheckedChanged);
            // 
            // radioStructureWithTMD
            // 
            this.radioStructureWithTMD.AutoSize = true;
            this.radioStructureWithTMD.Location = new System.Drawing.Point(144, 19);
            this.radioStructureWithTMD.Name = "radioStructureWithTMD";
            this.radioStructureWithTMD.Size = new System.Drawing.Size(117, 17);
            this.radioStructureWithTMD.TabIndex = 1;
            this.radioStructureWithTMD.Text = "Structure with TMD";
            this.radioStructureWithTMD.UseVisualStyleBackColor = true;
            this.radioStructureWithTMD.CheckedChanged += new System.EventHandler(this.radioParameter_CheckedChanged);
            // 
            // radioStructureWithoutTMD
            // 
            this.radioStructureWithoutTMD.AutoSize = true;
            this.radioStructureWithoutTMD.Checked = true;
            this.radioStructureWithoutTMD.Location = new System.Drawing.Point(6, 19);
            this.radioStructureWithoutTMD.Name = "radioStructureWithoutTMD";
            this.radioStructureWithoutTMD.Size = new System.Drawing.Size(132, 17);
            this.radioStructureWithoutTMD.TabIndex = 0;
            this.radioStructureWithoutTMD.TabStop = true;
            this.radioStructureWithoutTMD.Text = "Structure without TMD";
            this.radioStructureWithoutTMD.UseVisualStyleBackColor = true;
            this.radioStructureWithoutTMD.CheckedChanged += new System.EventHandler(this.radioParameter_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioDisplacement);
            this.groupBox5.Controls.Add(this.radioAcceleration);
            this.groupBox5.Location = new System.Drawing.Point(332, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(211, 52);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Result type";
            // 
            // radioDisplacement
            // 
            this.radioDisplacement.AutoSize = true;
            this.radioDisplacement.Location = new System.Drawing.Point(116, 19);
            this.radioDisplacement.Name = "radioDisplacement";
            this.radioDisplacement.Size = new System.Drawing.Size(89, 17);
            this.radioDisplacement.TabIndex = 1;
            this.radioDisplacement.Text = "Displacement";
            this.radioDisplacement.UseVisualStyleBackColor = true;
            this.radioDisplacement.CheckedChanged += new System.EventHandler(this.radioParameter_CheckedChanged);
            // 
            // radioAcceleration
            // 
            this.radioAcceleration.AutoSize = true;
            this.radioAcceleration.Checked = true;
            this.radioAcceleration.Location = new System.Drawing.Point(6, 19);
            this.radioAcceleration.Name = "radioAcceleration";
            this.radioAcceleration.Size = new System.Drawing.Size(84, 17);
            this.radioAcceleration.TabIndex = 0;
            this.radioAcceleration.TabStop = true;
            this.radioAcceleration.Text = "Acceleration";
            this.radioAcceleration.UseVisualStyleBackColor = true;
            this.radioAcceleration.CheckedChanged += new System.EventHandler(this.radioParameter_CheckedChanged);
            // 
            // chartResults
            // 
            chartArea1.AxisX.MinorGrid.Enabled = true;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot;
            chartArea1.AxisX.Title = "Force frequency [Hz]";
            chartArea1.AxisY.MinorGrid.Enabled = true;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot;
            chartArea1.Name = "ChartArea";
            this.chartResults.ChartAreas.Add(chartArea1);
            this.chartResults.Location = new System.Drawing.Point(6, 98);
            this.chartResults.Name = "chartResults";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "Serie";
            this.chartResults.Series.Add(series1);
            this.chartResults.Size = new System.Drawing.Size(728, 485);
            this.chartResults.TabIndex = 1;
            this.chartResults.Text = "-";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.statusProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 630);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(772, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 5;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = false;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(200, 17);
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusProgressBar
            // 
            this.statusProgressBar.Name = "statusProgressBar";
            this.statusProgressBar.Size = new System.Drawing.Size(550, 16);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 652);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "TMD Design";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabInputData.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabResults.ResumeLayout(false);
            this.tabResults.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartResults)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabInputData;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtTimeInterval;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtExcitationFrequencyInterval;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtExcitationForce;
        private System.Windows.Forms.Button btnRunCalculations;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chBoxIncludeStrDamping;
        private System.Windows.Forms.TextBox txtStrDampingRatio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStrNaturalFrequency;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStrModalMass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chBoxEnterOwnParameters;
        private System.Windows.Forms.Button btnCalculateTmdParameters;
        private System.Windows.Forms.TextBox txtOptimumTmdDapingRatio;
        private System.Windows.Forms.TextBox txtTmdFrequency;
        private System.Windows.Forms.TextBox txtOptimumTmdFrequency;
        private System.Windows.Forms.TextBox txtTmdMass;
        private System.Windows.Forms.TextBox txtAssumedTmdToStructureMassRatio;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabResults;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioDisplacement;
        private System.Windows.Forms.RadioButton radioAcceleration;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartResults;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton radioTMD;
        private System.Windows.Forms.RadioButton radioStructureWithTMD;
        private System.Windows.Forms.RadioButton radioStructureWithoutTMD;
        private System.Windows.Forms.Label lblMaxValue;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripProgressBar statusProgressBar;
        private System.Windows.Forms.Button btnCalculateExcitationFrequency;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtFinalFrequency;
        private System.Windows.Forms.TextBox txtStartFrequency;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton radioFiniteDifferenceMethod;
        private System.Windows.Forms.RadioButton radioNewmarkMethod;
        private System.Windows.Forms.CheckBox chBoxSaveResultsToExcel;
        private System.Windows.Forms.Button btnCancelCalculations;
    }
}

