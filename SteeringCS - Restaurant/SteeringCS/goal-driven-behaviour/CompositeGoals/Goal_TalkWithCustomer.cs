using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteeringCS.AtomicSubgoals;

namespace SteeringCS.goal_driven_behaviour.CompositeGoals
{
    class Goal_TalkWithCustomer : CompositeGoal
    {

        //todo

        public Goal_TalkWithCustomer(World theWorld) : base(theWorld)
        {
        }


        public override void Activate()
        {
            state = Enums.State.Active;

            //AddSubgoal(new Goal_ChooseCustomer());
            //AddSubgoal(new Goal_GoToTable());
            //AddSubgoal(new Goal_ChooseCustomer());

            
        }

        public override int Process()
        {
            ActivateIfIdle();

            Console.WriteLine("TALKING WITH CUSTOMER!!");


            state = (Enums.State)ProcessSubgoals();



            return (int) state;
        }

        public override void Terminate()
        {
            //nothing to clean up
            Console.WriteLine("TERMINATE TALKING");
        }

        public override string GetName()
        {
            return "TalkWithCustomer";
        }

        
    }
}
