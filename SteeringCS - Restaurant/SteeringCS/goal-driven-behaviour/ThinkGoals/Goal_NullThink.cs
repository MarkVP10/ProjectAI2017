using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.goal_driven_behaviour.ThinkGoals
{
    class Goal_NullThink : Goal_Think
    {
        public Goal_NullThink(World theWorld) : base(theWorld)
        {
            //throw new NotImplementedException();
        }



        public override string GetName()
        {
            //It doesn't do anything, so it will return an empty string
            return "";
        }
    }
}
