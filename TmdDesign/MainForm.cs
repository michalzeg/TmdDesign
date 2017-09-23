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
            //disabling tab results
            (this.tabResults as Control).Enabled = false;
            this.presenter = new MainPresenter(this);
        }

        //IView properties
        public TmdParameters TmdParameters
        {
            get
            {

                var m = double.Parse(this.txtTmdMass.Text);
                var mi = double.Parse(this.txtAssumedTmdToStructureMassRatio.Text)/100;
                var omegaD = double.Parse(this.txtTmdFrequency.Text);
                var deltaOpt = double.Parse(this.txtOptimumTmdFrequency.Text)/100;
                var ksi = double.Parse(this.txtOptimumTmdDapingRatio.Text)/100;
                var tmdParm = new TmdParameters(m, mi, omegaD, deltaOpt, ksi);
                return tmdParm;

            }
            set
            {
                var tmdParm = value;
                this.txtTmdMass.Text = tmdParm.M.ToString("F2");
                this.txtAssumedTmdToStructureMassRatio.Text = (tmdParm.Mi*100).ToString("F2");
                this.txtTmdFrequency.Text = tmdParm.OmegaD.ToString("F2");
                this.txtOptimumTmdFrequency.Text = (tmdParm.DeltaOpt*100).ToString("F2");
                this.txtOptimumTmdDapingRatio.Text = (tmdParm.Ksi*100).ToString("F2");

            }
        }
        public StructureParameters StructureParameters
        {
            get
            {
                double m;//mass
                double omega;//frequency
                double ksi; //damping ratio
                bool ignoreDamping;//

                m = double.Parse(this.txtStrModalMass.Text);
                omega = double.Parse(this.txtStrNaturalFrequency.Text);
                ksi = double.Parse(this.txtStrDampingRatio.Text)/100;
                ignoreDamping = this.chBoxIncludeStrDamping.Checked;

                var strParams = new StructureParameters(m, omega, ksi, ignoreDamping);
                return strParams;
            }
        }
        public TimeParameters TimeParameters 
        {
            get
            {
                double dt;//time
                dt = double.Parse(this.txtTimeInterval.Text);
                var timeParm = new TimeParameters(0, dt);
                return timeParm;
            }
                
        }
        public ForceParameters ForceParameters
        {
            get
            {
                var F = double.Parse(this.txtExcitationForce.Text);
                var dOmega = double.Parse(this.txtExcitationFrequencyInterval.Text);
                var startFrequency = double.Parse(this.txtStartFrequency.Text);
                var finalFrequency = double.Parse(this.txtFinalFrequency.Text);

                var forceParm = new ForceParameters
                {
                    ForceValue = F,
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
                return double.Parse(this.txtAssumedTmdToStructureMassRatio.Text)/100;
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
                /*if (this.statusProgressBar.InvokeRequired)
                    this.progressBarMain.Invoke(new MethodInvoker(delegate { this.progressBarMain.Value = value; }));
                else
                    this.progressBarMain.Value = value;*/
                /*if (this.statusStrip.InvokeRequired)
                    this.statusStrip.Invoke(new MethodInvoker(delegate { (this.statusStrip.Items["statusProgressBar"] = value; }));
                else
                    this.statusStrip.Items[1].Text = value;*/
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

                //if (this.statusStrip.InvokeRequired)
                //    this.statusStrip.Invoke(new MethodInvoker(delegate { this.statusStrip.Items[1].Text = value; }));
                //else
                //    this.statusStrip.Items[1].Text = value;
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
                SolvingMethod sm;
                if (radioNewmarkMethod.Checked)
                    sm = SolvingMethod.NewmarkMethod;
                else
                    sm = SolvingMethod.FiniteDifferenceMethod;
                return sm;
            }
                
        }
        //properties needed for equivalen form
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
        
        
        //validation of text boxes
        private void txtValidating(object sender, CancelEventArgs e)
        {
            double d;
            var t = sender as TextBox;
            if (!double.TryParse(t.Text,out d) || d <= 0)
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
        //View methods
        //sets results for given parameter in the given chart and labels
        private void updateChart(Chart chart,List<double> x, List<double> y)
        {
            //chart - chart to which the results are set
            chart.Series["Serie"].Points.Clear();
            for (int i =0;i<=x.Count -1;i++)
            {
                chart.Series["Serie"].Points.AddXY(x[i], y[i]);
                //chart.Series["WithoutTMD"].Points.AddXY(omega[i], pWithoutTMD[i]);
            }


        }
        private void updateResults() //void updates all charts with results
        {

            (this.tabResults as Control).Enabled = true; //enable tab with results

            List<double> x; //list with x on the chart
            List<double> y; //list with x on the chart


            //check result type: acceleration or displacement
            x = this.results.ResultsWithTMD.ConvertAll(e => e.Omega);
            string parameterName;
            if (radioAcceleration.Checked)
            {
                parameterName = "acceleration";
                if (radioStructureWithoutTMD .Checked)
                {
                    y = this.results.ResultsWithoutTMD.ConvertAll(e => e.StructureA);
                }
                else if (radioStructureWithTMD.Checked)
                {
                    y = this.results.ResultsWithTMD.ConvertAll(e => e.StructureA);
                }
                else
                {
                    y = this.results.ResultsWithTMD.ConvertAll(e => e.TmdA);
                }
                
                //acceleration result type has been chosen
                /*omega = this.results.ResultsWithTMD.ConvertAll(x => x.Omega);
                pStructureWithoutTMD = this.results.ResultsWithoutTMD.ConvertAll(x => x.StructureA);
                pStructureWithTMD = this.results.ResultsWithTMD.ConvertAll(x => x.StructureA);
                pTMD = this.results.ResultsWithTMD.ConvertAll(x => x.TmdA);*/
            }
            else
            {
                parameterName = "displacement";
                //displacement result type has been chosen
                if (radioStructureWithoutTMD.Checked)
                {
                    y = this.results.ResultsWithoutTMD.ConvertAll(e => e.StructureU);
                }
                else if (radioStructureWithTMD.Checked)
                {
                    y = this.results.ResultsWithTMD.ConvertAll(e => e.StructureU);
                }
                else
                {
                    y = this.results.ResultsWithTMD.ConvertAll(e => e.TmdU);
                }
                
            }

            //updating charts with results
            this.updateChart(this.chartResults, x, y);

            var max = y.Max().ToString();
            this.lblMaxValue.Text = string.Format("Max value of the {0}: {1}", parameterName, max);
            

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
