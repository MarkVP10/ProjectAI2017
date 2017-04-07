using System;
using SteeringCS.goal_driven_behaviour;
using SteeringCS.graph;

namespace SteeringCS.AtomicSubgoals
{
    class FindPathToCustomer : Goal
    {
        public FindPathToCustomer(World w) : base(w)
        { }

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
            state = Enums.State.Active;
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

            state = Enums.State.Complete;
            /*
             * Goal met, next task.
             */
        }

        public override string GetName()
        {
            throw new NotImplementedException();
        }
    }
}
