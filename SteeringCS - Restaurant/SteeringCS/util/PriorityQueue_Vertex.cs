using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.graph;

namespace SteeringCS.util
{
    class PriorityQueue_Vertex
    {
        private LinkedList<Vertex> queue;


        public PriorityQueue_Vertex()
        {
            queue = new LinkedList<Vertex>();
        }



        //todo Very inefficient!!
        //todo Improve/Optimize later
        public void Add(Vertex newItem)
        {
            //Adds the item to the first if the list is empty
            if (IsEmpty())
            {
                queue.AddFirst(newItem);
                return;
            }

            //Go through each vertex, untill you find one that has a lower 'score' (Score = Stepcount + (manhatten)Heuristic)
            foreach (Vertex vertex in queue)
            {
                if (newItem.GetScore() < vertex.GetScore())
                {
                    queue.AddBefore(queue.Find(vertex), newItem);
                    return;
                }
            }

            //Adds the item to the end when not found
            queue.AddLast(newItem);
        }

        public void UpdateQueue(Vertex relocateVertex)
        {
            queue.Remove(relocateVertex);
            Add(relocateVertex);
        }

        public Vertex Pop()
        {
            Vertex returnItem = queue.First.Value;
            queue.RemoveFirst();
            return returnItem;
        }

        public bool IsEmpty()
        {
            return queue.Count == 0;
        }

    }
}
