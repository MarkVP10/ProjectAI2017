using System;
using SteeringCS.goal_driven_behaviour;

namespace SteeringCS.AtomicSubgoals
{
    class GoToCustomer : Goal
    {
        public GoToCustomer(World w) : base(w)
        {}

        public override void Activate()
        {
            System.Windows.Forms.MessageBox.Show("Activating GoToCustomer");

            /*
             * Path has been found in previous state.
             * Walk that path here
             */
            state = Enums.State.Active;
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

            state = Enums.State.Complete;
        }

        public override string GetName()
        {
            throw new NotImplementedException();
        }
    }
}
