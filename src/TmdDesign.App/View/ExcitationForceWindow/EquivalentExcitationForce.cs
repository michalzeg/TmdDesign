using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TmdDesign.Calculations.Calculations;

namespace TmdDesign
{
    public partial class FormExcitationForce : Form, IExcitationForceView
    {
        private MainForm mainForm;
        private ExcitationForcePresenter presenter;

        public FormExcitationForce()
        {
            InitializeComponent();
        }

        public FormExcitationForce(MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.presenter = new ExcitationForcePresenter(this);
            InitializeComponent();

            this.fillDampingRatioAndModalMass();
            this.txtDisplacement_TextChanged(this, null);
        }

        public ExcitationForceInputData InputData
        {
            get
            {
                ExcitationForceInputData inputData = new ExcitationForceInputData();
                inputData.DampingRatio = double.Parse(this.txtDampingRatio.Text) / 100;
                inputData.DynamicDisplacement = double.Parse(this.txtDisplacement.Text);
                inputData.ExcitationFrequency = double.Parse(this.txtFrequency.Text);
                inputData.ModalMass = double.Parse(this.txtModalMass.Text);
                inputData.NaturalFrequency = double.Parse(this.txtNaturalFrequency.Text);
                return inputData;
            }
        }

        public double DynamicStiffness
        {
            set { this.lblDynamicStiffness.Text = value.ToString("0.##"); }
        }

        public double EquivalentExcitationForce
        {
            set { this.lblEquivalenForce.Text = value.ToString("0.##"); }
        }

        private void fillDampingRatioAndModalMass()
        {
            this.txtDampingRatio.Text = this.mainForm.DampingRatio;
            this.txtModalMass.Text = this.mainForm.ModalMass;
        }

        private void txtValidating(object sender, CancelEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (!double.TryParse(textbox.Text, out double value) || value <= 0)
            {
                e.Cancel = true;

                textbox.Select(0, textbox.Text.Length);
                this.errorProvider.SetError(textbox, "Enter positive numbers only.");
            }
            else
            {
                this.errorProvider.SetError(textbox, "");
            }
        }

        private void txtDisplacement_TextChanged(object sender, EventArgs e)
        {
            this.presenter.Calculate();
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            this.mainForm.EquivalentDynamicForce = this.lblEquivalenForce.Text;
            this.Close();
        }
    }
}