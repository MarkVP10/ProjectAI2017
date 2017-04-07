using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.goal_driven_behaviour.CompositeGoals;

namespace SteeringCS.goal_driven_behaviour.ThinkGoals
{
    class Goal_ManagerThink : Goal_Think
    {



        public Goal_ManagerThink(World theWorld) : base(theWorld)
        {}


        public override void Activate()
        {
            state = Enums.State.Active;

            Random RNG = new Random();


            //A 10% chance that the manager will talk to a customer when it has nothing to do.
            if (RNG.Next(0, 10) == 0)
                AddGoal_TalkWithCustomer();
            else
                AddGoal_WanderRestaurant();
        }
        
        public void AddGoal_TalkWithCustomer()
        {
            //Check to see if the current subgoal is the talk with customer.
            if (!IsCurrentActiveSubgoal("TalkWithCustomer"))
            {
                //Remove all other subgoals before adding TalkWithCustomer
                RemoveAllSubgoals();
                AddSubgoal(new Goal_TalkWithCustomer(world));
            }
        }


        public void AddGoal_GoToPlayerSpot(int xCoord, int yCoord)
        {
            if (!IsCurrentActiveSubgoal("GoToPlayerSpot"))
            {
                RemoveAllSubgoals();
                AddSubgoal(new Goal_GoToPlayerSpot(world));
            }
        }


        public void AddGoal_WanderRestaurant()
        {
            if (!IsCurrentActiveSubgoal("WanderRestaurant"))
            {
                RemoveAllSubgoals();
                AddSubgoal(new Goal_WanderRestaurant(world));
            }
        }





    }
}
