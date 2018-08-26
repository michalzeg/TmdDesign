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
    public class MainPresenter
    {
        private readonly IMainView view;
        private BackgroundWorker backgroundWorker;

        public MainPresenter(IMainView view)
        {
            this.view = view;
        }

        public void RunCalculations()
        {
            if (!(this.backgroundWorker == null))
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

            this.backgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            this.backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(this.bw_ProgressChanged);
            this.backgroundWorker.DoWork += new DoWorkEventHandler(this.bw_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bw_Completed);

            this.backgroundWorker.RunWorkerAsync(bwArg);
        }

        public void CalculateTMDProperties()
        {
            var tmdCalculations = new TmdParametersCalculations();
            var strParameters = this.view.StructureParameters;
            var miCoefficient = this.view.MiCoefficient;
            var tmdParameters = tmdCalculations.CalculateAllParameters(strParameters, miCoefficient);

            this.view.TmdParameters = tmdParameters;
        }

        public void CancelCalculations()
        {
            if (this.backgroundWorker != null)
            {
                this.backgroundWorker.CancelAsync();
                this.view.StatusText = "Canceling calculations....";
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var backgroundWorkerAgrument = (BackgroundWorkerArgument)e.Argument;

            var tmdParms = backgroundWorkerAgrument.TmdParms;
            var strParms = backgroundWorkerAgrument.StrParms;
            var timeParms = backgroundWorkerAgrument.TimeParms;
            var forceParms = backgroundWorkerAgrument.ForceParms;
            var solvingMethod = backgroundWorkerAgrument.SolvingMethod;
            var saveResultsToExcelFile = backgroundWorkerAgrument.SaveResultsToExcelFile;

            var resWithTMD = new List<ResultsTMD>();
            var resWithoutTMD = new List<ResultsSingleDOF>();
            var withTMDCalcs = GetSolverMethod(tmdParms, strParms, timeParms, forceParms, solvingMethod);

            var withoutTMDCalcs = new SingleDOFCalculations(strParms, forceParms);

            var finalFrequency = forceParms.FinalFrequency;
            var currentFrequency = forceParms.StartFrequency;
            var currentIteration = 1;

            var maxNumberOfIterations = Convert.ToInt32((finalFrequency - currentFrequency) / forceParms.ExcitationFrequencyIntervalValue + 1);

            var saveData = new ExcelExporter(withTMDCalcs);

            var progress = 0;
            this.backgroundWorker.ReportProgress(progress, currentFrequency.ToString("F2"));
            Calculate(e, forceParms, saveResultsToExcelFile, resWithTMD, resWithoutTMD, withTMDCalcs, withoutTMDCalcs, finalFrequency, ref currentFrequency, ref currentIteration, maxNumberOfIterations, saveData, ref progress);

            var res = new Results
            {
                ResultsWithoutTMD = resWithoutTMD,
                ResultsWithTMD = resWithTMD
            };
            e.Result = res;
        }

        private static ISolver GetSolverMethod(TmdParameters tmdParms, StructureParameters strParms, TimeParameters timeParms, ForceParameters forceParms, SolvingMethod solvingMethod)
        {
            ISolver withTMDCalcs;
            if (solvingMethod == SolvingMethod.NewmarkMethod)
                withTMDCalcs = new NewmarkMethod(strParms, tmdParms, forceParms.ForceValue, timeParms, 0.1);
            else
                withTMDCalcs = new FiniteDifferenceMethod(strParms, tmdParms, forceParms.ForceValue, timeParms, 0.1);
            return withTMDCalcs;
        }

        private void Calculate(DoWorkEventArgs e, ForceParameters forceParms, bool saveResultsToExcelFile, List<ResultsTMD> resWithTMD, List<ResultsSingleDOF> resWithoutTMD, ISolver withTMDCalcs, SingleDOFCalculations withoutTMDCalcs, double finalFrequency, ref double currentFrequency, ref int currentIteration, int maxNumberOfIterations, ExcelExporter saveData, ref int progress)
        {
            while (currentFrequency <= finalFrequency)
            {
                if (this.checkCancelCalculations())
                {
                    e.Cancel = true;
                    break;
                }
                var tempResWithTMD = withTMDCalcs.Calculate(currentFrequency);
                resWithTMD.Add(tempResWithTMD);
                if (saveResultsToExcelFile)
                    saveData.SaveResultsToFile(currentFrequency);

                var tempResWithoutTMD = withoutTMDCalcs.Calculate(currentFrequency);
                resWithoutTMD.Add(tempResWithoutTMD);

                progress = Convert.ToInt32(Convert.ToDouble(currentIteration) / Convert.ToDouble(maxNumberOfIterations) * 100);

                currentFrequency += forceParms.ExcitationFrequencyIntervalValue;
                currentIteration++;
                this.backgroundWorker.ReportProgress(progress, currentFrequency.ToString("F2"));
            }
        }

        private bool checkCancelCalculations()
        {
            var result = false;
            if (this.backgroundWorker.CancellationPending)
            {
                result = true;
                this.backgroundWorker.CancelAsync();
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

            this.backgroundWorker.Dispose();
            this.backgroundWorker = null;
        }
    }
}