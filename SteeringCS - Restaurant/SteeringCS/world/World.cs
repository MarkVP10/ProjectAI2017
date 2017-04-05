using SteeringCS.behaviour;
using SteeringCS.entity;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using SteeringCS.util;
using SteeringCS.graph;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using SteeringCS.goal_driven_behaviour.CompositeGoals;

namespace SteeringCS
{
    class World
    {
        private static readonly double graphNodeSeperationFactor = 30;
        private readonly Graph restaurandWallGraph = new Graph(graphNodeSeperationFactor);
        private readonly Graph restaurandFloorGraph = new Graph(graphNodeSeperationFactor);
        private AStarRemnant AStar_FirstRemnant;

        public bool graphVisible = false;


        private List<MovingEntity> entities = new List<MovingEntity>();
        private List<BaseGameEntity> obstacles = new List<BaseGameEntity>(); 

        public Vehicle Target { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public readonly int RestaurantWidth = 34; //amount of nodes that span the width of the restaurant
        public readonly int RestaurantHeight = 28;//amount of nodes that span the height of the restaurant






        public World(int w, int h)
        {
            TalkToCustomer goal = new TalkToCustomer();
            goal.Activate();
            goal.Process();


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
            //entities.Add(c2);

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
            //todo: uncomment
            //new GroupFollowingBehaviour().Group(timeElapsed, entities, Target);

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
                restaurandWallGraph.DrawGraph(g, Color.Black);
                restaurandFloorGraph.DrawGraph(g, Color.SteelBlue);

                AStar_FirstRemnant?.Draw(g); //Null Propogation

                /* Same as:
                    if(AStar_FirstRemnant != null)
                    {
                        AStar_FirstRemnant?.Draw(g);
                    }
                */
            }

            //Todo: edit render function to draw all Agents(entities) and StaticObjects(obstacles).
            entities.ForEach(e => e.Render(g));
            Target.Render(g);
            obstacles.ForEach(o => o.Render(g));
        }


        //Needed for object avoidance detection
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
            List<Vertex> restaurantFloorNodes = CreateNodesFromFile(@"Data\RestaurantNodes.txt");
            List<Vertex> restaurantWallNodes = CreateNodesFromFile(@"Data\RestaurantWallNodes.txt");


            restaurandWallGraph.AddVertecis(restaurantWallNodes);
            restaurandFloorGraph.AddVertecis(restaurantFloorNodes);
            GenerateWallEdges();
            GenerateFloorEdges();



            //todo: Remove this A* call
            string start = "3024";
            string end = "1117";
            //string start = "1117";
            //string end = "3103";
            AStarRemnant pathFindingRemnant = restaurandFloorGraph.AStar(start, end);
            AStar_FirstRemnant = pathFindingRemnant;
        }


        /*
         How to call A* example:
            //string start = "3024";
            //string end = "1117";
            string start = "1117";
            string end = "3103";
            AStarRemnant pathFindingRemnant = restaurandFloorGraph.AStar(start, end);
            AStar_FirstRemnant = pathFindingRemnant;

        Debug info:
            AStarRemnant indexRemnant = pathFindingRemnant;
            string message = "List of vertexes to target:";
            while (indexRemnant != null)
            {
                message += ("\r\n [" + indexRemnant.GetPosition().X + ":" + indexRemnant.GetPosition().Y + "]");
                indexRemnant = indexRemnant.GetNext();
            }
            MessageBox.Show(message);
                         */


        /// <summary>
        /// Returns a list of strings that contains only JSON objects and is save to be used for deserialization. (Without worry of syntax errors or empty strings)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private List<string> ReadNodeFile(string fileName)
        {
            //ex. filename = @"Data\RestaurantWallNodes.txt";
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
            string[] lines = File.ReadAllLines(path);
            List<string> nodeLines = new List<string>();


            //todo: test is file is opened/exists before trying to read it. If it can't be found/opened, return an empty list or throw an error.


            foreach (string s in lines)
            {
                string line = s.TrimStart();
                if(line.Length == 0)
                    continue;
                char firstChar = line.Substring(0, 1).ToCharArray()[0];
                if (firstChar == '{')
                    nodeLines.Add(line);
            }

            return nodeLines;
        }
        private List<Vertex> CreateNodesFromFile(string filename)
        {
            List<Vertex> nodeList = new List<Vertex>();
            List<string> fileNodeList = ReadNodeFile(filename);


            //Convert JSON objects from the file to Vertex node objects.
            foreach (string s in fileNodeList)
            {
                Vertex v = JsonConvert.DeserializeObject<Vertex>(s);
                v = new Vertex(v.X, v.Y, graphNodeSeperationFactor);
                nodeList.Add(v);
            }

            return nodeList;
        }



        private void GenerateWallEdges()
        {
            restaurandWallGraph.AddMultiEdge("0000", "0027");//TopLeft to BottomLeft
            restaurandWallGraph.AddMultiEdge("0000", "3300");//TopLeft to TopRight
            restaurandWallGraph.AddMultiEdge("0027", "3327");//BottomLeft to BottomRight
            restaurandWallGraph.AddMultiEdge("3300", "3327");//TopRight to BottomRight

            restaurandWallGraph.AddMultiEdge("0006", "2506");//kitchenBottomLeft to kitchenBottomRight

            restaurandWallGraph.AddMultiEdge("2500", "2503");//kitchenTopRight to toiletMaleBottomLeft
            restaurandWallGraph.AddMultiEdge("2900", "2903");//toiletMaleTopRight to toiletMaleBottomRight

            restaurandWallGraph.AddMultiEdge("2927", "2922");//receptionBottomLeft to receptionTopLeft
        }
        private void GenerateFloorEdges()
        {
            Vertex indexVertex = null;
            Vertex belowVertex = null;
            Vertex rightVertex = null;
            Vertex upperVertex = null;
            Vertex NE_Vertex = null;
            Vertex SE_Vertex = null;

            for (int x = 0; x < RestaurantWidth; x++)
            {
                for (int y = 0; y < RestaurantHeight; y++)
                {
                    indexVertex =
                        restaurandFloorGraph.GetVertexByName(Utility.LeadZero(x) + Utility.LeadZero(y));
                    if (indexVertex == null)
                        continue;

                    belowVertex = restaurandFloorGraph.GetVertexByName(Utility.LeadZero(x) + Utility.LeadZero(y + 1));
                    rightVertex = restaurandFloorGraph.GetVertexByName(Utility.LeadZero(x + 1) + Utility.LeadZero(y));



                    //Vertical: South(Down)
                    if (belowVertex != null)
                        restaurandFloorGraph.AddMultiEdge(indexVertex.Name, belowVertex.Name);

                    //Horizontal & Diagonal
                    if (rightVertex != null)
                    {
                        //Horizontal: East(Right)
                        restaurandFloorGraph.AddMultiEdge(indexVertex.Name, rightVertex.Name);

                        //Diagonal: NorthEast
                        upperVertex = restaurandFloorGraph.GetVertexByName(Utility.LeadZero(x) + Utility.LeadZero(y - 1));
                        if (upperVertex != null)
                        {
                            NE_Vertex =
                                restaurandFloorGraph.GetVertexByName(Utility.LeadZero(x + 1) + Utility.LeadZero(y - 1));
                            if(NE_Vertex != null)
                                restaurandFloorGraph.AddMultiEdge(indexVertex.Name, NE_Vertex.Name);
                        }

                        //Diagonal: SouthEast
                        if (belowVertex != null)
                        {
                            SE_Vertex = restaurandFloorGraph.GetVertexByName(Utility.LeadZero(x + 1) + Utility.LeadZero(y + 1));
                            if (SE_Vertex != null)
                                restaurandFloorGraph.AddMultiEdge(indexVertex.Name, SE_Vertex.Name);
                        }
                    }
                }
            }

        }


    }
}
