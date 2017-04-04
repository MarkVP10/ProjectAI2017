using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SteeringCS.goal_driven_behaviour.Enums;

namespace SteeringCS.goal_driven_behaviour
{
    public abstract class Goal
    {
        public State state;

        public abstract void Activate();
        public abstract int Process();
        public abstract void Terminate();
        public abstract void AddSubgoal(Goal g);

        public bool isActive()
        {
            return state == State.Active;
        }

        public bool isIdle()
        {
            return state == State.Idle;
        }

        public bool isComplete()
        {
            return state == State.Complete;
        }

        public bool hasFailed()
        {
            return state == State.Failed;
        }
    }
}
