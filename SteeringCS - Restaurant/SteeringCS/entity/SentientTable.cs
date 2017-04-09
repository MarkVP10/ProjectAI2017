using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.world;

namespace SteeringCS.entity
{
    class SentientTable : Obstacle
    {
        public Table table;
        private readonly int nodeWidth;

        public SentientTable(Vector2D pos, World w, Table t) : base(pos, w)
        {
            table = t;
            nodeWidth = World.graphNodeSeperationFactor;
        }


        public override void Render(Graphics g)
        {
            VColor = table.HasCustomers ? Color.Yellow : Color.SaddleBrown;
            SolidBrush sb = new SolidBrush(VColor);
            Pen outlinePen = new Pen(OutLineColor, 2);

            int rectX = (int)Pos.X - nodeWidth*(table.IsFourPerson ? 1 : 2);
            int rectY = (int)Pos.Y - nodeWidth*1;
            int rectWidth = nodeWidth*(table.IsFourPerson ? 2 : 4);
            int rectHeight = nodeWidth*2;

            g.FillRectangle(sb, rectX, rectY, rectWidth, rectHeight);
            g.DrawRectangle(outlinePen, rectX, rectY, rectWidth, rectHeight);

        }
    }
}
