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
        private ObstacleAvoidance obstacleAvoidance;


        
        public enum Behaviours
        {
            None,
            Seek,
            Arrive,
            Wander
        }

        private Behaviours activeBehaviour = Behaviours.None;


        public CombineForces(MovingEntity me)
        {
            seekBehaviour = new SeekBehaviour(me);
            arriveBehaviour = new ArriveBehaviour(me, ArriveBehaviour.Deceleration.Slow);
            wanderBehaviour = new WanderBehaviour(me, 30, 20, 20);
            obstacleAvoidance = new ObstacleAvoidance(me);
        }






        //todo, make the combined values
        public Vector2D calcCombinedForce()
        {
            Vector2D combinedVector = new Vector2D();
            switch (activeBehaviour)
            {
                case Behaviours.Seek:
                    combinedVector += seekBehaviour.Calculate();
                    break;
                case Behaviours.Arrive:
                    combinedVector += arriveBehaviour.Calculate();
                    break;
                case Behaviours.Wander:
                    combinedVector += wanderBehaviour.Calculate();
                    break;
                case Behaviours.None:
                    break;
            }


            //todo swap
            //combinedVector += (obstacleAvoidance.Calculate()*5);
            obstacleAvoidance.Calculate();


            return combinedVector;
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
