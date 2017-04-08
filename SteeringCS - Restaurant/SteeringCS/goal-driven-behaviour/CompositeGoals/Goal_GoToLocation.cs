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
        //todo

        private readonly Vector2D destination;


        public Goal_GoToLocation(World theWorld, Vector2D destination) : base(theWorld)
        {
            this.destination = destination;
        }

        public override void Activate()
        {
            state = Enums.State.Active;
            

            Vector2D currPos = world.TheBoss.Pos;
            List<Vertex> closestNodes = world.restaurandFloorGraph.PrepareAStarUsingWorldPosition((int)currPos.X, (int)currPos.Y, (int)destination.X, (int)destination.Y);
            AStarRemnant remnant = world.restaurandFloorGraph.AStar(closestNodes[0], closestNodes[1]);
            AddSubgoal(new Goal_FollowPath(world, remnant));
            
        }

        public override int Process()
        {
            ActivateIfIdle();

            state = (Enums.State)ProcessSubgoals();

            return (int)state;
        }

        public override void Terminate()
        {
            //Hardstop the host movement when arrived on target
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
