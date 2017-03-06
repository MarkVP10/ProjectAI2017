using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.graph
{
    class Edge
    {
        public Vertex Destination; //Contains the destination vertex. There is no 'second' vertex variable, because the Vertex is responsible for knowing all the Edges.
        

        public Edge(Vertex dest)
        {
            Destination = dest;
        }
    }
}
