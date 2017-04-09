using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;
using SteeringCS.goal_driven_behaviour;
using SteeringCS.goal_driven_behaviour.ThinkGoals;
using SteeringCS.graph;

namespace SteeringCS.entity
{

    abstract class MovingEntity : BaseGameEntity
    {
        private const float SteeringForceTweaker = 200.0f;

        
        public Vector2D Velocity { get; set; }
        public float Mass { get; set; }
        public float MaxSpeed { get; set; }
        public float MaxForce { get; set; }

        
        public CombineForces combineStratagy;
        public readonly Goal_Think Brain;
        public AStarRemnant PathToTarget;

        public Vector2D HeadingVector { get; set; } //Normalized vector pointing in the direction the entity is heading
        public Vector2D SideVector { get; set; } // A vector perpendicular to the heading vector
        public float MaxTurnRate { get; set; }
        public Vector2D SteeringV { get; set; }

        protected MovingEntity(Vector2D pos, World w, Goal_Think brain) : base(pos, w)
        {
            //Current decent parameters. Note: this was difficult to get 'right'. 
            Mass = 1;
            MaxSpeed = 50;
            MaxForce = 2 * SteeringForceTweaker;

            //Vectors
            Velocity = new Vector2D();
            SteeringV = new Vector2D(); //Only used for drawing a green line, indicating the steering of the agent
            HeadingVector = new Vector2D(0,1).Normalize();
            SideVector = HeadingVector.Perpendicular();

            //Behaviour & Brain stuff
            combineStratagy = new CombineForces(this);
            Brain = brain;
            PathToTarget = null;
        }

        public override void Update(float timeElapsed)
        {
            //Process the things in the brain.
            Brain.Process();

            //Calculate the combined force from each steering behavior in the vehicle's list
            Vector2D steeringForce = combineStratagy.calcCombinedForce();
            SteeringV = steeringForce;

            //Acceleration = Force/Mass (a = F/M)
            Vector2D acceletation = steeringForce/Mass;

            //Update velocity (m/s = m/s^2 * s = m/(s*s)*s = m/s)
            Velocity += acceletation*timeElapsed;


            //Make sure the vehicle does not exceed maximum velocity
            Velocity.truncate(MaxSpeed);


            //Update the position
            Pos += Velocity*timeElapsed;



            //Update the heading if the vehicle has a non zero velocity. (A very small number is used, as to not get a 'devide by zero' error. And so that the Heading and Side vectors will always have a value)
            if (Velocity.LengthSquared() > 0.00000001)
            {
                ChangeHeading(Velocity.Clone());
                //HeadingVector = Velocity.Clone().Normalize();
                //SideVector = HeadingVector.Perpendicular();
            }


            WrapAround(Pos, MyWorld.Width, MyWorld.Height);
        }


        public void ChangeHeading(Vector2D velocity)
        {
            HeadingVector = velocity.Clone().Normalize();
            SideVector = HeadingVector.Perpendicular();
        }

        public void WrapAround(Vector2D position, int worldWidth, int worldHeight)
        {
            //if position.x.makePositive > worldWidth/2
            //if position.y.makePositive > worldHeight/2
            if (worldWidth < position.X)
            {
                //out of bound. Right of screen. Need to put to the left.
                position.X -= worldWidth;
            }
            else if (position.X < 0)
            {
                //Out of bounds. Left of screen. Need to put to the right of screen.
                position.X += worldWidth;
            }

            if (worldHeight < position.Y)
            {
                //Out of bound. Top of screen. Need to put to the bottom of the screen.
                position.Y -= worldHeight;
            }
            else if (position.Y < 0)
            {
                //Out of bounds. Bottom of screen. Need to put to the top of screen.
                position.Y += worldHeight;
            }

            Pos = position;
        }


        protected void RenderHeadingAndVelocity(Graphics g)
        {
            //Draw the velocity
            Pen VelocityPen = new Pen(Color.Blue, 2);
            g.DrawLine(VelocityPen, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));

            //Draws the steering vector
            Pen steeringPen = new Pen(Color.Green, 2);
            g.DrawLine(steeringPen, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(SteeringV.X * 2),
                (int)Pos.Y + (int)(SteeringV.Y * 2));
        }
    }
}
