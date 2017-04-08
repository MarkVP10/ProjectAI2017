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
using SteeringCS.world;

namespace SteeringCS
{
    class World
    {
        public static readonly int graphNodeSeperationFactor = 30;
        private readonly Graph restaurandWallGraph = new Graph(graphNodeSeperationFactor);
        public readonly Graph restaurandFloorGraph = new Graph(graphNodeSeperationFactor);
        

        public bool graphVisible = true; //D
        public bool pathVisible = true; //F
        public bool goalsVisible = false; //G
        private readonly Font goalFont = new Font(new FontFamily("Arial"), 8, FontStyle.Regular);


        private readonly List<MovingEntity> entities = new List<MovingEntity>();
        private readonly List<BaseGameEntity> obstacles = new List<BaseGameEntity>();
        private readonly List<SentientTable> tables = new List<SentientTable>();
        private readonly List<Room> rooms = new List<Room>(); 
        public Manager TheBoss;

        public Vehicle Target { get; set; } //The player's last clicked location
        public int Width { get; set; }
        public int Height { get; set; }
        public readonly int RestaurantWidth = 34; //amount of nodes that span the width of the restaurant
        public readonly int RestaurantHeight = 28;//amount of nodes that span the height of the restaurant

        




        public World(int w, int h)
        {
            Width = w;
            Height = h;
            populate();
            GenerateGraph();
        }

        private void populate()
        {
            //Waitresses
            Waitress w1 = new Waitress(new Vector2D(100, 200), this);
            w1.combineStratagy.SwitchBehaviour(CombineForces.Behaviours.Wander);
            entities.Add(w1);

            Waitress w2 = new Waitress(new Vector2D(150, 200), this);
            w2.combineStratagy.SwitchBehaviour(CombineForces.Behaviours.Wander);
            entities.Add(w2);

            Waitress w3 = new Waitress(new Vector2D(200, 200), this);
            w3.combineStratagy.SwitchBehaviour(CombineForces.Behaviours.Wander);
            entities.Add(w3);

            Waitress w4 = new Waitress(new Vector2D(250, 200), this);
            w4.combineStratagy.SwitchBehaviour(CombineForces.Behaviours.Wander);
            entities.Add(w4);



            //Manager
            TheBoss = new Manager(new Vector2D(300, 300), this);
            entities.Add(TheBoss);



            //todo make customers
            //Customer c1 = new Customer(new Vector2D(100, 300), this);
            //entities.Add(c1);
            //Customer c2 = new Customer(new Vector2D(100, 400), this);
            //entities.Add(c2);

            Target = new Vehicle(new Vector2D(), this);
            Target.VColor = Color.DarkRed;
            Target.Pos = new Vector2D(-50, -50);
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
                me.Update(timeElapsed);
                //me.Update(timeElapsed); //Double update??
            }    
        }

        public void Render(Graphics g)
        {
            //todo: Draw rooms
            //Kitchen
            //BathroomMale
            //BathroomFemale
            //Reception
            //Dining Area
            //WalkingArea
            foreach (Room room in rooms)
            {
                g.FillRectangle(new SolidBrush(room.color), room.GetRectangle());
                g.DrawString(room.name, goalFont, Brushes.Black, room.GetCenterPosition().ToPointF());
            }

            //Draw the Walls
            restaurandWallGraph.DrawGraph(g, Color.Black, 2f);

            //Draw the graph, when needed
            if (graphVisible)
                restaurandFloorGraph.DrawGraph(g, Color.SteelBlue);



            
            obstacles.ForEach(o => o.Render(g));
            entities.ForEach(e => e.Render(g));
            Target.Render(g);


            //Draw the paths for entities if they have them, when needed
            if (pathVisible)
            {
                foreach (MovingEntity entity in entities)
                {
                    entity.PathToTarget?.Draw(g);
                }
            }


            //Draw the goals for all entities, when needed
            if (goalsVisible)
            {
                int goalDraw_xCoord = 0;
                int goalDraw_yCoord = 0;

                foreach (MovingEntity entity in entities)
                {
                    List<string> entityGoals = entity.Brain.GetCompositeGoalAsStringList();
                    goalDraw_xCoord = (int)(entity.Pos.X + entity.Scale + 5);
                    goalDraw_yCoord = (int)entity.Pos.Y;
                    foreach (string s in entityGoals)
                    {
                        g.DrawString(s, goalFont, Brushes.DarkSlateGray, goalDraw_xCoord, goalDraw_yCoord);
                        goalDraw_yCoord += 8;
                    }
                }
            }

            
        }


        //Get a random unfilled table
        public Table GetRandomFilledTable()
        {
            List<Table> unfilledTables = (from t in tables where t.table.HasCustomers select t.table).ToList();
            if (unfilledTables.Count == 0)
                return null;

            Random rng = new Random();
            return unfilledTables[rng.Next(0, unfilledTables.Count)];
        }



        //Needed for object avoidance detection
        public List<BaseGameEntity> GetAllWorldObstacles()
        {
            //todo: remove this function, seeing as we don;t use Obstacle Detection
            return obstacles;
        }
        public List<BaseGameEntity> GetAllObstaclesInRange(MovingEntity entity, double radius)
        {
            //todo: remove this function, seeing as we don;t use Obstacle Detection
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
        



        private void GenerateGraph()
        {
            List<Vertex> restaurantFloorNodes = CreateNodesFromFile(@"Data\RestaurantNodes.txt");
            List<Vertex> restaurantWallNodes = CreateNodesFromFile(@"Data\RestaurantWallNodes.txt");


            restaurandWallGraph.AddVertecis(restaurantWallNodes);
            restaurandFloorGraph.AddVertecis(restaurantFloorNodes);
            GenerateWallEdges();
            GenerateFloorEdges();
            GenerateTables();
            GenerateRooms();
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




        private void GenerateTables()
        {
            List<string> tableInJSON = ReadNodeFile(@"Data\RestaurantTablePosition.txt");

            foreach (string s in tableInJSON)
            {
                //Not yet safe... Needs a try-catch
                Table t = JsonConvert.DeserializeObject<Table>(s);
                t = new Table(t.X, t.Y, t.IsFourPerson);



                //todo remove this!!!
                t.HasCustomers = true;

                SentientTable st =
                    new SentientTable(
                        new Vector2D(t.X*graphNodeSeperationFactor, t.Y*graphNodeSeperationFactor), 
                        this,
                        t, 
                        graphNodeSeperationFactor);

                obstacles.Add(st);
                tables.Add(st);
            }
        }



        private void GenerateRooms()
        {
            rooms.Add(new Room(0,  0,  25, 6,  Color.DarkOrange, "Kitchen"));
            rooms.Add(new Room(25, 0,  29, 3,  Color.RoyalBlue, "Toilet ♂"));
            rooms.Add(new Room(29, 0,  33, 3,  Color.RoyalBlue, "Toilet ♀"));
            rooms.Add(new Room(29, 22, 33, 27, Color.Peru, "Reception"));
            rooms.Add(new Room(0,  6,  29, 27, Color.Pink, "Dining"));
            rooms.Add(new Room(25, 3,  33, 6,  Color.OrangeRed, "Walkway"));
            rooms.Add(new Room(29, 6,  33, 22, Color.OrangeRed, "Walkway"));
        }





        /// <summary>
        /// Returns a list of strings that contains only JSON objects and is save to be used for deserialization. (Without worry of syntax errors or empty strings).
        /// For this function to recognize JSON objects, the first character on the line must be a '{'.
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
