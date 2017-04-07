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
        //todo: remove all 'MyStack<Goal> subgoal' refecenses and commented code

        //public MyStack<Goal> subgoals;
        public Stack<Goal> subgoalStack;


        protected CompositeGoal(World theWorld) : base(theWorld)
        {
            subgoalStack = new Stack<Goal>();
        }


        public override void AddSubgoal(Goal g)
        {
            //subgoals.Push(g);
            subgoalStack.Push(g);
        }

        private bool IsNextGoalNonNull()
        {
            return (subgoalStack.Count > 0 && subgoalStack.Peek() != null);
        }
        
        public override List<string> GetCompositeGoalAsStringList()
        {
            
            //List<string> message = new List<string>();
            //ListNode<Goal> indexNode = subgoals?.List.Head.Next;
            
            
            ////Go through every subgoal to get the names of all goals.
            //while (indexNode?.Data != null)
            //{
            //    List<string> returnedNames = indexNode.Data.GetCompositeGoalAsStringList();
                    
            //    foreach (string s in returnedNames)
            //        message.Add("  " + s);

            //    indexNode = indexNode.Next;
            //}


            ////Because there is a stack, the name order needs to be reverced
            //message.Add(GetName());
            //message.Reverse();
            

            //return message;
            

            
            List<string> message = new List<string>();
            List<Goal> goalList = subgoalStack.ToList();
            //Go through every subgoal to get the names of all goals.
            foreach (Goal goal in goalList)
            {
                if(goal == null)
                    throw new Exception("There is a goal with a null value in the CompositeGoal class " + GetName() + ".");
                
                List<string> returnedNames = goal.GetCompositeGoalAsStringList();
                    
                foreach (string s in returnedNames)
                    message.Add("  " + s);
            }


            //Because there is a stack, the name order needs to be reverced
            message.Add(GetName());
            message.Reverse();
            

            return message;
        }

        public int ProcessSubgoals()
        {
            //Remove all completed and/or failed goals from the top the subgoal list.
            while (IsNextGoalNonNull() && (subgoalStack.Peek().isComplete() || subgoalStack.Peek().hasFailed()))
            {
                subgoalStack.Pop().Terminate();
            }


            if (subgoalStack.Count > 0)
            {
                if (subgoalStack.Peek().isIdle())
                    subgoalStack.Peek().Activate();

                int statusOfSubGoals = subgoalStack.Peek().Process();

                //If the subgoal is complete, but there are more subgoals to complete
                if (statusOfSubGoals == (int)State.Complete && subgoalStack.Count() > 1)
                    return (int)State.Active;


                //Return the status of the latest processed subgoal. The subgoal either failed, is still active or is the last in the goal list (and the status of that goal will be returned).
                return statusOfSubGoals;
            }
            else
            {
                //No more subgoals to process, so this composite goal is comlete.
                return (int)State.Complete;
            }





            ////Remove all completed and/or failed goals from the top the subgoal list.
            //while (subgoals.Top() != null && subgoals.Top().isComplete() || subgoals.Top().hasFailed())
            //{
            //    subgoals.Top().Terminate();
            //    subgoals.Pop();
            //}


            //if (subgoals.Top() != null)
            //{
            //    if(subgoals.Top().isIdle())
            //        subgoals.Top().Activate();

            //    int statusOfSubGoals = subgoals.Top().Process();

            //    //If the subgoal is complete, but there are more subgoals to complete
            //    if (statusOfSubGoals == (int)State.Complete && subgoals.Size() > 1)
            //        return (int)State.Active;


            //    //Return the status of the latest processed subgoal. The subgoal either failed, is still active or is the last in the goal list (and the status of that goal will be returned).
            //    return statusOfSubGoals;
            //}
            //else
            //{
            //    //No more subgoals to process, so this composite goal is comlete.
            //    return (int)State.Complete;
            //}
        }

        public void RemoveAllSubgoals()
        {
            while (IsNextGoalNonNull())
            {
                subgoalStack.Pop().Terminate();
            }
            subgoalStack = new Stack<Goal>();



            //while(subgoals.Top() != null)
            //{
            //    subgoals.Top().Terminate();
            //    subgoals.Pop();
            //}
            //subgoals = new MyStack<Goal>();
        }



    }
}
