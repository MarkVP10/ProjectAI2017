using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.goal_driven_behaviour.ThinkGoals;
using SteeringCS.util;

namespace SteeringCS.entity
{
    class Waitress : MovingEntity
    {
        public Waitress(Vector2D pos, World w) : base(pos, w, new Goal_WaitressThink(w))
        {
            Scale = 10;

            VColor = Color.CadetBlue;
            OutLineColor = Color.Black;
        }

        
        public override void Render(Graphics g)
        {
            //Get vectors, depending on the HeadingVector. (where it is moving to/looking at.)
            Vector2D v1 = Pos + HeadingVector * 2 * Scale;//Top
            Vector2D v2 = Pos + ((HeadingVector * -1 + SideVector) * Scale);//Left bottom
            Vector2D v3 = Pos + ((HeadingVector * -1 - SideVector) * Scale);//Right bottom

            

            //Inner
            SolidBrush innerBrush = new SolidBrush(VColor);
            g.FillPolygon(innerBrush, new[] { v1.ToPointF(), v2.ToPointF(), v3.ToPointF() });
            
            //Outline
            Pen outlinePen = new Pen(OutLineColor, 2);
            g.DrawLine(outlinePen, v1.ToPointF(), v2.ToPointF());
            g.DrawLine(outlinePen, v2.ToPointF(), v3.ToPointF());
            g.DrawLine(outlinePen, v3.ToPointF(), v1.ToPointF());
            
            

            //-------------------------------------------------------------
            //-----Detectionbox stuff for Obstacle Avoidance Debugging-----
            //-------------------------------------------------------------
            /*
            //Detectionbox variables
            double MinDetectionBoxLength = 40;
            double DetectionBoxLength = MinDetectionBoxLength + (Velocity.Length() / MaxSpeed) * MinDetectionBoxLength;
            Vector2D v5 = v2 + (HeadingVector * (DetectionBoxLength + Scale)); //Left detection box
            Vector2D v6 = v3 + (HeadingVector * (DetectionBoxLength + Scale)); //Right detection box

            //Draw the detection box
            g.DrawLine(outlinePen, v2.ToPointF(), v5.ToPointF()); //left side of detection box
            g.DrawLine(outlinePen, v3.ToPointF(), v6.ToPointF()); //top side of detection box
            g.DrawLine(outlinePen, v5.ToPointF(), v6.ToPointF()); //right side of detection box
            */

            if (MyWorld.steeringVisible)
                RenderHeadingAndVelocity(g);
        }
    }
}
