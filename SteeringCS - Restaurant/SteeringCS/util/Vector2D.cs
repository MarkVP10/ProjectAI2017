using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS
{
   
    public class Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double W { get; set; }

        public Vector2D() : this(0,0)
        {
        }

        public Vector2D(double x, double y, double w = 1)
        {
            X = x;
            Y = y;
            W = w;
        }

        public double Length() //Returns C
        {
            //sqrt(A^2 + B^2) = C
            return Math.Sqrt(LengthSquared());
        }

        public double LengthSquared() //Returns C^2
        {
            //A^2 + B^2 = C^2
            return Math.Pow(X, 2) + Math.Pow(Y, 2);
        }

        public Vector2D Add(Vector2D v)
        {
            this.X += v.X;
            this.Y += v.Y;
            return this;
        }

        public Vector2D Sub(Vector2D v)
        {
            this.X -= v.X;
            this.Y -= v.Y;
            return this;
        }

        public Vector2D Multiply(double value)
        {
            this.X *= value;
            this.Y *= value;
            return this;
        }

        public Vector2D Divide(double value)
        {
            if (value != 0)
            {
                this.X /= value;
                this.Y /= value;
            }
            return this;
        }

        public Vector2D Normalize()
        {
            Divide(Length());
            return this;
        }

        public Vector2D truncate(double maX)
        {
            if (Length() > maX)
            {
                Normalize();
                Multiply(maX);
            }
            return this;
        }
        
        public Vector2D Clone()
        {
            return new Vector2D(this.X, this.Y);
        }
        
        public override string ToString()
        {
            return String.Format("({0},{1})", X, Y);
        }

        public Vector2D Perpendicular()
        {
            return new Vector2D(-Y, X); //Turn X and Y around and multiply one with -1
        }


        
        /*     -------EXPLANATION-------
                          

                           negY
                   D     0°=360°     A
                    \   d   |   a   /                                            
                     \      |      /                                             
                      \ 30° | 30° /                                              
                       \    |    /                               
                        \   |   /                                
                         \  |  /                                 
                    60°   \ | /   60°                             
                           \|/                                                   
   negX  270°---------------+--------------- 90°  posX                            
                           /|\                      
                    60°   / | \   60°                             
                         /  |  \                                 
                        /   |   \                                
                       /    |    \                               
                      / 30° | 30° \                                             
                     /      |      \                             
                    /   c   |   b   \                            
                   C       180°      B                       
                           posY 

             a = 30°
             b = 30°
             c = 30°
             d = 30°

            A =   0° + 30° = 30°
            B = 180° - 30° = 150°
            C = 180° + 30° = 210°
            D = 360° - 30° = 330°
        */


        /// <summary>
        /// The one where it returns a degree using the Windows Forms way of graphic.
        /// Where the Zenith is negY.
        /// </summary>
        /// <returns></returns>
        public static float ToDegrees(Vector2D v1)
        {
            Vector2D tempV = v1.Clone();


            
            if (tempV.X == 0)
            {
                if (tempV.Y > 0) //Down(posY)
                    return 180;
                if (tempV.Y < 0) //Up (negY)
                    return 0;
            }
            else if (tempV.Y == 0)
            {
                if (tempV.X > 0) //Right (posX)
                    return 90;
                if (tempV.X < 0) //Left (negX)
                    return 270;
                return 0; //Both X and Y are 0
            }


            //Gets the angle
            double angle = Math.Atan(tempV.X/tempV.Y)/(Math.PI/180);
            float returnAngle = 0;

            
            //Determine what angle to return
            //Also, when there is an uneven amount of Negative coordinates, the angle needs to be multiplied by -1 to get the correct angle.
            if (tempV.X > 0 && tempV.Y < 0) //PosX & NegY -> up, right -> NorthEast -> A (0°-90°)
                returnAngle = (float)angle*-1;
            else if (tempV.X > 0 && tempV.Y > 0) //PosX & PosY -> down, right -> SouthEast -> B (90°-180°)
                returnAngle = (float)(180-angle);
            else if (tempV.X < 0 && tempV.Y > 0) //NegX & PosY -> down, left -> SouthWest -> C (180°-270°)   
                returnAngle = (float)(180+angle*-1);
            else if (tempV.X < 0 && tempV.Y < 0) //NegX & NegY -> up, left -> NorthWest -> D (270°-360°)
                returnAngle = (float)(360-angle);

            //Return the angle
            return returnAngle;
        }

            
        public PointF ToPointF()
        {
            return new PointF((float)this.X, (float)this.Y);
        }
        

        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            Vector2D v = v1.Clone();
            v.Add(v2);
            return v;
        }
        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            Vector2D v = v1.Clone();
            v.Sub(v2);
            return v;
        }

        public static Vector2D operator *(Vector2D v1, double value)
        {
            Vector2D v = v1.Clone();
            v.Multiply(value);
            return v;
        }
        public static Vector2D operator /(Vector2D v1, double value)
        {
            Vector2D v = v1.Clone();
            v.Divide(value);
            return v;
        }
    }


}
