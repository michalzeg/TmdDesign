using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TmdDesign.SimpleClasses;

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

        //Iview methods
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
            set { this.lblDynamicStiffness.Text = value.ToString(); }
        }
        public double EquivalentExcitationForce
        {
            set { this.lblEquivalenForce.Text = value.ToString(); }
        }

        private void fillDampingRatioAndModalMass()
        {
            //methods gets the damping ratio and modal mass from main form
            this.txtDampingRatio.Text = this.mainForm.DampingRatio;
            this.txtModalMass.Text = this.mainForm.ModalMass;
        }

        private void txtValidating(object sender, CancelEventArgs e)
        {
            double d;
            TextBox t = sender as TextBox;
            if (!double.TryParse(t.Text, out d) || d <= 0)
            {
                e.Cancel = true;

                t.Select(0, t.Text.Length);
                this.errorProvider.SetError(t, "Enter positive numbers only.");
            }
            else
            {
                this.errorProvider.SetError(t, "");
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
