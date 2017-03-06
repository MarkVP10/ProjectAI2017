using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class QueueBehaviour 
    {
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
