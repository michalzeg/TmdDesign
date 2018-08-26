using System.Collections.Generic;
using TmdDesign.Matrix;

namespace TmdDesign.SimpleClasses
{
    public interface ISolverDataProvider
    {
        IEnumerable<Vector> Acceleration { get; }
        IEnumerable<Vector> Displacement { get; }
        IEnumerable<Vector> Force { get; }
        IEnumerable<double> Time { get; }
        IEnumerable<Vector> Velocity { get; }
    }
}