using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.goal_driven_behaviour.AtomicSubgoals;

namespace SteeringCS.goal_driven_behaviour.CompositeGoals
{
    class Goal_GoToPlayerSpot : CompositeGoal
    {

        //todo

        private readonly Vector2D destination;

        public Goal_GoToPlayerSpot(World theWorld, Vector2D playerChoosenWorldCoords) : base(theWorld)
        {
            destination = playerChoosenWorldCoords;
        }

        public override void Activate()
        {
            state = Enums.State.Active;

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
            //nothing, realy...
            Console.WriteLine("Done going to player spot");
        }

        public override string GetName()
        {
            return "GoToPlayerSpot";
        }

        
    }
}
