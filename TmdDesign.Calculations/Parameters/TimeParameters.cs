namespace TmdDesign.Calculations.Parameters
{
    public class TimeParameters
    {
        public double StartTime { get; private set; }
        public double DeltaTime { get; private set; }

        public TimeParameters(double startTime, double deltaTime)
        {
            this.StartTime = startTime;
            this.DeltaTime = deltaTime;
        }
    }
}