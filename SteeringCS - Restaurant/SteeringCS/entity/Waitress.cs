using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.util;

namespace SteeringCS.entity
{
    class Waitress : MovingEntity
    {
        public Waitress(Vector2D pos, World w) : base(pos, w)
        {
            Scale = 10;

            VColor = Color.CadetBlue;
            OutLineColor = Color.Black;
        }

        
        public override void Render(Graphics g)
        {
            Vector2D v1 = Pos + HeadingVector * 2 * Scale;//Top
            Vector2D v2 = Pos + ((HeadingVector * -1 + SideVector) * Scale);//Left bottom
            Vector2D v3 = Pos + ((HeadingVector * -1 - SideVector) * Scale);//Right bottom


            double MinDetectionBoxLength = 40;
            double DetectionBoxLength = MinDetectionBoxLength + (Velocity.Length() / MaxSpeed) * MinDetectionBoxLength;
            Vector2D v5 = v2 + (HeadingVector*(DetectionBoxLength + Scale)); //Left detection box
            Vector2D v6 = v3 + (HeadingVector*(DetectionBoxLength + Scale)); //Right detection box

            //
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;

            //Inner
            SolidBrush innerBrush = new SolidBrush(VColor);
            g.FillPolygon(innerBrush, new[] { v1.ToPointF(), v2.ToPointF(), v3.ToPointF() });
            


            //Outline
            Pen outlinePen = new Pen(OutLineColor, 2);
            g.DrawLine(outlinePen, v1.ToPointF(), v2.ToPointF());
            g.DrawLine(outlinePen, v2.ToPointF(), v3.ToPointF());
            g.DrawLine(outlinePen, v3.ToPointF(), v1.ToPointF());


            //Draw the detection box
            g.DrawLine(outlinePen, v2.ToPointF(), v5.ToPointF()); //left side of detection box
            g.DrawLine(outlinePen, v3.ToPointF(), v6.ToPointF()); //top side of detection box
            g.DrawLine(outlinePen, v5.ToPointF(), v6.ToPointF()); //right side of detection box


            //Draw the velocity
            Pen VelocityPen = new Pen(Color.Blue, 2);
            g.DrawLine(VelocityPen, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));

            //Draws the steering vector
            Pen steeringPen = new Pen(Color.Green, 2);
            g.DrawLine(steeringPen, (int) Pos.X, (int) Pos.Y, (int) Pos.X + (int) (SteeringV.X*2),
                (int) Pos.Y + (int) (SteeringV.Y*2));
        }
    }
}
