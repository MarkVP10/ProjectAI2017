using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;
using SteeringCS.util;

namespace SteeringCS.goal_driven_behaviour.AtomicSubgoals
{
    class Goal_IdleAtCurrentLocation : Goal
    {
        private Stopwatch sw;
        private readonly int amountOfSecondsToIdle;

        public Goal_IdleAtCurrentLocation(World theWorld, int idleTime) : base(theWorld)
        {
            sw = new Stopwatch();
            amountOfSecondsToIdle = idleTime;
        }

        public override void Activate()
        {
            state = Enums.State.Active;

            world.TheBoss.Velocity = new Vector2D();
            world.TheBoss.combineStratagy.SetTarget(null);
            world.TheBoss.combineStratagy.SwitchBehaviour(CombineForces.Behaviours.None);

            string text = "Start idling for " + amountOfSecondsToIdle + " seconds. ";
            Utility.WriteToConsoleUsingColor(text, ConsoleColor.White, ConsoleColor.Blue);

            sw.Start();
        }

        public override int Process()
        {
            ActivateIfIdle();
            
            //Wait untill done idling
            if (sw.ElapsedMilliseconds > amountOfSecondsToIdle * 1000)
                state = Enums.State.Complete;


            return (int)state;
        }

        public override void Terminate()
        {
            sw.Stop();

            Utility.WriteToConsoleUsingColor("Stop idling.", ConsoleColor.White, ConsoleColor.Red);
        }

        public override string GetName()
        {
            return "IdleAtCurrentLocation";
        }
    }
}
