using SteeringCS.graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SteeringCS.goal_driven_behaviour.Enums;

namespace SteeringCS.goal_driven_behaviour.CompositeGoals.AtomicSubgoals
{
    class FindPathToCustomer : CompositeGoal
    {
        public Vertex vertexFrom;
        public Vertex vertexTo;


        public override void Activate()
        {
            System.Windows.Forms.MessageBox.Show("Activating FindPath");

            /* Customer has been chosen
             * 
             * find closest nodes to positions(); (?)
             * vertexFrom = closest Node(current x and y)
             * vertexTo = closestNode(target x and y)
             */
            state = State.Active;
        }

        public override int Process()
        {
            System.Windows.Forms.MessageBox.Show("Processing FindPath");

            /* 
             * Call A* method to seek best path
             * bestPath(vertexFrom, vertexTo();
             */
            Terminate();
            return (int)state;
        }

        public override void Terminate()
        {
            System.Windows.Forms.MessageBox.Show("Terminating FindPath");

            state = State.Complete;
            /*
             * Goal met, next task.
             */
        }
    }
}
