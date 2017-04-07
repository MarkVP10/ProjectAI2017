﻿using System;
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

            VColor = Color.Black;
        }
        
        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;

            Pen p = new Pen(VColor, 2);
            g.DrawEllipse(p, new Rectangle((int) leftCorner, (int) rightCorner, (int) size, (int) size));
            g.DrawLine(p, (int) Pos.X, (int) Pos.Y, (int) Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));

            //Draws the steering vector
            Pen steeringPen = new Pen(Color.Green, 2);
            g.DrawLine(steeringPen, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(SteeringV.X * 2), (int)Pos.Y + (int)(SteeringV.Y * 2));
        }
    }
}
