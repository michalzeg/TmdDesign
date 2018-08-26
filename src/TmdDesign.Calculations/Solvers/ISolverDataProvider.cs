using System.Collections.Generic;
using TmdDesign.Calculations.Matrix;

namespace TmdDesign.Calculations.Solvers
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