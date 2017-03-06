using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.graph
{
    class Graph
    {
        //private List<Vertex> Map = new List<Vertex>();

        private Dictionary<string, Vertex> graphMap = new Dictionary<string, Vertex>();


        public void AddVertex(string name, double x, double y)
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

        public void DrawGraph()
        {

        }




    }
}
