using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.behaviour
{
    class ObstacleAvoidance
    {
        private const double MinDetectionBoxLength = 40.0;


        public MovingEntity ME;
        public ObstacleAvoidance(MovingEntity me)
        {
            ME = me;
        }


        public Vector2D Calculate()
        {
            //The detection box length is proportional to the agent's velocity.
            double DetectionBoxLength = MinDetectionBoxLength + (ME.Velocity.Length()/ME.MaxSpeed)*MinDetectionBoxLength;


            DetectionBoxLength += ME.Scale; //Added, so that when an entity is REALY large, this will still work...
            List<BaseGameEntity> entitiesInRange = ME.MyWorld.GetAllObstaclesInRange(ME, DetectionBoxLength);




            //This will keep track of the closest intersecting obstacle (CIO)
            BaseGameEntity ClosestIntersectingObstacle = null;
            
            //This will we used to track the distance to the CIO (only keeps track of the distance on the x-axis)
            double DistanceToClosestIntersectionPoint = Double.MaxValue;
            
            //This will record the transformed local coordinates of the CIO
            Vector2D LocalPosOfClosestObstacle = new Vector2D();



            //Get the angle, so that it can be used for the rotation matrix.
            float angleNeededToPointHeaderToInfinitePositiveX = (90 - Vector2D.ToDegrees(ME.HeadingVector)) % 360;
            if (angleNeededToPointHeaderToInfinitePositiveX < 0)
                angleNeededToPointHeaderToInfinitePositiveX += 360;
            Matrix2D rotateMatrix2D = Matrix2D.RotateClockWise(angleNeededToPointHeaderToInfinitePositiveX);
            


            //todo remove console.writeline
            //Console.WriteLine(angleNeededToPointHeaderToInfinitePositiveX);
            //Console.WriteLine(ME.HeadingVector);
            //Console.WriteLine(Vector2D.ToDegrees(ME.HeadingVector));

            foreach (BaseGameEntity obstacle in entitiesInRange)
            {
                //Get the position
                Vector2D toTarget = obstacle.Pos - ME.Pos;
                double vectorLength = toTarget.Length();


                //todo remove
                //Console.WriteLine("VECTOR TO TARGET: " + toTarget.ToString());


                //Convert position to local (Basicaly you turn it around.) 
                //EXPLANATION: Because you are rotating the header vector a certain degrees, all other items also need to rotate with it. This is to make calculating the bounding box collision easier.
                Vector2D LocalPos = rotateMatrix2D*toTarget;
                //MessageBox.Show("difference:" + LocalPos.Length() + " - " + vectorLength);

                //Only allow positive-x coordinates. (The vectors with negative X are behind the entity, and therefore need not to be checked.)
                if(LocalPos.X < 0)
                    continue;


                //Find if local-y is higher than boundingHeight. (Only allow the ones that intersect with the bounding box on two points. If they only intersect on 1 point, it will look like the entity is grazing the obstacle.)
                double localY = LocalPos.Y < 0 ? -LocalPos.Y : LocalPos.Y; //Always get a positive localY
                if(localY >= ME.Scale + obstacle.Scale)
                    continue;


                //todo remove
                Console.WriteLine("WARNING! OBJECT IN RANGE! Location:" + LocalPos);

                //Find and save intersection
                double cX = LocalPos.X;
                double cY = LocalPos.Y;
                double extendedRadius = ME.Scale + obstacle.Scale;

                double SquareRootForIntersection = Math.Sqrt((extendedRadius*extendedRadius) - (cY*cY));

                double intersectionPoint = cX - SquareRootForIntersection; //Left most intersection point
                if (intersectionPoint <= 0) //If the point is behind the entity, choose the right most intersection point
                    intersectionPoint = cX + SquareRootForIntersection;

                if (intersectionPoint < DistanceToClosestIntersectionPoint)
                {
                    DistanceToClosestIntersectionPoint = intersectionPoint;
                    ClosestIntersectingObstacle = obstacle;
                    LocalPosOfClosestObstacle = LocalPos;
                }
                //if(intersect && intersect.distance < distancetoclosestintercetionpoint)
                //distancetoclosestintersectionpoint = intersect
            }




            //Create a new vector for returning stuff.
            Vector2D SteeringForce = new Vector2D();

            //If we found an intersection, assign force to the SteeringForce vector
            if (ClosestIntersectingObstacle != null)
            {
                //The closer the obstable is to the agent, the stronger the steering force must be
                double multiplier = 1.0 + (DetectionBoxLength - LocalPosOfClosestObstacle.X)/DetectionBoxLength;

                //Calculate the lateral steering force
                SteeringForce.Y = (ClosestIntersectingObstacle.Scale - LocalPosOfClosestObstacle.Y)*multiplier;
                

                //Apply a braking force proportional to the obstable's distance from the vehicle
                const double BrakingWeight = 0.2;
                SteeringForce.X = (ClosestIntersectingObstacle.Scale - LocalPosOfClosestObstacle.X)*BrakingWeight;


                //Turn the vector, so that it can be used in the world
                Matrix2D counterClockMatrix2D = Matrix2D.RotateCounterClockWise(angleNeededToPointHeaderToInfinitePositiveX);
                SteeringForce = counterClockMatrix2D*SteeringForce;
            }



            return SteeringForce;
        }
    }
}
