using SteeringCS.stack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.stack.LinkedList;
using static SteeringCS.goal_driven_behaviour.Enums;

namespace SteeringCS.goal_driven_behaviour
{
    abstract class CompositeGoal : Goal
    {
        public MyStack<Goal> subgoals;

        

        public override void AddSubgoal(Goal g)
        {
            subgoals.Push(g);
        }
        
        
        public override List<string> GetCompositeGoalAsStringList()
        {
            List<string> message = new List<string>();
            ListNode<Goal> indexNode = subgoals?.List.Head.Next;

            //Go through every subgoal to get the names of all goals.
            while (indexNode?.Data != null)
            {
                List<string> returnedNames = indexNode.Data.GetCompositeGoalAsStringList();
                    
                foreach (string s in returnedNames)
                    message.Add("  " + s);

                indexNode = indexNode.Next;
            }


            //Because there is a stack, the name order needs to be reverced
            message.Add(GetName());
            message.Reverse();
            

            return message;
        }

        public int ProcessSubgoals()
        {
            //Remove all completed and/or failed goals from the top the subgoal list.
            while (subgoals.Top() != null && subgoals.Top().isComplete() || subgoals.Top().hasFailed())
            {
                subgoals.Top().Terminate();
                subgoals.Pop();
            }
            

            if (subgoals.Top() != null)
            {
                if(subgoals.Top().isIdle())
                    subgoals.Top().Activate();

                int statusOfSubGoals = subgoals.Top().Process();

                //If the subgoal is complete, but there are more subgoals to complete
                if (statusOfSubGoals == (int)State.Complete && subgoals.Size() > 1)
                    return (int)State.Active;


                //Sub goal completed, go to next subgoal
                return statusOfSubGoals;
            }
            else
            {
                //Goal is met, go into braindead mode.
                return (int)State.Complete;
            }
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
