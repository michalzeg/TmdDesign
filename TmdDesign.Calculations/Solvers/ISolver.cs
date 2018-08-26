using System.Collections.Generic;
using TmdDesign.Matrix;

namespace TmdDesign.SimpleClasses
{
    public interface ISolver : ISolverDataProvider
    {
        ResultsTMD Calculate(double excitationFrequency);
    }
}