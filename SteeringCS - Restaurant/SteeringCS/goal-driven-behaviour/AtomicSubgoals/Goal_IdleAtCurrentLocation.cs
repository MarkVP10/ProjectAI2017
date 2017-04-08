using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;

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

            Console.WriteLine("Start idling.");
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
            Console.WriteLine("Stop idling.");
        }

        public override string GetName()
        {
            return "IdleAtCurrentLocation";
        }
    }
}
