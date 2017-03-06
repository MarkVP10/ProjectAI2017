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
        public double Y;            //Y-position on map
        public double X;            //X-position on map

        public Vertex Previous; //Stores the vertex
        public int StepCount; //How many steps away from the start this vertex is.
        public bool Seen; //Set to true when searched.
        public Vertex Target;

        public Vertex(string name,double x,double y)
        {
            Name = name;
            X = x;
            Y = y;
            Adjacent = new List<Edge>();

            ResetPath();
        }


        public void printAdjacent()
        {
            foreach (Edge edge in Adjacent)
            {
                Console.WriteLine("{0}\t-->\t{1}\t|\t{2}", Name, edge.Destination.Name);
            }
        }




        public void ResetPath()
        {
            Previous = null;
            StepCount = int.MaxValue;
            Seen = false;
        }

        public void SetTarget(Vertex t)
        {
            Target = t;
        }
        public int CalculateManhattanHeuristic()
        {
            if (Target == null)
                return 0;
            return (int)(Math.Abs(X - Target.X) + Math.Abs(Y - Target.Y));
        }

        public int GetScore()
        {
            return StepCount + CalculateManhattanHeuristic();
        }
    }
}
