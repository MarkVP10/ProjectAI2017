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


        public void Add(Vertex newItem)
        {
            foreach (Vertex vertex in queue)
            {
                if (newItem.GetScore() < vertex.GetScore())
                    queue.AddBefore(queue.Find(vertex), newItem);
            }
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
