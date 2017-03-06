using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class GroupFollowingBehaviour
    {
        public void Group(float timeElapsed, List<MovingEntity> entities, Vehicle target)
        {
            MovingEntity leader = null;
            MovingEntity[] followers = new MovingEntity[entities.Count() - 1];
            int index = 0;
            foreach (MovingEntity me in entities)
            {
                if (leader == null)
                {

                    me.combineStratagy.SetTarget(target.Pos);

                    me.Update(timeElapsed);
                    leader = me;

                }
                else
                {
                    followers[index] = me;
                    index++;
                }
            }

            this.UpdateFollowers(followers, leader, timeElapsed);
        }

        private void UpdateFollowers(MovingEntity[] followers, MovingEntity leader, float timeElapsed)
        {
            int numFollowers = followers.Count();
            int margin = 40;
            for (int i = 0; i < numFollowers; i++)
            {
                //if i is less than three we have to iniate the group positions.
                if (i < 3)
                {
                    if (i == 0)
                    {
                        followers[i].combineStratagy.SetTarget(leader.Pos);
                        followers[i].Update(timeElapsed);
                    }
                    else if (i == 1)
                    {
                        var posRight = leader.Pos.Clone();
                        posRight.X = posRight.X + margin;
                        followers[i].combineStratagy.SetTarget(posRight);
                        followers[i].Update(timeElapsed);
                    }
                    else
                    {
                        var posLeft = leader.Pos.Clone();
                        posLeft.X = posLeft.X - margin;
                        followers[i].combineStratagy.SetTarget(posLeft);
                        followers[i].Update(timeElapsed);
                    }
                }
                else
                {
                    followers[(i)].combineStratagy.SetTarget(followers[(i - 3)].Pos);
                    followers[i].Update(timeElapsed);
                }

            }
        }
    }
}
