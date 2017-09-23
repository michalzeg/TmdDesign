using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmdDesign.FindMax
{
    /// <summary>
    /// Based on given values i and i+1 class find the maxium values for steady stay conditions
    /// 
    /// </summary>
    public class MaxValue
    {
        private List<double> maxValueList = new List<double>(); //list with max values,
                                                                    //the list stores only numberOfStoredExtremes
        
        private int numberOfStoredExtermes; //number of exteremes stored in list
        private double epsilon; //the number which determines the accurancy 
 

        private bool findPositiveExterme;//determines if currentrly searched extreme is positive or negative
        private double currentExterme;//current extreme

        public double SteadyStateValue { get; private set; }
        
        public MaxValue(int maxNumberOfExtremes, double epsilon)
        {
       
            this.SteadyStateValue = double.NaN;
            this.numberOfStoredExtermes = maxNumberOfExtremes;
            this.epsilon = epsilon;
            this.findPositiveExterme = true;
            this.currentExterme = 0;
        }

        public bool FindMaxAcceleration(double vi, double vi1)
        {
            //vi - value for i iteration
            //vi1- value for i+1 iteration
            bool result = false;
            if (this.findPositiveExterme)
            {
                if (vi1 < vi)
                {
                    //local extereme has been found
                    result = this.checkSteadyStateConditions(Math.Abs(vi));
                    this.findPositiveExterme = false;
                }
            }
            else
            {
                if (vi1 > vi)
                {
                    //result = this.checkSteadyStateConditions(Math.Abs(vi));
                    this.findPositiveExterme = true;
                }
            }
            return result;

        }

        private bool checkSteadyStateConditions(double item)
        {
            if (this.maxValueList.Count == this.numberOfStoredExtermes)
            {
                //there is max items in list, removing first element
                this.maxValueList.RemoveAt(0);
            }
            this.maxValueList.Add(item);

            double avVal = this.maxValueList.Average(); //average value
            double maxVal = this.maxValueList.Max();
            double minVal = this.maxValueList.Min();


            //error defined as (max - min)/avegage
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
