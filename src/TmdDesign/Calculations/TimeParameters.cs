namespace TmdDesign.SimpleClasses
{
    public class TimeParameters
    {
        public double StartTime { get; private set; }
        public double DeltaTime { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="startTime">start time</param>
        /// <param name="deltaTime">time increment</param>
        public TimeParameters(double startTime, double deltaTime)
        {
            this.StartTime = startTime;
            this.DeltaTime = deltaTime;
        }
    }
}