using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmdDesign.ExcitationForces
{
    public delegate double ExcitationFunction(double x);

    public static class ExcitationFunctions
    {
        public static double Sin(double x)
        {
            return Math.Sin(x);
        }
    }
}