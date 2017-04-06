using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.goal_driven_behaviour.CompositeGoals.AtomicSubgoals;

namespace SteeringCS.goal_driven_behaviour.CompositeGoals
{
    class Goal_TalkWithCustomer : CompositeGoal
    {

        //todo

        public override void Activate()
        {


            AddSubgoal(new Goal_ChooseCustomer());
            AddSubgoal(new Goal_GoToTable());
            AddSubgoal(new Goal_ChooseCustomer());


            throw new NotImplementedException();
        }

        public override int Process()
        {
            throw new NotImplementedException();
        }

        public override void Terminate()
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            return "TalkWithCustomer";
        }
    }
}
