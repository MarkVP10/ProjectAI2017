using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.stack
{
    interface IStack<T>
    {
        void Push(T data);
        T Pop();
        T Top();
    }
}
