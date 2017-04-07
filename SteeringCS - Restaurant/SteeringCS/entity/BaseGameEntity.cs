using System;
using System.Drawing;
using SteeringCS.goal_driven_behaviour;

namespace SteeringCS
{
    abstract class BaseGameEntity
    {
        public Vector2D Pos { get; set; }
        public float Scale { get; set; }
        public World MyWorld { get; set; }
        
        protected Color OutLineColor;
        public Color VColor { get; set; }

        protected BaseGameEntity(Vector2D pos, World w)
        {
            Pos = pos;
            MyWorld = w;

            VColor = Color.Blue;
            OutLineColor = Color.Blue;
            Scale = 10;
        }

        public abstract void Update(float delta);

        public virtual void Render(Graphics g)
        {
            g.FillEllipse(Brushes.Blue, new Rectangle((int) Pos.X,(int) Pos.Y, 10, 10));
        }


        
        public bool IsAtPosition(Vector2D targetPos)
        {
            int deltaX = (int)Math.Abs(Pos.X - targetPos.X);
            int deltaY = (int)Math.Abs(Pos.Y - targetPos.Y);
            int difference = 10; //Max amount of pixels that the entity's and target's position can be differ.

            return deltaX < difference && deltaY < difference;
        }
    }
    


    

    
}
