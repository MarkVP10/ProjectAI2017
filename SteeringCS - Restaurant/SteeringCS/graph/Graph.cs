using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.util;

namespace SteeringCS.graph
{
    class Graph
    {
        //private List<Vertex> Map = new List<Vertex>();

        private Dictionary<string, Vertex> graphMap = new Dictionary<string, Vertex>();


        public void AddVertex(string name, float x, float y)
        {
            graphMap.Add(name, new Vertex(name, x, y));
        }

        public void AddEdge(string sourceName, string destinationName)
        {
            Vertex source = GetVertex(sourceName);
            Vertex destination = GetVertex(destinationName);


            source.Adjacent.Add(new Edge(destination));
        }
        public void AddMultiEdge(string sourceName, string destName)
        {
            AddEdge(sourceName, destName);
            AddEdge(destName, sourceName);
        }

        private Vertex GetVertex(string targetName)
        {
            //Find the vertex in the dictionary
            Vertex v;
            try
            {
                v = graphMap[targetName];
            }
            catch (Exception)
            {
                v = null;
            }

            return v;
        }

        public void DrawGraph(Graphics g)
        {
            var penBox = new Pen(Color.Gray, 7f);
            var penLine = new Pen(Color.Gray, 2f);
            foreach (Vertex vertex in graphMap.Values)
            {
                g.DrawEllipse(penBox, new RectangleF(vertex.X - 3.5f, vertex.Y - 3.5f, 7, 7));
                foreach (Edge edge in vertex.Adjacent)
                {
                    g.DrawLine(penLine, vertex.X, vertex.Y, edge.Destination.X, edge.Destination.Y);
                }
            }
        }


        //todo test!!!
        public List<Vertex> AStar(Vertex start, Vertex target)
        {
            int StepIncrement = 10;

            List<Vertex> pathToTarget = new List<Vertex>();
            List<Vertex> CleanUpList = new List<Vertex>();
            PriorityQueue_Vertex queue = new PriorityQueue_Vertex();
            Vertex currentVertex = null;
            

            CleanUpList.Add(start);
            start.Seen = true;
            start.StepCount = 0;
            start.Target = target;
            queue.Add(start);

            while (queue.IsEmpty())
            {
                currentVertex = queue.Pop();
                currentVertex.Seen = true;

                if (currentVertex == target)
                    break;

                foreach (Edge edge in currentVertex.Adjacent)
                {
                    //If already seen, no need to do anything.
                    if(edge.Destination.Seen)
                        continue;

                    //Add to cleanup list for resetting values when A* is done.
                    CleanUpList.Add(edge.Destination);

                    //Stepcount update
                    if (edge.Destination.StepCount > currentVertex.StepCount + StepIncrement)
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


            //Go through a loop to get all previous from Target.
            pathToTarget.Add(target);
            while (target.Previous != null)
            {
                pathToTarget.Add(target.Previous);
                target = target.Previous;
            }



            CleanUpList.ForEach(vertex => vertex.ResetPath()); //Reset all vertexes so they can be used again.

            return pathToTarget;
        }


    }
}
