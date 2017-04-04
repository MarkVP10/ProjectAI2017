using SteeringCS.fuzzy.fuzzysets.eten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.fuzzy
{
    class FuzzyVariable
    {
        public Dictionary<string, FuzzySet> memberSets = new Dictionary<string, FuzzySet>();

        public string name;
        public double minRange;
        public double maxRange;

        public FuzzyVariable(string name)
        {
            this.name = name;
            minRange = 0;
            maxRange = 0;
        }

        public FuzzySet AddLeftShoulderSet(string name, double minBound, double peak, double maxBound)
        {
            LeftShoulder tmp = new LeftShoulder(peak, minBound, maxBound, name);
            memberSets.Add(name, tmp);
            AdjustRangeToFit(minBound, maxBound);

            return tmp;
        }

        public FuzzySet AddRightShoulderSet(string name, double minBound, double peak, double maxBound)
        {
            RightShoulder tmp = new RightShoulder(peak, minBound, maxBound, name);
            memberSets.Add(name, tmp);
            AdjustRangeToFit(minBound, maxBound);

            return tmp;
        }

        public FuzzySet AddTriangular(string name, double minBound, double peak, double maxBound)
        {
            Triangle tmp = new Triangle(peak, minBound, maxBound, name);
            memberSets.Add(name, tmp);
            AdjustRangeToFit(minBound, maxBound);

            return tmp;
        }



        public Dictionary<string, double> Fuzzify(double val)
        {
            Dictionary<string, double> DOMSets = new Dictionary<string, double>();
            foreach (KeyValuePair<string, FuzzySet> fs in memberSets)
            {
                DOMSets.Add(fs.Key, fs.Value.CalculateDom(val));    
            }

            return DOMSets;
        }

        //public double DeFuzzify(){}


        void AdjustRangeToFit(double min, double max)
        {
            if(min < minRange)
            {
                minRange = min;
            }
            if(max > maxRange)
            {
                maxRange = max;
            }
        }
    }
}
