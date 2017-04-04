using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.fuzzy
{
    class FuzzyTerm
    {
        public FuzzySet fz;
        public double DOM; 

        public FuzzyTerm(FuzzySet fz)
        {
            this.fz = fz;
        }

        public virtual double GetDOM(double val)
        {
            return 0;
        }

        public virtual void ClearDOM()
        {
            //DOM = 0;
        }
        
        public virtual void ORwithDOM(double val)
        {
            DOM = val;
        }

    }
}
