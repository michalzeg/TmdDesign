using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdDesign.SimpleClasses;
using TmdDesign.ExcitationForces;

namespace TmdDesign
{
    public class ExcitationForcePresenter
    {
        private readonly IExcitationForceView view;

        public ExcitationForcePresenter(IExcitationForceView view)
        {
            this.view = view;
        }

        public void Calculate()
        {
            ExcitationForceInputData inputData = this.view.InputData;

            EquivalentExcitationForce eqForce = new EquivalentExcitationForce();
            double p = eqForce.CalculateEquivalenDynamicForce(inputData);
            double k = eqForce.DynamicStiffness;

            this.view.DynamicStiffness = k;
            this.view.EquivalentExcitationForce = p;
        }
    }
}