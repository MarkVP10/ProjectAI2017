using System;
using SteeringCS.behaviour;
using SteeringCS.goal_driven_behaviour;
using SteeringCS.graph;

namespace SteeringCS.AtomicSubgoals
{
    class Goal_FollowEdge : Goal
    {
        private readonly AStarRemnant remnant;
        
        public Goal_FollowEdge(World theWorld, AStarRemnant remnant) : base(theWorld)
        {
            this.remnant = remnant;
        }

        public override void Activate()
        {
            state = Enums.State.Active;

            //Set the target
            world.TheBoss.combineStratagy.SetTarget(remnant.GetPosition());

            //Set to Arrive to the last node. All other nodes will be Seeked.
            world.TheBoss.combineStratagy.SwitchBehaviour(remnant.isEnd() ? CombineForces.Behaviours.Arrive : CombineForces.Behaviours.Seek);
            world.TheBoss.combineStratagy.SetArriveDeceleration(ArriveBehaviour.Deceleration.Slow);
        }

        public override int Process()
        {
            ActivateIfIdle();
            

            //If reached the node, return complete (when it is the last remnant, you want to be more precise)
            if (remnant.isEnd() && world.TheBoss.IsAtPosition(remnant.GetPosition(), 0.1))
                state = Enums.State.Complete;
            else if (!remnant.isEnd() && world.TheBoss.IsAtPosition(remnant.GetPosition()))
                state = Enums.State.Complete;


            return (int)state;
        }

        public override void Terminate()
        {
            //nothing
        }

        public override string GetName()
        {
            return "FollowEdge";
        }
    }
}
