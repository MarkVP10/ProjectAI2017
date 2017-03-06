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
using SteeringCS.graph;

namespace SteeringCS
{
    class World
    {
        private Graph graph = new Graph();
        public bool graphVisible = false;
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
            GenerateGraph();
        }

        private void populate()
        {
            Vehicle v = new Vehicle(new Vector2D(200,100), this);
            v.VColor = Color.Blue;
            //entities.Add(v);

            Waitress w1 = new Waitress(new Vector2D(100, 200), this);
            //entities.Add(w1);
            Customer c1 = new Customer(new Vector2D(100, 300), this);
            //entities.Add(c1);
            Customer c2 = new Customer(new Vector2D(100, 400), this);
           // entities.Add(c2);



            Target = new Vehicle(new Vector2D(), this);
            Target.VColor = Color.DarkRed;
            Target.Pos = new Vector2D(200, 200);


            BasicCircularObstacle b1 = new BasicCircularObstacle(new Vector2D(300,300), this);
            b1.Scale = 10;
            //obstacles.Add(b1);
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
            if (graphVisible)
            {
                graph.DrawGraph(g);
            }
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

        private void GenerateGraph()
        {

            GenerateVertices();
            AddEdges();
            
        }

        private void GenerateVertices()
        {
            //Row1 of graph
            graph.AddVertex("Node11", 10f, 100f);
            graph.AddVertex("Node12", 100f, 100f);
            graph.AddVertex("Node13", 200f, 100f);
            graph.AddVertex("Node14", 400f, 100f);
            graph.AddVertex("Node15", 500f, 100f);
            graph.AddVertex("Node16", 600f, 100f);

            //Row 2
            graph.AddVertex("Node21", 10f, 200f);
            graph.AddVertex("Node22", 100f, 200f);
            graph.AddVertex("Node23", 200f, 200f);
            graph.AddVertex("Node24", 400f, 200f);
            graph.AddVertex("Node25", 500f, 200f);
            graph.AddVertex("Node26", 600f, 200f);

            //Row 3
            graph.AddVertex("Node31", 10f, 300f);
            graph.AddVertex("Node32", 100f, 300f);
            graph.AddVertex("Node33", 200f, 300f);
            graph.AddVertex("Node34", 400f, 300f);
            graph.AddVertex("Node35", 500f, 300f);
            graph.AddVertex("Node36", 600f, 300f);


            //Row 4
            graph.AddVertex("Node41", 10f, 400f);
            graph.AddVertex("Node42", 100f, 400f);
            graph.AddVertex("Node43", 200f, 400f);
            graph.AddVertex("Node4x", 300f, 400f);
            graph.AddVertex("Node44", 400f, 400f);
            graph.AddVertex("Node45", 500f, 400f);
            graph.AddVertex("Node46", 600f, 400f);
        }

        private void AddEdges()
        {
            //Add edges
            //1.1
            graph.AddMultiEdge("Node11", "Node12");
            graph.AddMultiEdge("Node11", "Node21");
            graph.AddMultiEdge("Node11", "Node22");

            //1.2
            graph.AddMultiEdge("Node12", "Node22");
            graph.AddMultiEdge("Node12", "Node13");
            graph.AddMultiEdge("Node12", "Node23");
            graph.AddMultiEdge("Node12", "Node21");

            //1.3
            graph.AddMultiEdge("Node13", "Node23");
            graph.AddMultiEdge("Node13", "Node22");

            //2.1
            graph.AddMultiEdge("Node21", "Node22");
            graph.AddMultiEdge("Node21", "Node31");
            graph.AddMultiEdge("Node21", "Node32");


            //2.2
            graph.AddMultiEdge("Node22", "Node23");
            graph.AddMultiEdge("Node22", "Node32");
            graph.AddMultiEdge("Node22", "Node33");
            graph.AddMultiEdge("Node22", "Node31");

            //2.3
            graph.AddMultiEdge("Node23", "Node33");
            graph.AddMultiEdge("Node23", "Node32");

            //3.1
            graph.AddMultiEdge("Node31", "Node32");
            graph.AddMultiEdge("Node31", "Node41");
            graph.AddMultiEdge("Node31", "Node42");

            //3.2
            graph.AddMultiEdge("Node32", "Node33");
            graph.AddMultiEdge("Node32", "Node42");
            graph.AddMultiEdge("Node32", "Node43");
            graph.AddMultiEdge("Node32", "Node41");

            //3.3
            graph.AddMultiEdge("Node33", "Node43");
            graph.AddMultiEdge("Node33", "Node42");
            graph.AddMultiEdge("Node33", "Node4x");

            //4 4.1 to 4.x
            graph.AddMultiEdge("Node41", "Node42");
            graph.AddMultiEdge("Node42", "Node43");
            graph.AddMultiEdge("Node43", "Node4x");

            //4.x
            graph.AddMultiEdge("Node4x", "Node44");
            graph.AddMultiEdge("Node4x", "Node34");

            //1.4
            graph.AddMultiEdge("Node14", "Node15");
            graph.AddMultiEdge("Node14", "Node24");
            graph.AddMultiEdge("Node14", "Node25");

            //1.5
            graph.AddMultiEdge("Node15", "Node16");
            graph.AddMultiEdge("Node15", "Node25");
            graph.AddMultiEdge("Node15", "Node26");
            graph.AddMultiEdge("Node15", "Node24");

            //1.6
            graph.AddMultiEdge("Node16", "Node26");
            graph.AddMultiEdge("Node16", "Node25");

            //2.4
            graph.AddMultiEdge("Node24", "Node25");
            graph.AddMultiEdge("Node24", "Node34");
            graph.AddMultiEdge("Node24", "Node35");

            //2.5
            graph.AddMultiEdge("Node25", "Node26");
            graph.AddMultiEdge("Node25", "Node35");
            graph.AddMultiEdge("Node25", "Node36");
            graph.AddMultiEdge("Node25", "Node34");

            //2.6
            graph.AddMultiEdge("Node26", "Node36");
            graph.AddMultiEdge("Node26", "Node35");

            //3.4
            graph.AddMultiEdge("Node34", "Node35");
            graph.AddMultiEdge("Node34", "Node44");
            graph.AddMultiEdge("Node34", "Node45");

            //3.5
            graph.AddMultiEdge("Node35", "Node36");
            graph.AddMultiEdge("Node35", "Node45");
            graph.AddMultiEdge("Node35", "Node46");
            graph.AddMultiEdge("Node35", "Node44");

            //2.6
            graph.AddMultiEdge("Node36", "Node46");
            graph.AddMultiEdge("Node36", "Node45");

            //4 the underline
            graph.AddMultiEdge("Node44", "Node45");
            graph.AddMultiEdge("Node45", "Node46");
        }

    }
}
