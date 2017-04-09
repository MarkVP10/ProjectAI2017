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
        public Stack<Goal> SubgoalStack;


        protected CompositeGoal(World theWorld) : base(theWorld)
        {
            SubgoalStack = new Stack<Goal>();
        }


        public override void AddSubgoal(Goal g)
        {
            SubgoalStack.Push(g);
        }

        private bool IsNextGoalNonNull()
        {
            return (SubgoalStack.Count > 0 && SubgoalStack.Peek() != null);
        }
        

        /// <summary>
        /// Get a list of strings that have the names of all subgoals.
        /// </summary>
        /// <returns></returns>
        public override List<string> GetCompositeGoalAsStringList()
        {
            List<string> message = new List<string>();
            List<Goal> goalList = SubgoalStack.ToList();


            message.Add(GetName());

            //Go through every subgoal to get the names of all goals.
            foreach (Goal goal in goalList)
            {
                if(goal == null)
                    throw new Exception("There is a goal with a null value in the CompositeGoal class " + GetName() + ".");
                
                List<string> returnedNames = goal.GetCompositeGoalAsStringList();
                    
                //Add a space for each string returned to simulate tabs
                foreach (string s in returnedNames)
                    message.Add("  " + s);
            }

            return message;
        }

        public int ProcessSubgoals()
        {
            //Remove all completed and/or failed goals from the top the subgoal list.
            while (IsNextGoalNonNull() && (SubgoalStack.Peek().isComplete() || SubgoalStack.Peek().hasFailed()))
            {
                SubgoalStack.Pop().Terminate();
            }


            if (SubgoalStack.Count > 0)
            {
                if (SubgoalStack.Peek().isIdle())
                    SubgoalStack.Peek().Activate();

                int statusOfSubGoals = SubgoalStack.Peek().Process();

                //If the subgoal is complete, but there are more subgoals to complete
                if (statusOfSubGoals == (int)State.Complete && SubgoalStack.Count() > 1)
                    return (int)State.Active;


                //Return the status of the latest processed subgoal. The subgoal either failed, is still active or is the last in the goal list (and the status of that goal will be returned).
                return statusOfSubGoals;
            }
            else
            {
                //No more subgoals to process, so this composite goal is comlete.
                return (int)State.Complete;
            }

            
        }

        public void RemoveAllSubgoals()
        {
            while (IsNextGoalNonNull())
            {
                SubgoalStack.Pop().Terminate();
            }
            SubgoalStack = new Stack<Goal>();
        }
    }
}
