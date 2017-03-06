using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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




    }
}
