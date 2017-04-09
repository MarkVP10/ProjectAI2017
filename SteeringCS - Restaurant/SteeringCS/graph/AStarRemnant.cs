using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.graph
{
    class AStarRemnant
    {
        private AStarRemnant NextInRoute;
        private readonly List<AStarRemnant> Considered;
        private readonly Vector2D Position;
        private readonly bool IsEnd;

        public AStarRemnant(Vector2D pos, bool end = false)
        {
            Position = pos;
            NextInRoute = null;
            Considered = new List<AStarRemnant>();
            IsEnd = end;
        }



        public void SetNext(AStarRemnant next)
        {
            NextInRoute = next;
        }
        public void AddConsidered(AStarRemnant remnant)
        {
            Considered.Add(remnant);
        }
        public void AddConsidered(List<AStarRemnant> remnants)
        {
            Considered.AddRange(remnants);
        }

        public bool isEnd()
        {
            return IsEnd;
        }

        
        public AStarRemnant GetNext()
        {
            return NextInRoute;
        }
        public List<AStarRemnant> GetConsidered()
        {
            return Considered;
        }
        public Vector2D GetPosition()
        {
            return Position;
        }


        public void Draw(Graphics g)
        {
            //Brush selection
            Brush b;
            if (IsEnd) // Is last
                b = Brushes.OrangeRed;
            else if (NextInRoute == null) //Is a considered
                b = Brushes.Purple;
            else //Is on route
                b = Brushes.LimeGreen;


            //Draw all considered
            foreach (AStarRemnant remnant in Considered)
            {
                g.DrawLine(new Pen(Color.Purple, 3), (float)Position.X, (float)Position.Y, (float)remnant.Position.X, (float)remnant.Position.Y);
                remnant.Draw(g);
            }

            //Draw next
            if (NextInRoute != null)
            {
                g.DrawLine(new Pen(Color.LimeGreen, 3), (float)Position.X, (float)Position.Y, (float)NextInRoute.Position.X, (float)NextInRoute.Position.Y);
                NextInRoute.Draw(g);
            }


            //Draw self
            g.FillEllipse(b, (float)Position.X - 5f, (float)Position.Y - 5f, 10, 10);
        }
    }
}
