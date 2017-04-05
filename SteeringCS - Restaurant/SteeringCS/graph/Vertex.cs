using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.util;

namespace SteeringCS.graph
{
    class Vertex
    {
        public string Name;         //Name of this vertex.
        public List<Edge> Adjacent; //List of adjacent edges.
        public int X;            //n-th node from the left (tilebased location)
        public int Y;            //n-th node from the top (tilebased location)
        public Vector2D Pos;    //Coord on the map

        //Used in A*
        public Vertex Previous; //Stores the vertex
        public int StepCount;   //How many steps away from the start this vertex is.
        public bool Seen;       //Set to true when searched.
        public Vertex Target;   //Target vertex

        public Vertex(int x, int y, double graphNodeSeperationFactor)
        {
            Name = Utility.LeadZero(x) + Utility.LeadZero(y);
            Adjacent = new List<Edge>();
            X = x;
            Y = y;
            Pos = new Vector2D(x * graphNodeSeperationFactor, y * graphNodeSeperationFactor);

            ResetPath();
        }


        //todo: remove debug function
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
            Target = null;
        }


        public void SetTarget(Vertex t)
        {
            Target = t;
        }
        public int GetScore()
        {
            return StepCount + CalculateManhattanHeuristic(Target);
        }
        public int GetScore(Vertex target)
        {
            return StepCount + CalculateManhattanHeuristic(target);
        }
        public int CalculateManhattanHeuristic(Vertex targetVertex)
        {
            if (targetVertex == null)
                return 0;
            return (Math.Abs(X - targetVertex.X) + Math.Abs(Y - targetVertex.Y));
        }

        
    }
}
