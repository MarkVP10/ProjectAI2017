using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteeringCS.util;

namespace SteeringCS.graph
{
    class Graph
    {
        private readonly Dictionary<string, Vertex> graphMap = new Dictionary<string, Vertex>();
        
        
        public void AddVertex(Vertex v)
        {
            //v.Name contains the tile number and nothing else. ex. v.Name = "2606"; 26 is the x-node and 6 is the y-node on the graph.
            graphMap.Add(v.Name, v);
        }

        public void AddVertecis(List<Vertex> vl)
        {
            foreach (Vertex vertex in vl)
                AddVertex(vertex);
        }

        public void AddEdge(string sourceName, string destinationName, bool IsDiagonal = false)
        {
            Vertex source = GetVertex(sourceName);
            Vertex destination = GetVertex(destinationName);

            
            if (source == null || destination == null)
            {
                //throw new NullReferenceException("Can't add Egde when the source or destination Vertex is NULL.");
                return;
            }
                

            source.Adjacent.Add(new Edge(destination, IsDiagonal));
        }
        public void AddMultiEdge(string sourceName, string destName, bool IsDiagonal = false)
        {
            AddEdge(sourceName, destName, IsDiagonal);
            AddEdge(destName, sourceName, IsDiagonal);
        }





        /// <summary>
        /// If you only have the world coordinates of a start and end position and want to get the Vertices of both at the same time.
        /// Usefull if you want to use A* next.
        /// </summary>
        /// <param name="xStart">X coordinate of the start position in the world</param>
        /// <param name="yStart">Y coordinate of the start position in the world</param>
        /// <param name="xTarget">X coordinate of the end position in the world</param>
        /// <param name="yTarget">Y coordinate of the end position in the world</param>
        /// <returns>A list of two Vertices. The first index is the start Vertex, the second index is the end Vertex.</returns>
        public List<Vertex> PrepareAStarUsingWorldPosition(int xStart, int yStart, int xTarget, int yTarget)
        {
            List<Vertex> resultList = new List<Vertex>();

            //Find the names of the nodes closest to the given coordinates
            resultList.Add(FindClosestNodeToWorldCoords(xStart, yStart));
            resultList.Add(FindClosestNodeToWorldCoords(xTarget, yTarget));

            return resultList;
        }


        public Vertex FindClosestNodeToWorldCoords(int xCoord, int yCoord)
        {
            Vertex returnVertex = null;
            double delta = Int32.MaxValue;
            Vector2D inWorldVector = new Vector2D(xCoord, yCoord);
            

            //Search all nodes - inefficient, but it works
            foreach (KeyValuePair<string, Vertex> pair in graphMap)
            {
                double diff = (pair.Value.Pos - inWorldVector).Length();
                if (diff < delta)
                {
                    returnVertex = pair.Value;
                    delta = diff;
                }
            }


            return returnVertex;
        }


        




        private Vertex GetVertex(string targetName)
        {
            //Find the vertex in the dictionary
            if (graphMap.ContainsKey(targetName))
                return graphMap[targetName];
            
            //Nothing found
            return null;
        }


        //Gets a random Vertex from the graph map
        public Vertex GetRandomVertex()
        {
            if (graphMap.Count == 0)
                return null;
            Random rng = new Random();
            return graphMap.Values.ToList()[rng.Next(0, graphMap.Count)];
        }
        


        public void DrawGraph(Graphics g, Color? color = null, float edgeSize = 1f)
        {
            Color trueColor = color ?? Color.Gray;

            var penBox = new Pen(trueColor, 4f);
            var penLine = new Pen(trueColor, edgeSize);
            foreach (Vertex vertex in graphMap.Values)
            {
                g.DrawEllipse(penBox, new RectangleF((float)vertex.Pos.X - 2f, (float)vertex.Pos.Y - 2f, 4, 4));
                foreach (Edge edge in vertex.Adjacent)
                {
                    //Note that all nodes will be drawn, even the ones that are already drawn. This is due to the Edges being multidirectional and stored in seperate Vertecis.
                    g.DrawLine(penLine, (float)vertex.Pos.X, (float)vertex.Pos.Y, (float)edge.Destination.Pos.X, (float)edge.Destination.Pos.Y);
                }
            }
        }



        public Vertex GetVertexByName(string nameOfVertex)
        {
            return GetVertex(nameOfVertex);
        }



        public AStarRemnant AStar(string startName, string targetName)
        {
            return AStar(GetVertex(startName), GetVertex(targetName));
        }
        public AStarRemnant AStar(Vertex start, Vertex target)
        {
            //If no valid start or target are given, return with null
            if (start == null || target == null)
                return null;

            //Set the values for NonDiagonal and Diagonal travel.
            int StepIncrementNonDiagonal = 1;
            double StepIncrementDiagonal = Math.Sqrt(2);
            

            List<Vertex> visitedVertices = new List<Vertex>();
            PriorityQueue_Vertex queue = new PriorityQueue_Vertex();
            
            //Set the values for the first loop
            start.Seen = true;
            start.StepCount = 0;
            start.Target = target;
            queue.Add(start);

            //Go through every node that is connected to the starter node to find the target node
            while (!queue.IsEmpty())
            {
                Vertex currentVertex = queue.Pop();
                currentVertex.Seen = true;
                visitedVertices.Add(currentVertex);

                if (currentVertex == target)
                    break;

                foreach (Edge edge in currentVertex.Adjacent)
                {
                    //If already seen, no need to do anything.
                    if (edge.Destination.Seen)
                        continue;

                    double StepIncrement = edge.IsDiagonal ? StepIncrementDiagonal : StepIncrementNonDiagonal;

                    //Stepcount update
                    if (edge.Destination.StepCount >= currentVertex.StepCount + StepIncrement)
                    {
                        //Update variables
                        edge.Destination.Previous = currentVertex;
                        edge.Destination.StepCount = currentVertex.StepCount + StepIncrement;
                        edge.Destination.Target = target;
                        //Re-add to queue
                        queue.UpdateQueue(edge.Destination);
                    }
                }
            }


            //Add all items in the priotity queue to the visitedList
            Vertex insertionVertex = queue.Pop();
            while (insertionVertex != null)
            {
                visitedVertices.Add(insertionVertex);
                insertionVertex = queue.Pop();
            }


            //Create the returning remnant
            AStarRemnant theFirstRemnant = Utility.ListOfVertexToRemnants(visitedVertices, target, start);

            //Clean up the vertecis in the graph to be used for A* again
            visitedVertices.ForEach(vertex => vertex.ResetPath()); //Reset all vertexes so they can be used again.

            //Return the remnant containing all the other remnants
            return theFirstRemnant;
        }


    }
}
