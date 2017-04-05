using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SteeringCS.goal_driven_behaviour.Enums;

namespace SteeringCS.goal_driven_behaviour.CompositeGoals.AtomicSubgoals
{
    class GoToCustomer : CompositeGoal
    {
        public override void Activate()
        {
            System.Windows.Forms.MessageBox.Show("Activating GoToCustomer");

            /*
             * Path has been found in previous state.
             * Walk that path here
             */
            state = State.Active;
        }

        public override int Process()
        {
            System.Windows.Forms.MessageBox.Show("Processing GoToCustomer");

            /*
            * Path has been found in previous state.
            * Walk that path here
            */
            //WalkPath(); TODO: make this method

            Terminate();
            return (int)state;

        }

        public override void Terminate()
        {
            System.Windows.Forms.MessageBox.Show("Terminating GoToCustomer");

            state = State.Complete;
        }
    }
}
