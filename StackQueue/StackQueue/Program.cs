using System;
using System.Collections;
using System.Collections.Generic;


namespace StackQueue
{
    class Stack<T>
    {
        private int Index;

        private int Length;

        private T[] StackArr;

        public Stack(int Length)
        {
            this.Length = Length;
            this.StackArr = new T[this.Length];
            this.Index = 0;
        }

        public void Push(T Item)
        {
            if (this.Index == this.Length)
            {
                throw new StackOverflowException("Stack is full");
            }
            this.StackArr[this.Index++] = Item;
        }

        public T Pop()
        {
            if (this.Index == 0)
            {
                throw new StackOverflowException("Stack is empty");
            }
            T item = this.StackArr[--this.Index];
            return item;
        }

        public int GetLength()
        {
            return this.Length;
        }
        public int GetFulled()
        {
            return this.Index;
        }
    }

    class Queue<T>
    {
        private int Index;

        private int Length;

        private T[] StackArr;

        public Queue(int Length)
        {
            this.Length = Length;
            this.StackArr = new T[this.Length];
            this.Index = 0;
        }

        public void Push(T Item)
        {
            if (this.Index == this.Length)
            {
                throw new StackOverflowException("Queue is full");
            }
            this.StackArr[this.Index++] = Item;
        }

        public T Pop()
        {
            if (this.Index == 0)
            {
                throw new Exception("Queue is empty");
            }
            T item = this.StackArr[0];
            for (int i = 1; i < this.Index; i++)
            {
                this.StackArr[i - 1] = this.StackArr[i];
            }
            this.Index--;
            return item;
        }

        public int GetLength()
        {
            return this.Length;
        }
        public int GetFulled()
        {
            return this.Index;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Stack<string> stack = new Stack<string>(7);
                stack.Push("qwerty1");
                stack.Push("qwerty2");
                stack.Push("qwerty3");
                stack.Push("qwerty4");
                stack.Push("qwerty5");
                stack.Push("qwerty6");


                Console.WriteLine($": {stack.GetFulled()}");

                Console.WriteLine($"1 pop: {stack.Pop()}");
                Console.WriteLine($"2 pop: {stack.Pop()}");
                Console.WriteLine($"1 pop: {stack.Pop()}");
                Console.WriteLine($"2 pop: {stack.Pop()}");


                Console.WriteLine($": {stack.GetFulled()}");

            }
            catch (StackOverflowException e)
            {
                Console.WriteLine(e.ToString());
            }

            try
            {
                Queue<string> stack = new Queue<string>(7);
                stack.Push("q1");
                stack.Push("q2");
                stack.Push("q3");
                stack.Push("q4");
                stack.Push("q5");
                stack.Push("q6");



                Console.WriteLine($": {stack.GetFulled()}");

                Console.WriteLine($"1 pop: {stack.Pop()}");
                Console.WriteLine($"2 pop: {stack.Pop()}");
                Console.WriteLine($"1 pop: {stack.Pop()}");
                Console.WriteLine($"2 pop: {stack.Pop()}");


                Console.WriteLine($": {stack.GetFulled()}");

            }
            catch (StackOverflowException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadKey();
        }
    }
}
