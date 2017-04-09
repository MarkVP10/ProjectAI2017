using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.goal_driven_behaviour.AtomicSubgoals;
using SteeringCS.util;

namespace SteeringCS.goal_driven_behaviour.CompositeGoals
{
    class Goal_GoToPlayerSpot : CompositeGoal
    {
        private readonly Vector2D destination;

        public Goal_GoToPlayerSpot(World theWorld, Vector2D playerChoosenWorldCoords) : base(theWorld)
        {
            destination = playerChoosenWorldCoords;
        }

        public override void Activate()
        {
            state = Enums.State.Active;

            Utility.WriteToConsoleUsingColor("Going to player appointed spot.", ConsoleColor.White, ConsoleColor.DarkYellow);

            //-Go to player appointed spot
            //-Idle for 1 second
            AddSubgoal(new Goal_IdleAtCurrentLocation(world, 1));
            AddSubgoal(new Goal_GoToLocation(world, destination));
        }

        public override int Process()
        {
            ActivateIfIdle();

            state = (Enums.State)ProcessSubgoals();

            return (int)state;
        }

        public override void Terminate()
        {
            //Just printing that the host is done.
            Utility.WriteToConsoleUsingColor("Done going to player spot\r\n", ConsoleColor.White, ConsoleColor.Red);
        }

        public override string GetName()
        {
            return "GoToPlayerSpot";
        }
    }
}
