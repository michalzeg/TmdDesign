using TmdDesign.SimpleClasses;

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
}
