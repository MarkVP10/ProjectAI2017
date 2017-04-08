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

        public int X;
        public int Y;
        public bool IsFourPerson;
        public bool HasCustomers; //todo: when customer arrives, set to true. When they leave, set to false

        

        public Table(int xNode, int yNode, bool fourPeople)
        {
            X = xNode;
            Y = yNode;
            IsFourPerson = fourPeople;
        }


        public string GetNodeNameForWaiterPosition()
        {
            string xxyy = "";

            xxyy += Utility.LeadZero(X + (IsFourPerson ? 2 : 3)); //Depends on if the table has room for 4 or 6 people.
            xxyy += Utility.LeadZero(Y - 2);

            return xxyy;
        }




    }
}
