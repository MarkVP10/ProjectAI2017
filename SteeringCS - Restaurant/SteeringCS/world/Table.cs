using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.util;

namespace SteeringCS.world
{
    class Table
    {
        //Center node of the table
        public int X; // x value of the node
        public int Y; // y value of the node
        
        public bool IsFourPerson;
        public bool HasCustomers;

        
        public Table(int xNode, int yNode, bool fourPeople)
        {
            X = xNode;
            Y = yNode;
            IsFourPerson = fourPeople;
        }
        
        //Returns the name for a node that the waiter (or any other personel) will stand at to talk to the table.
        //This node is to the top right of the table.
        public string GetNodeNameForWaiterPosition()
        {
            string xxyy = "";

            xxyy += Utility.LeadZero(X + (IsFourPerson ? 2 : 3)); //Depends on if the table has room for 4 or 6 people.
            xxyy += Utility.LeadZero(Y - 2);

            return xxyy;
        }
    }
}
