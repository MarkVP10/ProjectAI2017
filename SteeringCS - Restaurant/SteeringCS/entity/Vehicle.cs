using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.goal_driven_behaviour.ThinkGoals;

namespace SteeringCS.entity
{
    class Vehicle : MovingEntity
    {
        public Vehicle(Vector2D pos, World w) : base(pos, w, new Goal_NullThink(w))
        {
            Velocity = new Vector2D(0, 0);
            Scale = 5;

            VColor = Color.DarkRed;
        }
        
        public override void Render(Graphics g)
        {
            //Determine position & size
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;

            //Draw circle at last player clicked location
            Pen p = new Pen(VColor, 2);
            g.DrawEllipse(p, new Rectangle((int) leftCorner, (int) rightCorner, (int) size, (int) size));
        }
    }
}
