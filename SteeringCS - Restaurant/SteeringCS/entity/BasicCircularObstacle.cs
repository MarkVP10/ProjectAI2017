using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteeringCS.entity
{
    class BasicCircularObstacle : Obstacle
    {
        public BasicCircularObstacle(Vector2D pos, World w) : base(pos, w)
        {
            Scale = 10;
            VColor = Color.Orange;
        }

        public override void Render(Graphics g)
        {
            Pen outlinePen = new Pen(OutLineColor);
            SolidBrush colorBrush = new SolidBrush(VColor);
            
            g.FillEllipse(colorBrush, new RectangleF((float)Pos.X-Scale, (float)Pos.Y-Scale, Scale*2, Scale*2));
            g.DrawEllipse(outlinePen, new RectangleF((float)Pos.X-Scale, (float)Pos.Y-Scale, Scale*2, Scale*2));
            //MessageBox.Show("DRAW with: " + Pos.X + "|" + Pos.Y + "|" + Scale + "|");
        }
    }
}
