using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using TmdDesign.Calculations;
using TmdDesign.ExcitationForces;
using TmdDesign.SimpleClasses;
using TmdDesign.Excel;

namespace TmdDesign
{
    public interface IMainView
    {
        TmdParameters TmdParameters { get; set; }
        StructureParameters StructureParameters { get; }
        TimeParameters TimeParameters { get; }
        ForceParameters ForceParameters {get;}
        SolvingMethod SolvingMethod { get; }
        Results Results { set; }
        double MiCoefficient { get; } 
        bool SaveResultsToExcelFile { get; }

        string StatusText { set; }
        int Progress { set; }
    }
    
    public class MainPresenter
    {
        private readonly IMainView view;
        private BackgroundWorker bw;

        public MainPresenter(IMainView view)
        {
            this.view = view;
        }

        public void RunCalculations()
        {
            //getting data from view
            if (!(this.bw == null))
                return;
            var tmdParms = this.view.TmdParameters;
            var strParms = this.view.StructureParameters;
            var timeParms = this.view.TimeParameters;
            var forceParms = this.view.ForceParameters;
            var solvingMethod = this.view.SolvingMethod;
            var saveResultsToExcelFile = this.view.SaveResultsToExcelFile;

            var bwArg = new BackgroundWorkerArgument
            {
                ForceParms = forceParms,
                StrParms = strParms,
                TimeParms = timeParms,
                TmdParms = tmdParms,
                SolvingMethod = solvingMethod,
                SaveResultsToExcelFile = saveResultsToExcelFile
            };

            this.bw = new BackgroundWorker();
            this.bw.WorkerReportsProgress = true;
            this.bw.WorkerSupportsCancellation = true;
            this.bw.ProgressChanged += new ProgressChangedEventHandler(this.bw_ProgressChanged);
            this.bw.DoWork += new DoWorkEventHandler(this.bw_DoWork);
            this.bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bw_Completed);

            this.bw.RunWorkerAsync(bwArg);
        }

        public void CalculateTMDProperties()
        {
            var tmdCalcs = new TmdParametersCalculations();
            var strParms = this.view.StructureParameters;
            var mi = this.view.MiCoefficient;
            var tmdParms = tmdCalcs.CalculateAllParameters(strParms, mi);

            this.view.TmdParameters = tmdParms;//binding results to view
        }
        public void CancelCalculations()
        {
            if (this.bw != null)
            {
                this.bw.CancelAsync();
                this.view.StatusText = "Canceling calculations....";
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var bwArg = (BackgroundWorkerArgument)e.Argument;

            var tmdParms = bwArg.TmdParms;
            var strParms = bwArg.StrParms;
            var timeParms = bwArg.TimeParms;
            var forceParms = bwArg.ForceParms;
            var solvingMethod = bwArg.SolvingMethod;
            var saveResultsToExcelFile = bwArg.SaveResultsToExcelFile;


            //lists with results
            var resWithTMD = new List<ResultsTMD>();
            var resWithoutTMD = new List<ResultsSingleDOF>();

            //main calculation classes
            ISolver withTMDCalcs;
            if (solvingMethod == SolvingMethod.NewmarkMethod)
                withTMDCalcs = new NewmarkMethod(strParms, tmdParms, forceParms.ForceValue, timeParms, 0.1);
            else
                withTMDCalcs = new FiniteDifferenceMethod(strParms, tmdParms, forceParms.ForceValue, timeParms, 0.1);
            //NewmarkMethod withTMDCalcs = new NewmarkMethod(strParms, tmdParms, forceParms.ForceValue, timeParms, 0.01);
            var withoutTMDCalcs = new SingleDOFCalculations(strParms, forceParms);

            //calculations are being performed untill the force frequency is equal to 4x normal frequency
            var finalFrequency = forceParms.FinalFrequency;
            var currentFrequency = forceParms.StartFrequency; //current excitation force
            var currentIteration = 1;
            //calculate progress

            var maxNumberOfIterations = Convert.ToInt32((finalFrequency - currentFrequency) / forceParms.ExcitationFrequencyIntervalValue + 1);

            //savingDataToExcel
            var saveData = new SavingDataToExcel(withTMDCalcs);

            var progress = 0;
            this.bw.ReportProgress(progress, currentFrequency.ToString("F2"));
            Calculate(e, forceParms, saveResultsToExcelFile, resWithTMD, resWithoutTMD, withTMDCalcs, withoutTMDCalcs, finalFrequency, ref currentFrequency, ref currentIteration, maxNumberOfIterations, saveData, ref progress);

            var res = new Results
            {
                ResultsWithoutTMD = resWithoutTMD,
                ResultsWithTMD = resWithTMD
            };
            e.Result = res;
        }

        private void Calculate(DoWorkEventArgs e, ForceParameters forceParms, bool saveResultsToExcelFile, List<ResultsTMD> resWithTMD, List<ResultsSingleDOF> resWithoutTMD, ISolver withTMDCalcs, SingleDOFCalculations withoutTMDCalcs, double finalFrequency, ref double currentFrequency, ref int currentIteration, int maxNumberOfIterations, SavingDataToExcel saveData, ref int progress)
        {
            while (currentFrequency <= finalFrequency)
            {
                if (this.checkCancelCalculations())
                {
                    e.Cancel = true;
                    break;//cancel calculations
                }
                var tempResWithTMD = withTMDCalcs.Calculate(currentFrequency);
                resWithTMD.Add(tempResWithTMD);//add results to the list
                if (saveResultsToExcelFile)
                    saveData.SaveResultsToFile(currentFrequency);

                var tempResWithoutTMD = withoutTMDCalcs.Calculate(currentFrequency);
                resWithoutTMD.Add(tempResWithoutTMD);//add results to the list

                progress = Convert.ToInt32(Convert.ToDouble(currentIteration) / Convert.ToDouble(maxNumberOfIterations) * 100);

                currentFrequency += forceParms.ExcitationFrequencyIntervalValue;
                currentIteration++;
                this.bw.ReportProgress(progress, currentFrequency.ToString("F2"));
            }
        }

        private bool checkCancelCalculations()
        {
            var result = false;
            if (this.bw.CancellationPending)
            {
                result = true;
                this.bw.CancelAsync();
            }
            return result;
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!(e.UserState == null))
            {
                var s = (string)e.UserState;
                this.view.StatusText = string.Format("Current frequency: {0} Hz", s);
            }
            this.view.Progress = e.ProgressPercentage;
        }
        private void bw_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                var res = (Results)e.Result;
                this.view.Results = res;
                this.view.StatusText = "Calculations completed";
                this.view.Progress = 0;
            }
            else
            {
                this.view.Progress = 0;
                this.view.StatusText = "Calculations have been canceled";
            }

            this.bw.Dispose();
            this.bw = null;
        }
    }
}
