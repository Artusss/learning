using System;

namespace StackQueue
{
    class Stack
    {
        public int Index;

        public string[] StackArr;

        public Stack(int length)
        {
            this.StackArr = new string[length];
            this.Index = 0;
        }

        public void Push(string item)
        {
            this.StackArr[this.Index++] = item;
        }

        public string Pop()
        {
            string item = this.StackArr[--this.Index];
            return item;
        }
    }

    class Queue
    {

    }
    class Program
    {
        static void Main(string[] args)
        {
            Stack stack = new Stack(7);
            stack.Push("qwerty1");
            stack.Push("qwerty2");
            stack.Push("qwerty3");

            Console.WriteLine($"1 pop: {stack.Pop()}");
            Console.WriteLine($"2 pop: {stack.Pop()}");
            Console.ReadKey();
        }
    }
}
