namespace TmdDesign.SimpleClasses
{
    /// <summary>
    /// class with results from Newmart analysis for current excitation (steady state conditions)
    /// </summary>
    public class ResultsTMD
    {
        public double Omega { get; set; } //excitation frequency
        public double TmdA { get; set; }//TMD acceleration
        public double TmdU { get; set; }//TMD displacement
        public double StructureA { get; set; } //structure acceleration
        public double StructureU { get; set; } //structure displacement
    }
}