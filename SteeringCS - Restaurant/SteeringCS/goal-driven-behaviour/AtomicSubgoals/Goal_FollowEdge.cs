using System;
using SteeringCS.behaviour;
using SteeringCS.goal_driven_behaviour;
using SteeringCS.graph;

namespace SteeringCS.AtomicSubgoals
{
    class Goal_FollowEdge : Goal
    {
        private AStarRemnant remnant;
        //private Edge edge;
        //private bool lastEdge;
        
        //todo deside whether to use Remnant or Edge

        //public Goal_FollowEdge(World theWorld, Edge edge, bool lastEdge) : base(theWorld)
        public Goal_FollowEdge(World theWorld, AStarRemnant remnant) : base(theWorld)
        {
            //this.edge = edge;
            //this.lastEdge = lastEdge;
            this.remnant = remnant;
        }

        public override void Activate()
        {
            state = Enums.State.Active;

            //Set the target
            //world.TheBoss.combineStratagy.SetTarget(edge.Destination.Pos);
            world.TheBoss.combineStratagy.SetTarget(remnant.GetPosition());

            //Set to Arrive to the last node. All other nodes will be Seeked.
            world.TheBoss.combineStratagy.SwitchBehaviour(remnant.isEnd() ? CombineForces.Behaviours.Arrive : CombineForces.Behaviours.Seek);
        }

        public override int Process()
        {
            ActivateIfIdle();
            

            //If reached the node, return complete
            //if (world.TheBoss.IsAtPosition(edge.Destination.Pos))
            if (world.TheBoss.IsAtPosition(remnant.GetPosition()))
                state = Enums.State.Complete;

            return (int)state;
        }

        public override void Terminate()
        {
            world.TheBoss.combineStratagy.SwitchBehaviour(CombineForces.Behaviours.None);
        }

        public override string GetName()
        {
            return "FollowEdge";
        }
    }
}
