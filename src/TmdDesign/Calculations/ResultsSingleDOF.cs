namespace TmdDesign.SimpleClasses
{
    /// <summary>
    /// results for SDF element (without TMD)
    /// </summary>
    public class ResultsSingleDOF
    {
        public double Omega { get; set; } //excitation frequency
        public double StructureA { get; set; } //structure acceleration
        public double StructureU { get; set; } //structure displacement
    }
}