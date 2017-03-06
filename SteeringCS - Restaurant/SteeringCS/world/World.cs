using SteeringCS.behaviour;
using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS
{
    class World
    {
        private List<MovingEntity> entities = new List<MovingEntity>();
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
            Vehicle v = new Vehicle(new Vector2D(200,200), this);
            v.VColor = Color.Blue;
            entities.Add(v);

            Waitress w1 = new Waitress(new Vector2D(300, 300), this);
            entities.Add(w1);
            Customer c1 = new Customer(new Vector2D(500, 600), this);
            entities.Add(c1);
            Customer c2 = new Customer(new Vector2D(700, 800), this);
            entities.Add(c2);


            Target = new Vehicle(new Vector2D(300, 400), this);
            Target.VColor = Color.DarkRed;
            Target.Pos = new Vector2D(100, 40);
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

                // me.SB = new SeekBehaviour(me); // restore later
                me.Update(timeElapsed);
            }
        }

        public void Render(Graphics g)
        {
            entities.ForEach(e => e.Render(g));
            Target.Render(g);
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
