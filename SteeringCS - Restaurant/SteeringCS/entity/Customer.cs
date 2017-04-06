using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity
{
    class Customer : MovingEntity
    {
        public Customer(Vector2D pos, World w) : base(pos, w)
        {
            Velocity = new Vector2D(0, 0);
            Scale = 15;

            VColor = Color.Cyan;
            OutLineColor = Color.Black;
        }

        
        public override void Render(Graphics g)
        {
            Vector2D v1 = Pos + (SideVector * Scale);//Left Circle
            Vector2D v2 = Pos - (SideVector * Scale);//Right Circle



            //
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;

            double v1_xCorner = v1.X - (Scale/2);
            double v1_yCorner = v1.Y - (Scale/2);
            double v2_xCorner = v2.X - (Scale/2);
            double v2_yCorner = v2.Y - (Scale/2);

            //Colors
            SolidBrush innerBrush = new SolidBrush(VColor);
            Pen outlinePen = new Pen(OutLineColor, 2);


            //Outer Circles
            g.FillEllipse(innerBrush, new Rectangle((int)v1_xCorner, (int)v1_yCorner, (int)Scale, (int)Scale));
            g.DrawEllipse(outlinePen, new Rectangle((int)v1_xCorner, (int)v1_yCorner, (int)Scale, (int)Scale));
            g.FillEllipse(innerBrush, new Rectangle((int)v2_xCorner, (int)v2_yCorner, (int)Scale, (int)Scale));
            g.DrawEllipse(outlinePen, new Rectangle((int)v2_xCorner, (int)v2_yCorner, (int)Scale, (int)Scale));

            //Inner Circle
            g.FillEllipse(innerBrush, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
            g.DrawEllipse(outlinePen, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));


            //Draw the velocity
            Pen VelocityPen = new Pen(Color.Blue, 2);
            g.DrawLine(VelocityPen, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));

            //Draws the steering vector
            Pen steeringPen = new Pen(Color.Green, 2);
            g.DrawLine(steeringPen, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(SteeringV.X * 2),
                (int)Pos.Y + (int)(SteeringV.Y * 2));
        }
    }
}
