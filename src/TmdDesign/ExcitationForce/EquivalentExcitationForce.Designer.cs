namespace TmdDesign
{
    partial class FormExcitationForce
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExcitationForce));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDisplacement = new System.Windows.Forms.TextBox();
            this.txtFrequency = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDampingRatio = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtModalMass = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblEquivalenForce = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.txtNaturalFrequency = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblDynamicStiffness = new System.Windows.Forms.Label();
            this.btnSaveAndClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Displacement due to \r\nexcitation force [m]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Excitation frequency [Hz]";
            // 
            // txtDisplacement
            // 
            this.txtDisplacement.Location = new System.Drawing.Point(182, 28);
            this.txtDisplacement.Name = "txtDisplacement";
            this.txtDisplacement.Size = new System.Drawing.Size(100, 20);
            this.txtDisplacement.TabIndex = 1;
            this.txtDisplacement.Text = "0.04";
            this.txtDisplacement.TextChanged += new System.EventHandler(this.txtDisplacement_TextChanged);
            this.txtDisplacement.Validating += new System.ComponentModel.CancelEventHandler(this.txtValidating);
            // 
            // txtFrequency
            // 
            this.txtFrequency.Location = new System.Drawing.Point(182, 59);
            this.txtFrequency.Name = "txtFrequency";
            this.txtFrequency.Size = new System.Drawing.Size(100, 20);
            this.txtFrequency.TabIndex = 2;
            this.txtFrequency.Text = "1.5";
            this.txtFrequency.TextChanged += new System.EventHandler(this.txtDisplacement_TextChanged);
            this.txtFrequency.Validating += new System.ComponentModel.CancelEventHandler(this.txtValidating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Damping ratio [%]";
            // 
            // txtDampingRatio
            // 
            this.txtDampingRatio.Location = new System.Drawing.Point(182, 114);
            this.txtDampingRatio.Name = "txtDampingRatio";
            this.txtDampingRatio.Size = new System.Drawing.Size(100, 20);
            this.txtDampingRatio.TabIndex = 4;
            this.txtDampingRatio.Text = "0.8";
            this.txtDampingRatio.TextChanged += new System.EventHandler(this.txtDisplacement_TextChanged);
            this.txtDampingRatio.Validating += new System.ComponentModel.CancelEventHandler(this.txtValidating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Modal mass [kg]";
            // 
            // txtModalMass
            // 
            this.txtModalMass.Location = new System.Drawing.Point(182, 141);
            this.txtModalMass.Name = "txtModalMass";
            this.txtModalMass.Size = new System.Drawing.Size(100, 20);
            this.txtModalMass.TabIndex = 5;
            this.txtModalMass.Text = "2000000";
            this.txtModalMass.TextChanged += new System.EventHandler(this.txtDisplacement_TextChanged);
            this.txtModalMass.Validating += new System.ComponentModel.CancelEventHandler(this.txtValidating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Equivalen excitation force [N]\r\n";
            // 
            // lblEquivalenForce
            // 
            this.lblEquivalenForce.AutoSize = true;
            this.lblEquivalenForce.Location = new System.Drawing.Point(179, 202);
            this.lblEquivalenForce.Name = "lblEquivalenForce";
            this.lblEquivalenForce.Size = new System.Drawing.Size(0, 13);
            this.lblEquivalenForce.TabIndex = 9;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Natural Frequency [Hz]";
            // 
            // txtNaturalFrequency
            // 
            this.txtNaturalFrequency.Location = new System.Drawing.Point(182, 88);
            this.txtNaturalFrequency.Name = "txtNaturalFrequency";
            this.txtNaturalFrequency.Size = new System.Drawing.Size(100, 20);
            this.txtNaturalFrequency.TabIndex = 3;
            this.txtNaturalFrequency.Text = "1.5";
            this.txtNaturalFrequency.TextChanged += new System.EventHandler(this.txtDisplacement_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Dynamic Stiffness [N/m]";
            // 
            // lblDynamicStiffness
            // 
            this.lblDynamicStiffness.AutoSize = true;
            this.lblDynamicStiffness.Location = new System.Drawing.Point(179, 178);
            this.lblDynamicStiffness.Name = "lblDynamicStiffness";
            this.lblDynamicStiffness.Size = new System.Drawing.Size(0, 13);
            this.lblDynamicStiffness.TabIndex = 13;
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(104, 223);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(95, 23);
            this.btnSaveAndClose.TabIndex = 6;
            this.btnSaveAndClose.Text = "Save && Close";
            this.btnSaveAndClose.UseVisualStyleBackColor = true;
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // FormExcitationForce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 258);
            this.Controls.Add(this.btnSaveAndClose);
            this.Controls.Add(this.lblDynamicStiffness);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtNaturalFrequency);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblEquivalenForce);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtModalMass);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDampingRatio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFrequency);
            this.Controls.Add(this.txtDisplacement);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExcitationForce";
            this.Text = "Excitation Force";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDisplacement;
        private System.Windows.Forms.TextBox txtFrequency;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDampingRatio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtModalMass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblEquivalenForce;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TextBox txtNaturalFrequency;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDynamicStiffness;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSaveAndClose;
    }
}