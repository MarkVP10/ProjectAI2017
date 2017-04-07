using System;
using SteeringCS.goal_driven_behaviour;

namespace SteeringCS.AtomicSubgoals
{
    class ChooseCustomer : Goal
    {
        public ChooseCustomer(World w) : base(w)
        { }

        private Random rnd;
        private int maxValue;

        public override void Activate()
        {
            System.Windows.Forms.MessageBox.Show("activated ChooseCustomer");

            rnd = new Random();
            //maxValue = World.Customers.Length(); //TODO: set Max value --> Max value is the highest index in an array of customers.
            state = Enums.State.Active;
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

            this.state = Enums.State.Complete;
            /* Goal was met.
             * Next subgoal of main goal.
             */


        }

        public override string GetName()
        {
            throw new NotImplementedException();
        }
    }
}
