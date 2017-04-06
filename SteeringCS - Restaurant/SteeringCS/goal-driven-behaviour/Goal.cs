using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SteeringCS.goal_driven_behaviour.Enums;

namespace SteeringCS.goal_driven_behaviour
{
    //todo: Make this class generic and add a rule that all classes must be children of BaseGameEntity

    public abstract class Goal
    {
        public State state;

        public abstract void Activate();
        public abstract int Process();
        public abstract void Terminate();
        public virtual void AddSubgoal(Goal g)
        { throw new Exception("Cannot add goals to atomic goals.");}


        //todo 'HandleMessage()' remove if not used
        public virtual bool HandleMessage(Telegram t)
        {return false;}


        public abstract string GetName();
        public virtual List<string> GetCompositeGoalAsStringList()
        {
            List<string> result = new List<string>();
            result.Add(GetName());
            return result;
        }

        
        
        
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
