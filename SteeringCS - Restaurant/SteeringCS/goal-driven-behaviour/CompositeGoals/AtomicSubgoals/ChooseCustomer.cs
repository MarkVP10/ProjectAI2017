using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SteeringCS.goal_driven_behaviour.Enums;

namespace SteeringCS.goal_driven_behaviour.CompositeGoals.AtomicSubgoals
{
    class ChooseCustomer : CompositeGoal
    {
        private Random rnd;
        private int maxValue;

        public override void Activate()
        {
            System.Windows.Forms.MessageBox.Show("activated ChooseCustomer");

            rnd = new Random();
            //maxValue = World.Customers.Length(); //TODO: set Max value --> Max value is the highest index in an array of customers.
            state = State.Active;
        }

        public override int Process()
        {
            System.Windows.Forms.MessageBox.Show("Procesing Choosecustomer");

            //Choose customer to talk to
            var x = rnd.Next();//TODO: set Max value --> Max value is the highest index in an array of customers.
            Terminate();
            return (int)state;
        }

        public override void Terminate()
        {
            System.Windows.Forms.MessageBox.Show("Terminate Choosecustomer");

            this.state = State.Complete;
            /* Goal was met.
             * Next subgoal of main goal.
             */


        }
    }
}
