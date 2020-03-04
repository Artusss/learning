using System;
using System.Collections.Generic;

namespace BinarySearch
{
    class Program
    {
        public static int[] generateData(int length, int maxItem)
        {
            int[] data = new int[length];
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                data[i] = rand.Next(maxItem);
            }
            return data;
        }

        public static void bubbleSort(int[] arr)
        {
            for (int k = 0; k < arr.Length; k++)
            {
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        int t = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = t;
                    }
                }
            }
        }

        public static void printIntArr(int[] arr)
        {
            Console.WriteLine($"Length: {arr.Length}");
            int i = 0;
            foreach (int item in arr)
            {
                Console.WriteLine($"{i}. {item}");
                i++;
            }
        }

        public static int binarySearch(int[] arr, int x)
        {
            int xIndex, middleIndex;
            int leftInterval = 0, rightInterval = arr.Length - 1;
            while (true)
            {
                if (leftInterval >= rightInterval)
                {
                    xIndex = -1;
                    break;
                }
                if (x == arr[leftInterval])
                {
                    xIndex = leftInterval;
                    break;
                }
                else if (x == arr[rightInterval])
                {
                    xIndex = rightInterval;
                    break;
                }
                middleIndex = leftInterval + (rightInterval - leftInterval) / 2;
                if (x == arr[middleIndex])
                {
                    xIndex = middleIndex;
                    break;
                }
                else if (x > arr[middleIndex])
                {
                    leftInterval = middleIndex + 1;
                }
                else
                {
                    rightInterval = middleIndex - 1;
                }
            }
            return xIndex;
        }

        static void Main(string[] args)
        {
            int length = 10_000, maxItem = 9_999;
            int[] data = generateData(length, maxItem);

            bubbleSort(data);

            printIntArr(data);


            int x = 0, xIndex;
            Random rand = new Random();
            foreach (int item in data) //test all array items
            {
                xIndex = binarySearch(data, item);
                Console.WriteLine($"[{item} : {xIndex} !OK!]");
            }
            for (int i = 0; i < 2000; i++) //test random items
            {
                x = rand.Next(maxItem);
                xIndex = binarySearch(data, x);
                Console.Write($"[{x} : {xIndex} !OK!]");
            }

            while (true)
            {

                int x0 = Convert.ToInt32(Console.ReadLine());

                var startTime = System.Diagnostics.Stopwatch.StartNew();

                int xIndex0 = binarySearch(data, x0);

                startTime.Stop();
                var startTimeResult = startTime.Elapsed;
                var elapsedTime = String.Format("{0:00}:{1:00}:{2:00000}",
                    startTimeResult.Minutes,
                    startTimeResult.Seconds,
                    startTimeResult.Milliseconds
                );
                Console.WriteLine($"[{x} : {xIndex0} !OK!]");
                Console.WriteLine($"For {length}: {elapsedTime}");
            }
            Console.ReadKey();
        }
    }
}
