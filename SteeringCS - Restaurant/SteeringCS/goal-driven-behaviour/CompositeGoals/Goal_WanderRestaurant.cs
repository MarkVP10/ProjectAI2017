using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteeringCS.goal_driven_behaviour.AtomicSubgoals;
using SteeringCS.util;

namespace SteeringCS.goal_driven_behaviour.CompositeGoals
{
    class Goal_WanderRestaurant : CompositeGoal
    {
        private Stopwatch sw;
        private readonly int minTimeToWander;
        private readonly int maxTimeToWander;
        private int timeToWander;
        private int consoleUpdateTime;

        public Goal_WanderRestaurant(World theWorld, int minimumWanderTime, int maximumWanderTime) : base(theWorld)
        {
            sw = new Stopwatch();
            minTimeToWander = minimumWanderTime;
            maxTimeToWander = maximumWanderTime;
        }

        public override void Activate()
        {
            state = Enums.State.Active;

            //Get a random amount of time to wander
            Random rng =  new Random();
            timeToWander = rng.Next(minTimeToWander, maxTimeToWander+1);


            //Find a random spot
            Vector2D randomSpot = world.restaurandFloorGraph.GetRandomVertex().Pos;


            //Print to console
            Utility.WriteToConsoleUsingColor("Start wandering the restaurant.", ConsoleColor.White, ConsoleColor.Blue);


            //Add last goal: Idle
            AddSubgoal(new Goal_IdleAtCurrentLocation(world, 3));
            //Add first goal: GoToLocation
            AddSubgoal(new Goal_GoToLocation(world, randomSpot));
            

            //Start the stopwatch after all the 'heavy' cost functions
            sw.Start();
        }

        public override int Process()
        {
            ActivateIfIdle();


            if (sw.ElapsedMilliseconds > consoleUpdateTime)
            {
                consoleUpdateTime += 1000;
                string text = "Wandering the restaurant! At " + Math.Round(sw.ElapsedMilliseconds/1000.0) + " of " +
                              timeToWander + " max seconds of wandering";
                Utility.WriteToConsoleUsingColor(text, ConsoleColor.White, ConsoleColor.DarkYellow);
            }
            

            //Process the walking and idling
            state = (Enums.State)ProcessSubgoals();

            //If the wandering takes to long to get to the goal, it will complete automatically
            if (sw.ElapsedMilliseconds > timeToWander*1000)
                state = Enums.State.Complete;


            return (int)state;
        }

        public override void Terminate()
        {
            sw.Stop();

            Utility.WriteToConsoleUsingColor("Stop Wandering.\r\n\r\n", ConsoleColor.White, ConsoleColor.Red);
        }

        public override string GetName()
        {
            return "WanderRestaurant";
        }
    }
}
