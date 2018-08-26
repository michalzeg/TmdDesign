using TmdDesign.Calculations.Parameters;
using TmdDesign.Calculations.Solvers;

namespace TmdDesign.App.BackgroundWorker
{
    public class BackgroundWorkerArgument
    {
        public TmdParameters TmdParms { get; set; }
        public StructureParameters StrParms { get; set; }
        public TimeParameters TimeParms { get; set; }
        public ForceParameters ForceParms { get; set; }
        public SolvingMethod SolvingMethod { get; set; }
        public bool SaveResultsToExcelFile { get; set; }
    }
}