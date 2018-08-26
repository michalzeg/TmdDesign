﻿namespace TmdDesign.SimpleClasses
{
    /// <summary>
    /// class which wraps data on ExcitationForce
    /// </summary>
    public class ForceParameters
    {
        public double ForceValue { get; set; }
        public double ExcitationFrequencyIntervalValue { get; set; }
        public double StartFrequency { get; set; }
        public double FinalFrequency { get; set; }
    }
}