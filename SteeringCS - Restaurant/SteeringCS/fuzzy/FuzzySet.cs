using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.fuzzy
{
    abstract class FuzzySet
    {
        public double DOM;
        public double RepresentativeValue;
        public double peak;
        public double leftOffset;
        public double rightOffset;
        public string name;

        public FuzzySet(double RepVal, double mid, double left, double right, string name)
        {
            RepresentativeValue = RepVal; //Where the peakpoint is.
            peak = mid;
            leftOffset = left;
            rightOffset = right;
            this.name = name;
        }

        public virtual double CalculateDom(double val) {
            throw new NotImplementedException();
        }

        //if this fuzzy set is part of a consequent FLV and it is fired by a rule,
        //then this method sets the DOM (in this context, the DOM represents a
        //confidence level) to the maximum of the parameter value or the set's
        //existing m_dDOM value
        public void ORwithDom(double val)
        {
            throw new NotImplementedException();
        }
    }
}
