using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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


            //A 33% chance that the manager will wander around the restaurant
            //A 67% change to talk to a customer
            if (RNG.Next(0, 3) == 0)
                AddGoal_WanderRestaurant();
            else
                AddGoal_TalkWithCustomer();
            
        }

        public override void HandleMessageToBrain(string simpleMessage, object data = null)
        {
            switch (simpleMessage)
            {
                case "GoToPlayerSpot":
                    if (data.GetType() == typeof (MouseEventArgs))
                        AddGoal_GoToPlayerSpot(((MouseEventArgs)data).X, ((MouseEventArgs)data).Y);
                    break;
                case "ForceCustomerTalk":
                    AddGoal_TalkWithCustomer();
                    break;
            }
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
            //Player will always interupt the current goal.
            RemoveAllSubgoals();
            AddSubgoal(new Goal_GoToPlayerSpot(world, new Vector2D(xCoord, yCoord)));
        }


        public void AddGoal_WanderRestaurant()
        {
            if (!IsCurrentActiveSubgoal("WanderRestaurant"))
            {
                RemoveAllSubgoals();
                AddSubgoal(new Goal_WanderRestaurant(world, 20, 40));
            }
        }


        public override string GetName()
        {
            return "Think_Manager";
        }
    }
}
