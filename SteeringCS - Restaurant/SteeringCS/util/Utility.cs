using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteeringCS.graph;

namespace SteeringCS.util
{
    class Utility
    {
        /// <summary>
        /// Converts an integer between the value of 0 and 99 to a string with leading zeros.
        /// </summary>
        /// <param name="i">Must be a value between 0 and 99</param>
        /// <returns></returns>
        public static string LeadZero(int i)
        {
            if (i < 0 || i > 99)
            {
                Console.WriteLine("WARNING: Utility.LeadZero was called using invalid input. The requested input was: " + i);
                return i < 0 ? "00" : "99";
                //throw new ArgumentOutOfRangeException("The value of the integer you are trying to make a string with leading zeros is not between 0 and 99.");
            }
                

            return i < 10 ? "0" + i : i.ToString(); // 1 -> "01"  --  24 -> "24"  --  4 -> "04"  --  etc.|
        }



        public static AStarRemnant ListOfVertexToRemnants(List<Vertex> vList, Vertex targetVertex, Vertex startVertex)
        {
            Dictionary<string, AStarRemnant> remnants = new Dictionary<string, AStarRemnant>();
            List<Vertex> vertices = new List<Vertex>(vList); //duplicate the list, so you don't override changes
            

            //First create a Remnant for every unique vertex.
            foreach (Vertex vertex in vertices)
            {
                if(!remnants.ContainsKey(vertex.Name))
                    remnants.Add(vertex.Name, new AStarRemnant(vertex.Pos));
            }
                
            //Edit the target vertex's value to make it the last one.
            remnants[targetVertex.Name] = new AStarRemnant(targetVertex.Pos, true);


            //Then get target and work your way down to the startvertex
            Vertex intermediateVertex = targetVertex;
            while (intermediateVertex.Previous != null)
            {
                //add the intermediateVertex to interVer's previous's remnant's NextInRoute
                remnants[intermediateVertex.Previous.Name].SetNext(remnants[intermediateVertex.Name]);

                //Remove the intermediate from the vertices list
                vertices.Remove(intermediateVertex);

                //Set intermediate to intermediate.prev;
                intermediateVertex = intermediateVertex.Previous;
            }

            //Removes the starter vertex.
            vertices.Remove(intermediateVertex);


            //Go through the rest of the vertices to add to remnant's Considered-List
            foreach (Vertex vertex in vertices)
            {
                //Just in case the starter vertex was not removed.
                if (vertex.Previous == null)
                    continue;

                remnants[vertex.Previous.Name].AddConsidered(remnants[vertex.Name]);
            }


            //Returns the starter remnant, for it connects to all other remnants like a tree structure.
            return remnants[startVertex.Name];
        }
    }
}
