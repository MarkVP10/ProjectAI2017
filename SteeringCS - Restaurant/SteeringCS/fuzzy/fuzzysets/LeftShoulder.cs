using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.fuzzy.fuzzysets.eten
{
    class LeftShoulder : FuzzySet //LeftShoulder
    {
        //the values that define the shape of this FLV
        double PeakPoint;
        double LeftOffset;
        double RightOffset;

        public LeftShoulder(double mid, double lft, double rgt, string name) : base(((mid + lft) / 2), mid, lft, rgt, name) // mid + lft / 2  because its a left shoulder.
        {
            PeakPoint = mid;
            LeftOffset = lft;
            RightOffset = rgt;
        }

        public override double CalculateDom(double val)
        {
            double DOM = 0.0;

            //check if in set
            if (val >= RightOffset)
            {
                return DOM;
            }

            //Since this is the left shoulder of the graph, if the value less or equal than the peakpoint the values DOM is definitely 1.0. Cause the value cant outscale the graph.
            if (val <= PeakPoint)
            {
                return DOM = 1.0;
            }

            //if you come here, the value is between the right offset and the peak.
                
            //val = 30

            double distanceToPeak = Math.Abs(RightOffset - PeakPoint); 
            double gradSide = 1.0 / distanceToPeak;                    
            double DifferenceFromPeak = Math.Abs(RightOffset - val);   
            DOM = DifferenceFromPeak * gradSide;

            return DOM;
        }
    }
}
