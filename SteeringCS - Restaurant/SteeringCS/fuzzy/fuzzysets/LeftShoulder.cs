using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.fuzzy.fuzzysets.eten
{
    class RightShoulder : FuzzySet //Rightshoulder
    {
        //the values that define the shape of this FLV
        public double PeakPoint;
        public double LeftOffset;
        public double RightOffset;

        public RightShoulder(double mid, double lft, double rgt, string name) : base(((mid + rgt) / 2), mid, lft, rgt, name) // mid + rgt / 2  because its a right shoulder.
        {
            PeakPoint = mid;
            LeftOffset = lft;
            RightOffset = rgt;
        }

        public override double CalculateDom(double val)
        {
            double DOM = 0;

            //check if in set
            if(val <= LeftOffset)
            {
                return DOM;
            }

            //Since this is the right shoulder of the graph, if the value bigger or equal than the peakpoint the values DOM is definitely 1.0. Cause the value cant outscale the graph.
            if(val >= PeakPoint)
            {
                return DOM = 1.0;
            }

            //if you come here, the value is between the left offset and the peak.
            double distanceToPeak = Math.Abs(LeftOffset - PeakPoint);
            double gradSide = 1.0 / distanceToPeak;
            double DifferenceFromPeak = Math.Abs(LeftOffset - val);
            DOM = DifferenceFromPeak * gradSide;

            return DOM;
        }

    }
}
