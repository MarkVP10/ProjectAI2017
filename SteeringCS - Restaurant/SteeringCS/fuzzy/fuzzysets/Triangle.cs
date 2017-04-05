using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.fuzzy.fuzzysets.eten
{
    class Triangle : FuzzySet
    {
        //the values that define the shape of this FLV
        double PeakPoint;
        double LeftOffset;
        double RightOffset;

        public Triangle(double mid, double lft, double rgt, string name) : base(mid, mid, lft, rgt, name)
        {
            PeakPoint = mid;
            LeftOffset = lft;
            RightOffset = rgt;
        }


        public override double CalculateDom(double val)
        {
            double DOM = 0;

            //check if value is in set.
            if (val <= LeftOffset)
            {
                return DOM;
            }

            if (val >= RightOffset)
            {
                return DOM;
            }

            //If you come here, value is in set.

            //Check if value is equal to peakpoint where DOM is 1.0.
            if(val == PeakPoint)
            {
                return DOM = 1.0;
            }

            double distanceToPeak;
            double differenceFromOffset;
            //Check if value is on the right side of the set.
            if (val > PeakPoint) 
            {
                distanceToPeak = Math.Abs(RightOffset - PeakPoint); 
                differenceFromOffset = Math.Abs(RightOffset - val); 

            }
            //value is on left side.
            else
            {
                //val = 30
                distanceToPeak = Math.Abs(LeftOffset - PeakPoint); // 25
                differenceFromOffset = Math.Abs(LeftOffset - val); // 5

            }

            double gradSide = 1.0 / distanceToPeak; 
            DOM = differenceFromOffset * gradSide;

            return DOM;
        }



    }
}
