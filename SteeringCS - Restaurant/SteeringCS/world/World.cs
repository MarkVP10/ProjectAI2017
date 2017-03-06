using SteeringCS.behaviour;
using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteeringCS.util;

namespace SteeringCS
{
    class World
    {
        private List<MovingEntity> entities = new List<MovingEntity>();
        private List<BaseGameEntity> obstacles = new List<BaseGameEntity>(); 
        public Vehicle Target { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public World(int w, int h)
        {
            Width = w;
            Height = h;
            populate();
        }

        private void populate()
        {
            Vehicle v = new Vehicle(new Vector2D(200,100), this);
            v.VColor = Color.Blue;
            entities.Add(v);

            Waitress w1 = new Waitress(new Vector2D(100, 200), this);
            entities.Add(w1);
            Customer c1 = new Customer(new Vector2D(100, 300), this);
            entities.Add(c1);
            Customer c2 = new Customer(new Vector2D(100, 400), this);
            entities.Add(c2);



            Target = new Vehicle(new Vector2D(), this);
            Target.VColor = Color.DarkRed;
            Target.Pos = new Vector2D(200, 200);


            BasicCircularObstacle b1 = new BasicCircularObstacle(new Vector2D(300,300), this);
            b1.Scale = 10;
            obstacles.Add(b1);
        }


        public void Queue(float timeElapsed)
        {
            //Queue behaviour, dont know if this is the right way to call it. It works though.
            //To use this you can call this method in the Update method instead of the current code that is in there.
            new QueueBehaviour().UpdateQueue(timeElapsed, entities, Target);
        }
        public void GroupFollowing(float timeElapsed)
        {
            //Group following behaviour, dont know if this is the right way to call it. It works though.
            //To use this you can call this method in the Update method instead of the current code that is in there.
            new GroupFollowingBehaviour().Group(timeElapsed, entities, Target);
        }


        public void Update(float timeElapsed)
        {
            foreach (MovingEntity me in entities)
            {
                me.combineStratagy.SetTarget(Target.Pos);
                me.Update(timeElapsed);
            }
        }

        public void Render(Graphics g)
        {
            entities.ForEach(e => e.Render(g));
            Target.Render(g);
            obstacles.ForEach(o => o.Render(g));
        }

        public List<BaseGameEntity> GetAllWorldObstacles()
        {
            return obstacles;
        }

        public List<BaseGameEntity> GetAllObstaclesInRange(MovingEntity entity, double radius)
        {
            //Fill the container with obstacles in range
            List<BaseGameEntity> obstaclesWithinRange = new List<BaseGameEntity>();
            foreach (BaseGameEntity obstacle in obstacles)
            {
                Vector2D toTarget = obstacle.Pos - entity.Pos;

                //Test if the obstacle is within bounding radius, if it is, add it to the list
                if (toTarget.Length() - obstacle.Scale < radius + entity.Scale)
                    obstaclesWithinRange.Add(obstacle);
            }

            return obstaclesWithinRange;
        } 

        public void SwitchAgentBehaviour(CombineForces.Behaviours behaviour)
        {
            foreach (MovingEntity me in entities)
            {
                me.combineStratagy.SwitchBehaviour(behaviour);
            }
        }

        public void SetArriveDeceleration(ArriveBehaviour.Deceleration deceleration)
        {
            foreach (MovingEntity me in entities)
            {
                me.combineStratagy.SetArriveDeceleration(deceleration);
            }
        }
    }
}
