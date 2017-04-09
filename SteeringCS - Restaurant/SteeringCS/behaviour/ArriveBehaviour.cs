using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;

namespace SteeringCS.behaviour
{
    class ArriveBehaviour : SteeringBehaviour
    {
        public enum Deceleration { Slow = 3, Normal = 2, Fast = 1}
        public Vector2D TargetPos { get; set; }
        public Deceleration deceleration;

        public ArriveBehaviour(MovingEntity me, Deceleration dec, Vector2D target = null) : base(me)
        {
            TargetPos = target;
            deceleration = dec;
        }
        

        public override Vector2D Calculate()
        {
            if (TargetPos == null)
                return new Vector2D(0, 0);

            Vector2D ToTarget = TargetPos - ME.Pos;

            //Calculate the distance to the target position
            double distance = ToTarget.Length();

            if (distance > 0)
            {
                //Because Deceleration is enumered as an int, this value is required
                //to provide fine tweaking of the deceleration.
                //const double DecelerationTweaker = 2.0;
                const double DecelerationTweaker = 0.3;

                //Calculate the speed required to reach the target given the desired deceleration
                double speed = distance/((double) deceleration*DecelerationTweaker);


                //Make sure the velocity does not exceed the max
                if (speed > ME.MaxSpeed)
                    speed = ME.MaxSpeed;
                speed = Min(speed, ME.MaxSpeed);

                //From here proceed just like Seek except we don't
                //need to normalize the ToTarget vector because we 
                //have already gone to the trouble of calculating
                //its length (double distance = ToTarget.Length() )
                Vector2D DesiredVelocity = ToTarget*speed/distance;

                return (DesiredVelocity - ME.Velocity);
            }

            return new Vector2D(0, 0);
        }



        public double Min(double value1, double value2)
        {
            return ((value1 < value2) ? value1 : value2);
        }
    }
}
