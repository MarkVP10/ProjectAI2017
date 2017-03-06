using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;

namespace SteeringCS.behaviour
{
    class CombineForces
    {
        private SeekBehaviour seekBehaviour;
        private ArriveBehaviour arriveBehaviour;
        private WanderBehaviour wanderBehaviour;


        
        public enum Behaviours
        {
            None,
            Seek,
            Arrive,
            Wander
        }

        private Behaviours activeBehaviour = Behaviours.Seek;


        public CombineForces(MovingEntity me)
        {
            seekBehaviour = new SeekBehaviour(me);
            arriveBehaviour = new ArriveBehaviour(me, ArriveBehaviour.Deceleration.Slow);
            wanderBehaviour = new WanderBehaviour(me, 30, 20, 20);
        }






        //todo, make the combined values
        public Vector2D calcCombinedForce()
        {
            switch (activeBehaviour)
            {
                case Behaviours.Seek:
                    return seekBehaviour.Calculate();

                case Behaviours.Arrive:
                    return arriveBehaviour.Calculate();

                case Behaviours.Wander:
                    return wanderBehaviour.Calculate();

                default:
                    return new Vector2D();
            }
        }


        public void SetTarget(Vector2D target)
        {
            SetSeekTarget(target);
            SetArriveTarget(target);
        }

        private void SetSeekTarget(Vector2D target)
        {
            seekBehaviour.Target = target;
        }

        private void SetArriveTarget(Vector2D target)
        {
            arriveBehaviour.TargetPos = target;
        }

        public void SetArriveDeceleration(ArriveBehaviour.Deceleration deceleration)
        {
            arriveBehaviour.deceleration = deceleration;
        }

        public void SwitchBehaviour(Behaviours b)
        {
            activeBehaviour = b;
        }
    }
}
