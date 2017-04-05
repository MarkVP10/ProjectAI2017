using SteeringCS.stack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SteeringCS.goal_driven_behaviour.Enums;

namespace SteeringCS.goal_driven_behaviour
{
    abstract class CompositeGoal : Goal
    {
        public MyStack<Goal> subgoals;

        public override void Activate()
        {
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

        public override void AddSubgoal(Goal g)
        {
            subgoals.Push(g);
        }

        public int ProcessSubgoals()
        {
            while (subgoals.Top() != null && subgoals.Top().isComplete() || subgoals.Top().hasFailed())
            {
                subgoals.Top().Terminate();
                subgoals.Pop();
            }
            
            while (subgoals.Top() != null)
            {
                subgoals.Top().Activate();
                int statusOfSubGoals = subgoals.Top().Process();

                //if (statusOfSubGoals == (int)State.Complete && subgoals.Size() > 1)
                //{
                //    return (int)State.Active;
                //}
                subgoals.Pop();//sub goal completed, go to next subgoal
                //return statusOfSubGoals;
            }
            //else
            //{
            //goal is met, go into braindead mode.
                return (int)State.Complete;
            //}
        }

        public void RemoveAllSubgoals()
        {
            while(subgoals.Top() != null)
            {
                subgoals.Top().Terminate();
                subgoals.Pop();
            }
            subgoals = new MyStack<Goal>();

        }



    }
}
