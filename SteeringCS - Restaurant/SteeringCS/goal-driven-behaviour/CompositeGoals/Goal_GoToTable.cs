using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.goal_driven_behaviour.CompositeGoals
{
    class Goal_GoToTable : CompositeGoal
    {

        //todo

        public Goal_GoToTable(World theWorld) : base(theWorld)
        {
        }

        public override void Activate()
        {
            throw new NotImplementedException();
        }

        public override int Process()
        {
            throw new NotImplementedException();
        }

        public override void Terminate()
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            return "GoToTable";
        }

        
    }
}
