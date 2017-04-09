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
            OutLineColor = Color.Black;
            Scale = 10;
        }

        public abstract void Update(float delta);

        public virtual void Render(Graphics g)
        {
            g.FillEllipse(Brushes.Blue, new Rectangle((int) Pos.X,(int) Pos.Y, 10, 10));
        }


        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetPos">The location you want to be at</param>
        /// <param name="diffFactor">Optional param: Give a factor to multiply the differnce with. Numbers lower than 1 will increase accuracy. Numbers heigher than 1 will decrease accuracy</param>
        /// <returns></returns>
        public bool IsAtPosition(Vector2D targetPos, double diffFactor = 1)
        {
            double deltaX = (int)Math.Abs(Pos.X - targetPos.X);
            double deltaY = (int)Math.Abs(Pos.Y - targetPos.Y);
            double diff = 10 * diffFactor; //Max amount of pixels that the entity's and target's position can be differ.

            return deltaX < diff && deltaY < diff;
        }
    }
    


    

    
}
