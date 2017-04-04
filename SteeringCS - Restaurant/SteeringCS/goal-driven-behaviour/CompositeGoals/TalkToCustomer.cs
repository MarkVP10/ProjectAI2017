using SteeringCS.goal_driven_behaviour.CompositeGoals.AtomicSubgoals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.goal_driven_behaviour.CompositeGoals
{
    class TalkToCustomer : CompositeGoal
    {
        
        public override void Activate()
        {

            /* Make list of subgoals:
             * Choose customer to talk to.
             * Find path to that customer
             * Go to that customer.
             * Talk to that customer
             */
            subgoals = new stack.MyStack<Goal>();

            this.AddSubgoal(new TalkCustomer());
            this.AddSubgoal(new GoToCustomer());
            this.AddSubgoal(new FindPathToCustomer());
            this.AddSubgoal(new ChooseCustomer());

        }

        public override int Process()
        {
            System.Windows.Forms.MessageBox.Show("Start processing subgoals.");

            return ProcessSubgoals();
        }

        public override void Terminate()
        {

            System.Windows.Forms.MessageBox.Show("TalkToCustomerDone");

            /* Goal was met.
             * Go Away from customer using wander method (?)
             */


        }
    }
}
