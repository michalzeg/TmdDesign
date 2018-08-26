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
using System.Windows.Forms.DataVisualization.Charting;

namespace TmdDesign
{
    public partial class MainForm : Form, IMainView
    {
        private readonly MainPresenter presenter;
        private Results results;

        public MainForm()
        {
            InitializeComponent();

            (this.tabResults as Control).Enabled = false;
            this.presenter = new MainPresenter(this);
        }

        public TmdParameters TmdParameters
        {
            get
            {
                var mass = double.Parse(this.txtTmdMass.Text);
                var mi = double.Parse(this.txtAssumedTmdToStructureMassRatio.Text) / 100;
                var omegaD = double.Parse(this.txtTmdFrequency.Text);
                var deltaOpt = double.Parse(this.txtOptimumTmdFrequency.Text) / 100;
                var ksi = double.Parse(this.txtOptimumTmdDapingRatio.Text) / 100;
                var tmdParm = new TmdParameters(mass, mi, omegaD, deltaOpt, ksi);
                return tmdParm;
            }
            set
            {
                var tmdParm = value;
                this.txtTmdMass.Text = tmdParm.M.ToString("F2");
                this.txtAssumedTmdToStructureMassRatio.Text = (tmdParm.Mi * 100).ToString("F2");
                this.txtTmdFrequency.Text = tmdParm.OmegaD.ToString("F2");
                this.txtOptimumTmdFrequency.Text = (tmdParm.DeltaOpt * 100).ToString("F2");
                this.txtOptimumTmdDapingRatio.Text = (tmdParm.Ksi * 100).ToString("F2");
            }
        }

        public StructureParameters StructureParameters
        {
            get
            {
                var mass = double.Parse(this.txtStrModalMass.Text);
                var omega = double.Parse(this.txtStrNaturalFrequency.Text);
                var ksi = double.Parse(this.txtStrDampingRatio.Text) / 100;
                var ignoreDamping = this.chBoxIncludeStrDamping.Checked;

                var strParams = new StructureParameters(mass, omega, ksi, ignoreDamping);
                return strParams;
            }
        }

        public TimeParameters TimeParameters
        {
            get
            {
                var deltaTime = double.Parse(this.txtTimeInterval.Text);
                var timeParm = new TimeParameters(0, deltaTime);
                return timeParm;
            }
        }

        public ForceParameters ForceParameters
        {
            get
            {
                var force = double.Parse(this.txtExcitationForce.Text);
                var dOmega = double.Parse(this.txtExcitationFrequencyInterval.Text);
                var startFrequency = double.Parse(this.txtStartFrequency.Text);
                var finalFrequency = double.Parse(this.txtFinalFrequency.Text);

                var forceParm = new ForceParameters
                {
                    ForceValue = force,
                    ExcitationFrequencyIntervalValue = dOmega,
                    StartFrequency = startFrequency,
                    FinalFrequency = finalFrequency
                };
                return forceParm;
            }
        }

        public bool SaveResultsToExcelFile
        {
            get
            {
                return this.chBoxSaveResultsToExcel.Checked;
            }
        }

        public double MiCoefficient
        {
            get
            {
                return double.Parse(this.txtAssumedTmdToStructureMassRatio.Text) / 100;
            }
        }

        public Results Results
        {
            set
            {
                this.results = value;
                this.updateResults();
            }
        }

        public int Progress
        {
            set
            {
                if (this.statusProgressBar.GetCurrentParent().InvokeRequired)
                {
                    this.statusProgressBar.GetCurrentParent().Invoke(new MethodInvoker(delegate { this.statusProgressBar.Value = value; }));
                }
                else
                {
                    this.statusProgressBar.Value = value;
                }
            }
        }

        public string StatusText
        {
            set
            {
                if (this.statusLabel.GetCurrentParent().InvokeRequired)
                {
                    this.statusLabel.GetCurrentParent().Invoke(new MethodInvoker(delegate { this.statusLabel.Text = value; }));
                }
                else
                {
                    this.statusLabel.Text = value;
                }
            }
        }

        public SolvingMethod SolvingMethod
        {
            get
            {
                SolvingMethod solverMethod = radioNewmarkMethod.Checked
                    ? SolvingMethod.NewmarkMethod
                    : SolvingMethod.FiniteDifferenceMethod;

                return solverMethod;
            }
        }

        public string DampingRatio
        {
            get { return this.txtStrDampingRatio.Text; }
        }

        public string ModalMass
        {
            get { return this.txtStrModalMass.Text; }
        }

        public string EquivalentDynamicForce
        {
            set { this.txtExcitationForce.Text = value; }
        }

        private void txtValidating(object sender, CancelEventArgs e)
        {
            var timebox = sender as TextBox;
            if (!double.TryParse(timebox.Text, out double value) || value <= 0)
            {
                e.Cancel = true;

                timebox.Select(0, timebox.Text.Length);
                this.errorProvider.SetError(timebox, "Enter positive numbers only.");
            }
            else
            {
                this.errorProvider.SetError(timebox, "");
            }
        }

        private void chBoxEnterOwnParameters_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chBoxEnterOwnParameters.Checked)
            {
                this.txtTmdMass.ReadOnly = false;
                this.txtTmdFrequency.ReadOnly = false;
                this.txtOptimumTmdDapingRatio.ReadOnly = false;
                this.txtOptimumTmdFrequency.ReadOnly = false;

                this.txtTmdMass.TabStop = true;
                this.txtTmdFrequency.TabStop = true;
                this.txtOptimumTmdDapingRatio.TabStop = true;
                this.txtOptimumTmdFrequency.TabStop = true;

                this.btnCalculateTmdParameters.Enabled = false;
            }
            else
            {
                this.txtTmdMass.ReadOnly = true;
                this.txtTmdFrequency.ReadOnly = true;
                this.txtOptimumTmdDapingRatio.ReadOnly = true;
                this.txtOptimumTmdFrequency.ReadOnly = true;

                this.txtTmdMass.TabStop = false;
                this.txtTmdFrequency.TabStop = false;
                this.txtOptimumTmdDapingRatio.TabStop = false;
                this.txtOptimumTmdFrequency.TabStop = false;

                this.btnCalculateTmdParameters.Enabled = true;
            }
        }

        private void btnRunCalculations_Click(object sender, EventArgs e)
        {
            this.presenter.RunCalculations();
            this.btnCancelCalculations.Enabled = true;
        }

        private void updateChart(Chart chart, List<double> x, List<double> y)
        {
            chart.Series["Serie"].Points.Clear();
            for (int i = 0; i <= x.Count - 1; i++)
            {
                chart.Series["Serie"].Points.AddXY(x[i], y[i]);
            }
        }

        private void updateResults()
        {
            (this.tabResults as Control).Enabled = true;

            List<double> xValues;
            List<double> yValues;

            xValues = this.results.ResultsWithTMD.ConvertAll(e => e.Omega);
            string parameterName;
            if (radioAcceleration.Checked)
            {
                GetAcceleration(out yValues, out parameterName);
            }
            else
            {
                GetDisplacement(out yValues, out parameterName);
            }

            this.updateChart(this.chartResults, xValues, yValues);

            var max = yValues.Max().ToString();
            this.lblMaxValue.Text = string.Format("Max value of the {0}: {1}", parameterName, max);
        }

        private void GetDisplacement(out List<double> yValues, out string parameterName)
        {
            parameterName = "displacement";
            if (radioStructureWithoutTMD.Checked)
            {
                yValues = this.results.ResultsWithoutTMD.ConvertAll(e => e.StructureU);
            }
            else if (radioStructureWithTMD.Checked)
            {
                yValues = this.results.ResultsWithTMD.ConvertAll(e => e.StructureU);
            }
            else
            {
                yValues = this.results.ResultsWithTMD.ConvertAll(e => e.TmdU);
            }
        }

        private void GetAcceleration(out List<double> yValues, out string parameterName)
        {
            parameterName = "acceleration";
            if (radioStructureWithoutTMD.Checked)
            {
                yValues = this.results.ResultsWithoutTMD.ConvertAll(e => e.StructureA);
            }
            else if (radioStructureWithTMD.Checked)
            {
                yValues = this.results.ResultsWithTMD.ConvertAll(e => e.StructureA);
            }
            else
            {
                yValues = this.results.ResultsWithTMD.ConvertAll(e => e.TmdA);
            }
        }

        private void clearResults()
        {
            (this.tabResults as Control).Enabled = false;
            this.chartResults.Series["Serie"].Points.Clear();
        }

        private void radioParameter_CheckedChanged(object sender, EventArgs e)
        {
            this.updateResults();
        }

        private void btnCalculateExcitationFrequency_Click(object sender, EventArgs e)
        {
            using (FormExcitationForce form = new FormExcitationForce(this))
            {
                form.ShowDialog();
            }
        }

        private void btnCalculateTmdParameters_Click(object sender, EventArgs e)
        {
            this.presenter.CalculateTMDProperties();
        }

        private void btnCancelCalculations_Click(object sender, EventArgs e)
        {
            this.presenter.CancelCalculations();
        }
    }
}