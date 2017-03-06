using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.graph
{
    class Vertex
    {
        public string Name;         //Name of this vertex.
        public List<Edge> Adjacent; //List of adjacent edges.
        public float Y;            //Y-position on map
        public float X;            //X-position on map

        public Vertex(string name, float x, float y)
        {
            Name = name;
            X = x;
            Y = y;
            Adjacent = new List<Edge>();
        }

        public void printAdjacent()
        {
            foreach (Edge edge in Adjacent)
            {
                Console.WriteLine("{0}\t-->\t{1}\t|\t{2}", Name, edge.Destination.Name);
            }
        }
    }
}
