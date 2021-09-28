using System;
using System.Diagnostics;

namespace Sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;
            Console.Write("Please input the length of an array: ");
            n = Convert.ToInt32(Console.ReadLine());
            double[] arr = new double[n];
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                arr[i] = rnd.NextDouble() * 100;
            }
            HeapSort.Sort(arr, out TimeSpan bubbleTime, out long mem);
            Console.WriteLine(bubbleTime);
            Console.WriteLine(@"Please choose one or several sorting alghorithms from the following options:
1. Insertion sort
2. Bubble sort
3. Quick sort
4. Heap sort
5. Merge sort
6. All");
            string selection = Console.ReadLine();

        }
    }


    static class InsertionSort
    {
        public static void Sort(double[] arr, out TimeSpan sortTime, out long usedMemory)
        {
            Stopwatch stopwatch = new Stopwatch();
            var startMemory = GC.GetTotalMemory(true);
            double[] newarr = new double[arr.Length];
            arr.CopyTo(newarr, 0);
            stopwatch.Start();
            for (int i = 0; i < newarr.Length; i++)
            {
                double current = newarr[i];
                int j = i - 1;
                while (j >= 0 && newarr[j] > current)
                {
                    newarr[j + 1] = newarr[j];
                    j--;
                }
                newarr[j + 1] = current;
            }
            stopwatch.Stop();
            sortTime = stopwatch.Elapsed;
            var endMemory = GC.GetTotalMemory(false);
            usedMemory = endMemory - startMemory;
        }
    }

    static class BubbleSort
    {
        public static void Sort(double[] arr, out TimeSpan sortTime, out long usedMemory)
        {
            Stopwatch stopwatch = new Stopwatch();
            var startMemory = GC.GetTotalMemory(true);
            double[] newarr = new double[arr.Length];
            arr.CopyTo(newarr, 0);
            stopwatch.Start();
            bool isSorted;
            for (int i = 0; i < newarr.Length; i++)
            {
                isSorted = true;
                for (int j = 1; j < newarr.Length - i; j++)
                {
                    if (newarr[j] < newarr[j-1])
                    {
                        double current = newarr[j];
                        newarr[j] = newarr[j - 1];
                        newarr[j - 1] = current;
                        isSorted = false;
                    }
                }
                if (isSorted == true)
                {
                    break;
                }
            }
            stopwatch.Stop();
            sortTime = stopwatch.Elapsed;
            var endMemory = GC.GetTotalMemory(true);
            usedMemory = endMemory - startMemory;
        }
    }

    static class MergeSort
    {
        public static void Sort(double[] arr, out TimeSpan sortTime, out long usedMemory)
        {
            Stopwatch stopwatch = new Stopwatch();
            var startMemory = GC.GetTotalMemory(false);
            double[] newarr = new double[arr.Length];
            arr.CopyTo(newarr, 0);
            stopwatch.Start();
            Devide(newarr).CopyTo(newarr,0);
            stopwatch.Stop();
            sortTime = stopwatch.Elapsed;
            var endMemory = GC.GetTotalMemory(false);
            usedMemory = endMemory - startMemory;
        }

        private static double[] Devide(double[] arr)
        {
            if (arr.Length == 1)
            {
                return arr;
            }
            else
            {
                double[] arr1 = new double[arr.Length / 2];
                double[] arr2 = new double[arr.Length - arr.Length / 2];
                for (int i = 0; i < arr.Length/2; i++)
                {
                    arr1[i] = arr[i];
                }
                for (int i = 0; i < arr.Length - arr.Length/2; i++)
                {
                    arr2[i] = arr[arr.Length / 2 + i];
                }

                arr1 = Devide(arr1);
                arr2 = Devide(arr2);

                return Merge(arr1, arr2);
            }
        }

        private static double[] Merge(double[] a, double[] b)
        {
            double[] c = new double[a.Length + b.Length];
            int i = 0;
            int j = 0;
            while (i < a.Length && j < b.Length) //heres a problem 
            {
                if (a[i]>b[j])
                {
                    c[i+j] = b[j];
                    j++;
                }
                else
                {
                    c[i+j] = a[i];
                    i++;
                }
            }
            while (i < a.Length) 
            {
                c[i+j] = a[i];
                i++;
            }
            while (j < b.Length)
            {
                c[i+j] = b[j];
                j++;
            }
            return c;
        }
    }

    static class QuickSort
    {
        public static void Sort(double[] arr, out TimeSpan sortTime, out long usedMemory)
        {
            Stopwatch stopwatch = new Stopwatch();
            var startMemory = GC.GetTotalMemory(false);
            double[] newarr = new double[arr.Length];
            arr.CopyTo(newarr, 0);
            stopwatch.Start();
            Devide(newarr, 0, newarr.Length-1);
            for (int i = 0; i < newarr.Length; i++)
            {
                Console.WriteLine(newarr[i]);
            }
            stopwatch.Stop();
            sortTime = stopwatch.Elapsed;
            var endMemory = GC.GetTotalMemory(false);
            usedMemory = endMemory - startMemory;
        }

        private static void Devide(double[] arr, int low , int high)
        {
            if (low < high)
            {
                int pivotLoc = Partition(arr, low, high);
                Devide(arr, low, pivotLoc-1);
                Devide(arr, pivotLoc+1, high);
            }
        }

        private static int Partition(double[] arr, int low, int high)
        {
            double pivot = arr[low];
            while (true)
            {
                while (arr[low] < pivot)
                {
                    low++;
                }
                while (arr[high] > pivot)
                {
                    high--;
                }
                if (low<high)
                {
                    if (arr[low] == arr[high])
                    {
                        return high;
                    }
                    double current = arr[low];
                    arr[low] = arr[high];
                    arr[high] = current;
                }
                else
                {
                    return high;
                }
            }
        }
    }

    static class HeapSort
    {
        public static void Sort(double[] arr, out TimeSpan sortTime, out long usedMemory)
        {
            Stopwatch stopwatch = new Stopwatch();
            var startMemory = GC.GetTotalMemory(false);
            double[] newarr = new double[arr.Length];
            arr.CopyTo(newarr, 0);
            stopwatch.Start();
            int n = arr.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(newarr, n, i);
            }
            for (int i = n - 1; i >= 0; i--)
            {
                double current = newarr[0];
                newarr[0] = newarr[i];
                newarr[i] = current;
                Heapify(newarr, i, 0);
            }
            stopwatch.Stop();
            sortTime = stopwatch.Elapsed;
            var endMemory = GC.GetTotalMemory(false);
            usedMemory = endMemory - startMemory;
            for (int i = 0; i < newarr.Length; i++)
            {
                Console.WriteLine(newarr[i]);
            }
        }

        private static void Heapify(double[] arr, int n, int i)
        {
            int max = i;
            int left = 2*i + 1;
            int right = 2*i + 2;

            if (left < n && arr[left] > arr[max])
            {
                max = left;
            }
            if (right < n && arr[right] > arr[max])
            {
                max = right;
            }
            if (max != i)
            {
                double current = arr[i];
                arr[i] = arr[max];
                arr[max] = current;
                Heapify(arr, n , max);
            }
        }
    }
}
