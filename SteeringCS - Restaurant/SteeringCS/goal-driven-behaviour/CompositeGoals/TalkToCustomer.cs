using SteeringCS.goal_driven_behaviour.CompositeGoals.AtomicSubgoals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;

namespace SteeringCS.goal_driven_behaviour.CompositeGoals
{
    class TalkToCustomer : CompositeGoal
    {
        public TalkToCustomer()
        {}

        public override void Activate()
        {
            subgoals = new stack.MyStack<Goal>();

            //this.AddSubgoal(new TalkCustomer());
            //this.AddSubgoal(new GoToCustomer());
            //this.AddSubgoal(new FindPathToCustomer());
            //this.AddSubgoal(new ChooseCustomer());
            

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

        public override string GetName()
        {
            throw new NotImplementedException();
        }
    }
}
