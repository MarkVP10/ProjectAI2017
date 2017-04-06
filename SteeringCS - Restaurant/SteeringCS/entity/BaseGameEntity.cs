using System.Drawing;

namespace SteeringCS
{
    abstract class BaseGameEntity
    {
        public Vector2D Pos { get; set; }
        public float Scale { get; set; }
        public World MyWorld { get; set; }

        
        protected Color OutLineColor;
        public Color VColor { get; set; }

        public BaseGameEntity(Vector2D pos, World w)
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
        
    }
    


    

    
}
