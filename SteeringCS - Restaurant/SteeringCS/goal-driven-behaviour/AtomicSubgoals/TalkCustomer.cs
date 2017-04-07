using System;
using System.Collections.Generic;
using SteeringCS.fuzzy;
using SteeringCS.goal_driven_behaviour;

namespace SteeringCS.AtomicSubgoals
{
    class TalkCustomer : Goal
    {
        public TalkCustomer(World w) : base(w)
        {}

        public override void Activate()
        {
            System.Windows.Forms.MessageBox.Show("Activating talking");

            state = Enums.State.Active;
        }

        public override int Process()
        {
            //Construct Fuzzy module
            FuzzyModule fm = new FuzzyModule();

            //Create FLV's
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

            //Construct rule set.
            fm.AddRule(new FzAND(bad, discontent), new FuzzyTerm(sad));
            fm.AddRule(new FzAND(bad, content), new FuzzyTerm(meh));

            fm.AddRule(new FzAND(decent, discontent), new FuzzyTerm(meh));
            fm.AddRule(new FzAND(decent, content), new FuzzyTerm(yay));

            fm.AddRule(new FzAND(good, discontent), new FuzzyTerm(meh));
            fm.AddRule(new FzAND(good, content), new FuzzyTerm(yay));

            //Generate random number to go into the Fuzzy logic to determine a grade for this restaurant.
            Random rnd = new Random();
            int x = rnd.Next(0, 101);
            int y = rnd.Next(0, 101);

            //Fuzzify the Food and the Service values
            Dictionary<string, double>  domFood = fm.Fuzzify("Food", x);
            Dictionary<string, double> domService = fm.Fuzzify("Service", y);

            //Calculate the result of the DOMs out of the food and Service FLV's.
            double fuzzyCrispResult = fm.CalculatePerRule(domFood, domService, "Total");

            //Talk to customer and use the fuzzy logic result to show the result of the review.
            System.Windows.Forms.MessageBox.Show("Manager:'Is everything to your liking?'");
            System.Windows.Forms.MessageBox.Show("Customer:'I'll give the dinner a " + 
                Math.Round((fuzzyCrispResult/10), 1) + " out of 10!'");
            System.Windows.Forms.MessageBox.Show("Manager:'Thank you for your review with your Fuzzy Logic. :)'");

            Terminate();
            return (int)state;
        }

        public override void Terminate()
        {
            System.Windows.Forms.MessageBox.Show("Terminate talking");

            state = Enums.State.Complete;
        }

        public override string GetName()
        {
            throw new NotImplementedException();
        }
    }
}
