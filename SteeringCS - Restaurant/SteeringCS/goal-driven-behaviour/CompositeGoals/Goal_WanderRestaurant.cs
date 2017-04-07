using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteeringCS.goal_driven_behaviour.CompositeGoals
{
    class Goal_WanderRestaurant : CompositeGoal
    {

        //todo
        private Stopwatch sw;
        private int minTimeToWander = 20;
        private int maxTimeToWander = 40;
        private int timeToWander;


        public Goal_WanderRestaurant(World theWorld) : base(theWorld)
        {
            sw = new Stopwatch();
        }

        public override void Activate()
        {
            state = Enums.State.Active;
            Random rng =  new Random();
            timeToWander = rng.Next(minTimeToWander, maxTimeToWander+1);


            //Find a random spot
            Vector2D randomSpot = world.restaurandFloorGraph.GetRandomVertex().Pos;

            //Add new goal: GoToLocation
            AddSubgoal(new Goal_GoToLocation(world, randomSpot));
            
            //Start the stopwatch after all the 'heavy' cost functions
            sw.Start();
        }

        public override int Process()
        {
            ActivateIfIdle();
            
            Console.WriteLine("WANDERING RESTAURANT!! at " + sw.ElapsedMilliseconds/1000.0 + " of " + timeToWander + " seconds");

            
            //state = (Enums.State)
                ProcessSubgoals();

            //If the wandering takes to long to get to the goal, it will complete automatically
            if(sw.ElapsedMilliseconds > timeToWander*1000)
                state = Enums.State.Complete;


            return (int)state;
        }

        public override void Terminate()
        {
            Console.WriteLine("TERMINATE WANDERING");
            sw.Stop();
        }

        public override string GetName()
        {
            return "WanderRestaurant";
        }
    }
}
