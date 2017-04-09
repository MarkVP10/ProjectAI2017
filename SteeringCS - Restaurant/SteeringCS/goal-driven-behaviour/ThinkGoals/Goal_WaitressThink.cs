using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.goal_driven_behaviour.ThinkGoals
{
    class Goal_WaitressThink : Goal_Think
    {
        public Goal_WaitressThink(World theWorld) : base(theWorld)
        {
        }

        public override void HandleMessageToBrain(string simpleMessage, object data = null)
        {
            //doesn't handle messages. (yet)
        }

        public override string GetName()
        {
            return "BrainlessWaitress";
        }
    }
}
