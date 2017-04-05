using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SteeringCS.goal_driven_behaviour.Enums;
using SteeringCS.fuzzy;
using SteeringCS.fuzzy.fuzzysets;



namespace SteeringCS.goal_driven_behaviour.CompositeGoals.AtomicSubgoals
{
    class TalkCustomer : CompositeGoal
    {
        public override void Activate()
        {
            System.Windows.Forms.MessageBox.Show("Activating talking");

            state = State.Active;
        }

        public override int Process()
        {
            FuzzyModule fm = new FuzzyModule();

            FuzzyVariable food = fm.CreateFLV("Food");
            FuzzySet bad = food.AddLeftShoulderSet("Bad", 0, 25, 50);
            FuzzySet decent = food.AddTriangular("Decent", 25, 50, 75);
            FuzzySet good = food.AddRightShoulderSet("Good", 50, 75, 100);

            FuzzyVariable service = fm.CreateFLV("Service");
            FuzzySet discontent = service.AddLeftShoulderSet("Discontent", 0,0,100);
            FuzzySet content = service.AddRightShoulderSet("Content",0,100,100 );
            
            FuzzyVariable total = fm.CreateFLV("Total");
            FuzzySet sad = total.AddLeftShoulderSet("Sad", 0, 30, 50);
            FuzzySet meh = total.AddTriangular("Meh", 30, 50, 100);
            FuzzySet yay = total.AddRightShoulderSet("Yay",50,100,100);

            fm.AddRule(new FzAND(bad, discontent), new FuzzyTerm(sad));
            fm.AddRule(new FzAND(bad, content), new FuzzyTerm(meh));

            fm.AddRule(new FzAND(decent, discontent), new FuzzyTerm(meh));
            fm.AddRule(new FzAND(decent, content), new FuzzyTerm(yay));

            fm.AddRule(new FzAND(good, discontent), new FuzzyTerm(meh));
            fm.AddRule(new FzAND(good, content), new FuzzyTerm(yay));

            Random rnd = new Random();

            int x = rnd.Next(0, 101);
            int y = rnd.Next(0, 101);

            Dictionary<string, double>  domFood = fm.Fuzzify("Food", 52);
            Dictionary<string, double> domService = fm.Fuzzify("Service", 32);

            double fuzzyCrispResult = fm.CalculatePerRule(domFood, domService);


            //fuzzy logic here
            System.Windows.Forms.MessageBox.Show("Manager:'Is everything to your liking?'");
            System.Windows.Forms.MessageBox.Show("Customer:'I'll give the dinner a " + Math.Round((fuzzyCrispResult/10), 1) + " out of 10!'");
            System.Windows.Forms.MessageBox.Show("Manager:'Thank you for your review with your Fuzzy Logic. :)'");

            Terminate();
            return (int)state;
        }

        public override void Terminate()
        {
            System.Windows.Forms.MessageBox.Show("Terminate talking");

            state = State.Complete;
        }
    }
}
