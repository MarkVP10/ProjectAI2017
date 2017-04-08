using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteeringCS.AtomicSubgoals;
using SteeringCS.goal_driven_behaviour.AtomicSubgoals;
using SteeringCS.world;

namespace SteeringCS.goal_driven_behaviour.CompositeGoals
{
    class Goal_TalkWithCustomer : CompositeGoal
    {

        //todo
        private Table table;


        public Goal_TalkWithCustomer(World theWorld) : base(theWorld)
        {
        }


        public override void Activate()
        {
            state = Enums.State.Active;
            Console.WriteLine("ACTIVATING TALKING WITH CUSTOMER!");


            //Choose a customer filled table
            table = world.GetRandomFilledTable();
            
            if (table == null)
            {
                //No tables available to talk to.
                state = Enums.State.Failed;
                Console.WriteLine("Wanted to talk to a customer, but there are none. :c");
                return;
            }

            string nameOfRandomTable = table.GetNodeNameForWaiterPosition();
            Vector2D tablePos = world.restaurandFloorGraph.GetVertexByName(nameOfRandomTable).Pos;

            
            //Goto customer (GoToLocation)
            //Idle at table for 2 seconds
            //Get score from the customer
            //Idle at table for 2 seconds
            //todo: Dismiss customers from table?


            AddSubgoal(new Goal_IdleAtCurrentLocation(world, 2));
            AddSubgoal(new Goal_GetScoreFromCustomer(world));
            AddSubgoal(new Goal_IdleAtCurrentLocation(world, 2));
            AddSubgoal(new Goal_GoToLocation(world, tablePos));
        }

        public override int Process()
        {
            ActivateIfIdle();
            
            state = (Enums.State)ProcessSubgoals();
            
            return (int) state;
        }

        public override void Terminate()
        {
            //nothing to clean up
            Console.WriteLine("TERMINATE TALKING WITH CUSTOMER\r\n\r\n");


            //todo remove this!!!
            table.HasCustomers = false;
        }

        public override string GetName()
        {
            return "TalkWithCustomer";
        }

        
    }
}
