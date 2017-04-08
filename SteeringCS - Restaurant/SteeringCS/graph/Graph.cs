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
        //private List<Vertex> Map = new List<Vertex>();

        private Dictionary<string, Vertex> graphMap = new Dictionary<string, Vertex>();

        public double graphNodeSeperationFactor;

        public Graph(double separationFactor)
        {
            graphNodeSeperationFactor = separationFactor;
        }
        
        
        
        public void AddVertex(Vertex v)
        {
            //v.Name contains the tile number and nothing else. ex. v.Name = "2606"; 26 is the x-coord and 6 is the y-coord
            graphMap.Add(v.Name, v);
        }

        public void AddVertecis(List<Vertex> vl)
        {
            foreach (Vertex vertex in vl)
                AddVertex(vertex);
        }

        public void AddEdge(string sourceName, string destinationName)
        {
            Vertex source = GetVertex(sourceName);
            Vertex destination = GetVertex(destinationName);

            
            if (source == null || destination == null)
            {
                //throw new NullReferenceException("Can't add Egde when the source or destination Vertex is NULL.");
                return;
            }
                

            source.Adjacent.Add(new Edge(destination));
        }
        public void AddMultiEdge(string sourceName, string destName)
        {
            AddEdge(sourceName, destName);
            AddEdge(destName, sourceName);
        }






        public List<Vertex> PrepareAStarUsingWorldPosition(int xStart, int yStart, int xTarget, int yTarget)
        {
            List<Vertex> resultList = new List<Vertex>();


            //
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
            //todo: possiblitlity to make the step increment be math.sqrt(2) for diagonal edges...
            int StepIncrement = 1;
            
            List<Vertex> visitedVertices = new List<Vertex>();
            PriorityQueue_Vertex queue = new PriorityQueue_Vertex();
            
            start.Seen = true;
            start.StepCount = 0;
            start.Target = target;
            queue.Add(start);

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



        //todo: Remove this unnecesary function. I'll let it stay, because it might be used later on.
        public void RemoveVertex(string vertexName)
        {
            //Get the vertex and check if it is present in the list.
            Vertex target = GetVertexByName(vertexName);
            if (target == null)
                return;

            //Go through each vertex that the target is connected to and remove all edges that connect to the target-vertex.
            //(basically removing all multiedges.)
            foreach (Edge edge in target.Adjacent)
            {
                //Gets the destination vertex from the target-vertex.
                Vertex destination = edge.Destination;
                destination.Adjacent =
                    (from backEgde in destination.Adjacent where backEgde.Destination.Name != vertexName select backEgde)
                        .ToList();
            }

            //Remove the target vertex from the list of vertexes. If this fails, throw an exception, because this isn't ment to happen.
            if (!graphMap.Remove(target.Name))
                throw new NullReferenceException(
                    "There was an error removing a Node from the Graph.\r\nThe Node that the Graph is trying to remove is not present in the Vertex List.");
        }


    }
}
