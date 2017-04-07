using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.goal_driven_behaviour.ThinkGoals;

namespace SteeringCS.entity
{
    class Manager : MovingEntity
    {

        //todo
        public Manager(Vector2D pos, World w) : base(pos, w, new Goal_ManagerThink(w))
        {
            Scale = 15;
            VColor = Color.CadetBlue;
            OutLineColor = Color.Black;
        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;
            
            g.FillRectangle(new SolidBrush(VColor), (int)leftCorner, (int)rightCorner, (int)size, (int)size);
            g.DrawRectangle(new Pen(OutLineColor), (int)leftCorner, (int)rightCorner, (int)size, (int)size);

        }
    }
}
