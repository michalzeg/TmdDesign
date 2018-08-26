using System.Collections.Generic;

namespace TmdDesign.SimpleClasses
{
    public class Results
    {
        public List<ResultsTMD> ResultsWithTMD { get; set; }
        public List<ResultsSingleDOF> ResultsWithoutTMD { get; set; }
    }
}