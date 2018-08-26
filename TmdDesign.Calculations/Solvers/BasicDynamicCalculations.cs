using System;

namespace TmdDesign.Calculations.Solvers
{
    public static class BasicDynamicCalculations
    {
        public static double DynamicFactor(double excitationFrequency, double naturalFrequency, double dampingRatio)
        {
            double rd = 1 / (Math.Sqrt(Math.Pow(1 - Math.Pow(excitationFrequency / naturalFrequency, 2), 2) + 2 * dampingRatio * Math.Pow(excitationFrequency / naturalFrequency, 2)));
            return rd;
        }

        public static double StaticDisplacement(double force, double stiffness)
        {
            double u0 = force / stiffness;
            return u0;
        }

        public static double Acceleration(double excitationFrequency, double staticDisplacement, double dynamicFactor)
        {
            return dynamicFactor * staticDisplacement * Math.Pow(Math.PI * 2 * excitationFrequency, 2);
        }

        public static double DynamicDisplacement(double staticDisplacement, double dynamicDisplacement)
        {
            return dynamicDisplacement * staticDisplacement;
        }

        public static double DynamicStiffness(double modalMass, double naturalFrequency)
        {
            double ds = Math.Pow((2 * Math.PI * naturalFrequency), 2) * modalMass;
            return ds;
        }

        public static double EquivalentDynamicForce(double displacement, double stiffness, double dynamicFactor)
        {
            double p = (displacement * stiffness) / dynamicFactor;
            return p;
        }
    }
}