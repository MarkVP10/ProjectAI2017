using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.fuzzy
{
    class FzAND : FuzzyTerm
    {
        public FuzzySet fz1;
        public FuzzySet fz2;
        public double dom1;
        public double dom2;


        public FzAND(FuzzySet fz1, FuzzySet fz2) : base (fz1)
        {
            this.fz1 = fz1;
            this.fz2 = fz2;
            ClearDOM();
        }

        public virtual double GetDOM(double val, double val2)
        {
            dom1 = fz1.CalculateDom(val);
            dom2 = fz2.CalculateDom(val2);

            if(dom1 <= dom2)
            {
                return dom1;
            }

            return dom2;
        }

        public override void ClearDOM()
        {
            dom1 = 0;
            dom2 = 0;
        }

        public override void ORwithDOM(double val)
        {
            //DOM = val;
        }

    }
}
