using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.fuzzy
{
    class FuzzyModule
    {
        public Dictionary<string, FuzzyVariable> fuzzyMap = new Dictionary<string, FuzzyVariable>();

        public List<FuzzyRule> m_rules = new List<FuzzyRule>();
            
        public FuzzyModule(){}

        public void AddRule(FzAND antecedent, FuzzyTerm consequent)
        {
            m_rules.Add(new FuzzyRule(antecedent, consequent));
        }

        public Dictionary<string, double> Fuzzify(string NameOfFLV, double val)
        {
            FuzzyVariable fv;
            fuzzyMap.TryGetValue(NameOfFLV, out fv);
            return fv.Fuzzify(val);
            
        }

        public double CalculatePerRule(Dictionary<string, double> set1, Dictionary<string, double> set2, string nameOfResultSet)
        {
            double crisp = 0;

            Dictionary<string, double> fam = new Dictionary<string, double>();
            foreach (KeyValuePair<string, FuzzySet> resSet in fuzzyMap[nameOfResultSet].memberSets)
            {
                fam.Add(resSet.Key, 0);
            }

            foreach (FuzzyRule fr in m_rules)
            {
                string key1 = fr.antecedent.fz1.name;
                string key2 = fr.antecedent.fz2.name;
                double value1;
                double value2;
                set1.TryGetValue(key1, out value1);
                set2.TryGetValue(key2, out value2);

                fr.Calculate(value1, value2);


                double curr;
                fam.TryGetValue(fr.consequent.fz.name, out curr);
                if (curr < fr.consequent.DOM)
                {
                    fam[fr.consequent.fz.name] = fr.consequent.DOM;
                }

            }

            double numerator = 0;
            double denumerator = 0;

            foreach (KeyValuePair<string, double> element in fam)
            {
                denumerator += element.Value;
                fuzzyMap["Total"].memberSets[element.Key].DOM = element.Value;
            }


            foreach(KeyValuePair<string, FuzzySet> element in fuzzyMap["Total"].memberSets)
            {
                numerator += element.Value.RepresentativeValue * element.Value.DOM;
            }

            crisp = numerator / denumerator;

            return crisp;
        }

        double DeFuzzify(string key) { return 1.0; }
        
        public FuzzyVariable CreateFLV(string name)
        {
            FuzzyVariable tmp = new FuzzyVariable(name);
            fuzzyMap.Add(name, tmp);
            return tmp;

        }


    }
}
