using System.Collections.Generic;
using SteeringCS.entity;

namespace SteeringCS.behaviour.unused
{
    class QueueBehaviour 
    {
        //Call this method in the Update. Or make a way to implement this in CombineForces.
        public void UpdateQueue(float timeElapsed, List<MovingEntity> entities, Vehicle target)
        {
            MovingEntity leader = null;
            foreach (MovingEntity me in entities)
            {
                if (leader == null)
                {

                    me.combineStratagy.SetTarget(target.Pos);

                    // me.SB = new SeekBehaviour(me); // restore later
                    me.Update(timeElapsed);
                }
                else
                {
                    me.combineStratagy.SetTarget(leader.Pos);
                    me.Update(timeElapsed);
                }
                leader = me;
            }
        }


    }
}
