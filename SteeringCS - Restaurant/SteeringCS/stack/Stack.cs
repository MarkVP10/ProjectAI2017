using SteeringCS.stack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.stack.LinkedList;

namespace SteeringCS.stack
{
    class MyStack<T> : IStack<T>
    {
        private MyLinkedList<T> List;

        public MyStack()
        {
            List = new MyLinkedList<T>();
        }

        //Add new item to stack
        public void Push(T data)
        {
            List.AddLast(data);
        }

        //Returns the Top of the stack and removes it || Returns the last added item in the stack and removes it.
        public T Pop()
        {
            var curr = List.Current.Data;
            List.RemoveLast();
            return curr;
        }

        //Return the Top of the stack || Return the last added item in the stack
        public T Top()
        {
            return List.Current.Data;
        }

        public int Size()
        {
            return List.Size();
        }

    }
}
