using System.Collections.Generic;
using TmdDesign.Matrix;

namespace TmdDesign.SimpleClasses
{
    public interface ISolver
    {
        IEnumerable<Vector> Displacement { get; }
        IEnumerable<Vector> Velocity { get; }
        IEnumerable<Vector> Acceleration { get; }
        IEnumerable<double> Time { get; }
        IEnumerable<Vector> Force { get; }

        ResultsTMD Calculate(double excitationFrequency);
    }
}