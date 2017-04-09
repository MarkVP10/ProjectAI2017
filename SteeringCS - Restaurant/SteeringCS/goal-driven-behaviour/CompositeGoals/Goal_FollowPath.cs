using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.AtomicSubgoals;
using SteeringCS.behaviour;
using SteeringCS.graph;

namespace SteeringCS.goal_driven_behaviour.CompositeGoals
{
    class Goal_FollowPath : CompositeGoal
    {
        private AStarRemnant theFirstRemnant; //Possible future uses.
        private AStarRemnant currentRemnant;
        

        public Goal_FollowPath(World theWorld, AStarRemnant firstRemnantOfPath) : base(theWorld)
        {
            theFirstRemnant = firstRemnantOfPath;
            currentRemnant = firstRemnantOfPath;
        }



        public override void Activate()
        {
            state = Enums.State.Active;

            //In case there are no remntants left to follow
            if (currentRemnant == null)
            {
                world.TheBoss.PathToTarget = null;
                return;
            }
                


            //Get the remnant for the next FollowEdge
            AStarRemnant remnantToGoTo = currentRemnant;
            currentRemnant = currentRemnant?.GetNext();

            //Update the remnant of the host
            world.TheBoss.PathToTarget = remnantToGoTo;

            //Add a new subgoal
            AddSubgoal(new Goal_FollowEdge(world, remnantToGoTo));
        }

        public override int Process()
        {
            ActivateIfIdle();
            
            //Process the 1 FollowEdge goal that is in the subgoal stack
            state = (Enums.State)ProcessSubgoals();

            //When the FollowEdge is complete, but there are still more edges to follow.
            //Gets the next remnant
            if (isComplete() && currentRemnant != null)
            {
                Activate();
            }


            return (int)state;
        }

        public override void Terminate()
        {
            //nothing to do.
        }

        public override string GetName()
        {
            return "FollowPath";
        }
    }
}
