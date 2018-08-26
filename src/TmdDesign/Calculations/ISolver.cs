using System.Collections.Generic;
using TmdDesign.Matrix;

namespace TmdDesign.SimpleClasses
{
    public interface ISolver
    {
        List<Vector> U { get; } //list of vectors with displacement
        List<Vector> V { get; } //list of vectors with velocity
        List<Vector> A { get; } //list of vectors with acceleration
        List<double> Time { get; } //list with time
        List<Vector> P { get; } //list with load

        ResultsTMD Calculate(double excitationFrequency);
    }
}