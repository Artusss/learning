using System;
using System.Collections.Generic;

namespace Sorts
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

        public static int[] bubbleSort(int[] arr)
        {
            for (int k = 0; k < arr.Length; k++)
            {
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    if (arr[i] < arr[i + 1])
                    {
                        int t = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = t;
                    }
                }
            }
            return arr;
        }

        public static int[] selectSort(int[] arr)
        {
            int iMax;
            for (int i = 0; i < arr.Length; i++)
            {
                iMax = i;
                for (int j = i; j < arr.Length; j++)
                {
                    if (arr[j] >= arr[iMax])
                    {
                        iMax = j;
                    }
                }
                int t = arr[i];
                arr[i] = arr[iMax];
                arr[iMax] = t; 
            }
            return arr;
        }


        public static int[] quickSort(int[] arr)
        {
            if (arr.Length < 2)
            {
                return arr;
            }
            int iPivot = 0;
            var midList = new List<int>() { arr[iPivot] };
            var leftList = new List<int>();
            var rightList = new List<int>();
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] >= arr[iPivot])
                {
                    leftList.Add(arr[i]);              
                }
                else
                {
                    rightList.Add(arr[i]);
                }
            }
            var fullList = new List<int>();
            fullList.AddRange(quickSort(leftList.ToArray()));
            fullList.AddRange(midList);
            fullList.AddRange(quickSort(rightList.ToArray()));
            int[] fullArr = fullList.ToArray();

            return fullArr;
        }
        static void Main(string[] args)
        {
            int length = 100, maxItem = 100;
            int[] data = generateData(length, maxItem);

            printIntArr(data);

            int[] bubbleSortArr = bubbleSort(data);
            printIntArr(bubbleSortArr);

            int[] selectSortArr = selectSort(data);
            printIntArr(selectSortArr);

            int[] quickSortArr = quickSort(data);
            printIntArr(quickSortArr);

            Console.ReadKey();
        }
    }
}
