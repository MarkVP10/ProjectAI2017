using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.fuzzy
{
    class FuzzyRule
    {
        public FzAND antecedent { get; set; }
        public FuzzyTerm consequent { get; set; }

        public FuzzyRule(FzAND ant, FuzzyTerm cons)
        {
            antecedent = ant;
            consequent = cons;
        }

        public void Calculate(double dom1, double dom2)
        {
            if(dom1 < dom2)
            {
                consequent.ORwithDOM(dom1);
            } else
            {
                consequent.ORwithDOM(dom2);
            }

        }

        

    }
}
