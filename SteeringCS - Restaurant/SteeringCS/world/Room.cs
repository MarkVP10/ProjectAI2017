using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.world
{
    class Room
    {
        //Top Left node of the room.
        public readonly int topLeftNodeX;
        public readonly int topLeftNodeY;

        //Bottom right node of the room
        public readonly int bottomRightNodeX;
        public readonly int bottomRightNodeY;

        public readonly Color color;
        public readonly string name;

        /// <summary>
        /// Creates a room using the Node location from the graph.
        /// </summary>
        /// <param name="tlX">TopLeft Node X</param>
        /// <param name="tlY">TopLeft Node Y</param>
        /// <param name="brX">BottomRight Node X</param>
        /// <param name="brY">BottomRight Node Y</param>
        public Room(int tlX, int tlY, int brX, int brY, Color color, string name)
        {
            topLeftNodeX = tlX;
            topLeftNodeY = tlY;
            bottomRightNodeX = brX;
            bottomRightNodeY = brY;
            
            this.color = color;
            this.name = name;
        }


        public Rectangle GetRectangle()
        {
            int xCoord = topLeftNodeX * World.graphNodeSeperationFactor;
            int yCoord = topLeftNodeY * World.graphNodeSeperationFactor;

            int width = (bottomRightNodeX - topLeftNodeX)*World.graphNodeSeperationFactor;
            int height = (bottomRightNodeY - topLeftNodeY)*World.graphNodeSeperationFactor;

            return new Rectangle(xCoord, yCoord, width, height);
        }

        public Vector2D GetCenterPosition()
        {
            int xCoord = (topLeftNodeX + (bottomRightNodeX - topLeftNodeX)/2) * World.graphNodeSeperationFactor;
            int yCoord = (topLeftNodeY + (bottomRightNodeY - topLeftNodeY)/2) * World.graphNodeSeperationFactor;

            return new Vector2D(xCoord, yCoord);
        }
    }
}
