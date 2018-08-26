using System.Collections.Generic;

namespace TmdDesign.Calculations.Results
{
    public class Results
    {
        public List<ResultsTMD> ResultsWithTMD { get; set; }
        public List<ResultsSingleDOF> ResultsWithoutTMD { get; set; }
    }
}