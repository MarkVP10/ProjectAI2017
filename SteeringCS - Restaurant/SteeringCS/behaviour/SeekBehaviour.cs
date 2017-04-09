using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class SeekBehaviour : SteeringBehaviour 
    {
        public Vector2D Target { get; set; }

        
        public SeekBehaviour(MovingEntity me, Vector2D target = null) : base(me)
        {
            Target = target;
        }
        

        public override Vector2D Calculate()
        {
            if(Target == null)
                return  new Vector2D(0,0);

            //The vector from the Entity to the Target
            Vector2D desiredVelocity = (Target - ME.Pos).Normalize() * ME.MaxSpeed;
            
            //The entity has a certain velocity already. This will get the vector that is needed to 'redirect' the the entity to get to its target.
            //return (desiredVelocity - ME.Velocity).truncate(ME.MaxForce);
            return (desiredVelocity - ME.Velocity);
        }
    }
}
