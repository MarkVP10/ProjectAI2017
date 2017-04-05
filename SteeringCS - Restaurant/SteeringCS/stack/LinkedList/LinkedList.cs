using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.stack.LinkedList
{
    class MyLinkedList<T>
    {
        public ListNode<T> Head { get; set; }
        public ListNode<T> Current { get; set; }

        public MyLinkedList()
        {
            Head = new ListNode<T>();
            Current = Head;
        }

        public void AddFirst(T data)
        {
            //Create new node, Add the Heads next to the Next of this Object.
            ListNode<T> newNode = new ListNode<T>
            {
                Data = data,
                Next = Head.Next //new node will have reference of head's next reference
            };

            //If Addfirst is used to add an item to an empty linked list, we need to set current node, otherwise 
            //This node will dissapear.
            if (Current.Data == null)
            {
                Current = newNode;
            }

            //and now head will refer to new node
            Head.Next = newNode;
        }

        public void AddLast(T data)
        {
            //Create a new Node and assign the Data. Next is not needed as this is the last node.
            ListNode<T> newNode = new ListNode<T>
            {
                Data = data,
            };
            //Current.Next will now have reference to the new Node.
            Current.Next = newNode;
            Current = newNode;
        }

        private void Clear(ListNode<T> curr)
        {
            //When we reach the last node in the linked list we can start removing references to the object,
            //so the GBC will clean it. After that, we will go one step backwards and remove all the other objects using recursion.
            if (curr.Next == null)
            {
                Console.WriteLine("Deleted: {0}", curr.Data);
                curr = null;
            }
            else
            {
                Clear(curr.Next);
                Console.WriteLine("Deleted: {0}", curr.Data);
                curr = null;
            }
        }

        public int Size()
        {
            int counter = 0;
            var curr = Current;
            while(curr != null)
            {
                counter++;
                curr = curr.Next;
            }

            return counter;
        }

        public void Clear()
        {
            Clear(Head.Next);
        }

        public void Print()
        {
            //Using Head.Next because the Head is empty so we don't want to print that.
            ListNode<T> curr = Head.Next;
            //Print all nodes, we stop doing it when we hit an empty node.
            while (curr != null)
            {
                Console.WriteLine(curr.Data);
                curr = curr.Next;
            }
        }

        public void Insert(int index, T data)
        {
            ListNode<T> curr = Head.Next;
            //Looping through the LinkedList untill we find the node that belongs to the given index.
            for (int i = 0; i < index; i++)
            {
                curr = curr.Next;
            }
            //Create a new Node, Assign the Next reference of the new node, we grab the next reference of the current node.
            ListNode<T> newNode = new ListNode<T>()
            {
                Data = data,
                Next = curr.Next
            };
            //Assign the Next reference of the Current Node to the new Node.
            curr.Next = newNode;
        }

        public void RemoveFirst()
        {
            //Just assign the Head.Next to the Head.Next.Next.
            Head.Next = Head.Next.Next;
        }

        public void RemoveLast()
        {
            ListNode<T> curr = Head.Next;
            ListNode<T> last = new ListNode<T>();
            while (curr.Next != null)
            {
                last = curr;
                curr = curr.Next;
            }
            last.Next = null;
            Current = last;
        }

        public object GetFirst()
        {
            return Head.Next.Data;
        }
    }
}
