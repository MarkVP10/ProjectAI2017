using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteeringCS.behaviour;
using SteeringCS.graph;

namespace SteeringCS.goal_driven_behaviour.CompositeGoals
{
    class Goal_GoToLocation : CompositeGoal
    {
        private readonly Vector2D destination;


        public Goal_GoToLocation(World theWorld, Vector2D destination) : base(theWorld)
        {
            this.destination = destination;
        }

        public override void Activate()
        {
            state = Enums.State.Active;
            
            //Get the current host's position
            Vector2D currPos = world.TheBoss.Pos;

            //Do A*
            List<Vertex> closestNodes = world.restaurandFloorGraph.PrepareAStarUsingWorldPosition((int)currPos.X, (int)currPos.Y, (int)destination.X, (int)destination.Y);
            AStarRemnant remnant = world.restaurandFloorGraph.AStar(closestNodes[0], closestNodes[1]);
            
            //Add subgoal to follow remnant path.
            AddSubgoal(new Goal_FollowPath(world, remnant)); //Even if remnant is null, the Goal_FollowPath will handle the null correctly without crashing
        }

        public override int Process()
        {
            ActivateIfIdle();

            state = (Enums.State)ProcessSubgoals();

            return (int)state;
        }

        public override void Terminate()
        {
            //Hardstop the host movement when arrived on target. Otherwise it will slide across the floor because it still has Velocity.
            //If the host has a high Velocity and makes a 'perfect' landing, it will stop immediately.
            //It's ugly, we know... 

            world.TheBoss.Velocity = new Vector2D();
            world.TheBoss.combineStratagy.SetTarget(null);
            world.TheBoss.combineStratagy.SwitchBehaviour(CombineForces.Behaviours.None);
        }

        public override string GetName()
        {
            return "GoToLocation";
        }
    }
}
