using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmdDesign.FindMax
{
    public class MaxValueFinder
    {
        private List<double> maxValueList = new List<double>();

        private int numberOfStoredExtermes;
        private double epsilon;

        private bool findPositiveExterme;

        public double SteadyStateValue { get; private set; } = double.NaN;

        public MaxValueFinder(int maxNumberOfExtremes, double epsilon)
        {
            this.numberOfStoredExtermes = maxNumberOfExtremes;
            this.epsilon = epsilon;
            this.findPositiveExterme = true;
        }

        public bool FindMaxAcceleration(double vi, double vi1)
        {
            bool result = false;
            if (this.findPositiveExterme && vi1 < vi)
            {
                result = this.checkSteadyStateConditions(Math.Abs(vi));
                this.findPositiveExterme = false;
            }
            else if (!this.findPositiveExterme && vi1 > vi)
            {
                this.findPositiveExterme = true;
            }
            return result;
        }

        private bool checkSteadyStateConditions(double item)
        {
            if (this.maxValueList.Count == this.numberOfStoredExtermes)
            {
                this.maxValueList.RemoveAt(0);
            }
            this.maxValueList.Add(item);

            double avVal = this.maxValueList.Average();
            double maxVal = this.maxValueList.Max();
            double minVal = this.maxValueList.Min();

            double error = Math.Abs((maxVal - minVal) / maxVal);
            if (error <= this.epsilon && this.maxValueList.Count == this.numberOfStoredExtermes)
            {
                this.SteadyStateValue = avVal;
                return true;
            }
            return false;
        }
    }
}