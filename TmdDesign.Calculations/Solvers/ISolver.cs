using System.Collections.Generic;
using TmdDesign.Calculations.Results;

namespace TmdDesign.Calculations.Solvers
{
    public interface ISolver : ISolverDataProvider
    {
        ResultsTMD Calculate(double excitationFrequency);
    }
}