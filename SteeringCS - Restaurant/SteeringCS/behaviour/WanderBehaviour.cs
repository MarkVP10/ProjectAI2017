using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteeringCS.entity;

namespace SteeringCS.behaviour
{
    class WanderBehaviour : SteeringBehaviour
    {
        public double wanderRadius; //Radious of the steering circle
        public double wanderDistance; //Distance from the center of the agent to the center of the steering circle
        public double wanderJitter; //Maximum amount of random displacement that can be added to the target each second.
        public double wanderCurrentAngle; //Current angle of wandering


        private const double OneRadian = Math.PI/180;
        private static Random randomizer = new Random();

        
        public WanderBehaviour(MovingEntity me, double radius, double distance, double jitter) : base(me)
        {
            wanderRadius = radius;
            wanderDistance = distance;
            wanderJitter = jitter;
            wanderCurrentAngle = randomizer.Next(0, 360);
            //MessageBox.Show(wanderCurrentAngle.ToString()); //Prints current randomized angle to test if the randomizers are different //todo remove the messagebox.show
        }

        public override Vector2D Calculate()
        {
            //1. Get the vector from Origin to the circle
            //2. Calculate the change in angle (and apply it)
            //3. Get the vector from the circle to the target
            //4. Get the vector from Origin to the target
            //5. Return OriginTargetVector - Entity.Position



            //Creates a vector from Origin to the center of the wander circle
            Vector2D wanderCircle = ME.Pos + (ME.HeadingVector.Clone().Normalize()*wanderDistance);

            //Change the angle. First get a float between 0 and 1. Then multiply by the jitter to get a certain percentage of jitter.
            //Then subtract by half a jitter so that one half is negative and the other half positive.
            //(0)               0% -> -50%  (- jitter/2)
            //(jitter/100*1)    1% -> -49%  (- jitter/50*49)
            //(jitter/4)        25% -> -25% (- jitter/4)
            //(jitter/2)        50% -> +0%  (+ 0)
            //(jitter/4*3)      75& -> +25% (+ jitter/4)
            //(jitter)          100% -> +50%(+ jitter/2)
            wanderCurrentAngle += ((randomizer.NextDouble() * wanderJitter) - (wanderJitter/2));
            wanderCurrentAngle %= 360; //Reset to a readable angle


            //Get the vector from the circle to the target.
            Vector2D wanderCircleTarget = new Vector2D(
                Math.Cos(wanderCurrentAngle * OneRadian),
                Math.Sin(wanderCurrentAngle * OneRadian)
                ).Multiply(wanderRadius);


            //Get the vector from Origin to the actual target.
            Vector2D actualTarget = wanderCircle + wanderCircleTarget;


            //Return the vector from the Entity to the Target.
            return actualTarget - ME.Pos;
        }
    }
}
