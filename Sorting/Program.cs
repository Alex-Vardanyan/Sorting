using System;
using System.Diagnostics;
using System.Collections.Generic;

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

            Console.WriteLine(@"Please choose one or several sorting alghorithms from the following options:
1. Insertion sort
2. Bubble sort
3. Quick sort
4. Heap sort
5. Merge sort
6. All");

            string selection = Console.ReadLine();
            if (selection.Contains(' '))
            {
                selection = selection.Replace(" ", "");
            }
            List<int> numbers = new List<int>();
            if (selection.Length == 1 && Convert.ToInt32(selection) >= 1 && Convert.ToInt32(selection) <= 6)
            {
                if (selection == "6")
                {
                    for (int i = 0; i < 5; i++)
                    {
                        numbers.Add(i);
                    }
                    Sorting(arr, numbers);
                }
                else
                {
                    numbers.Add(Convert.ToInt32(selection) - 1);
                    Sorting(arr, numbers);
                }
            }

            else if (selection.Length == 3 && selection[1] == '-')
            {
                if (Convert.ToInt32(selection[0].ToString()) > 0 && Convert.ToInt32(selection[2].ToString()) > 0)
                {
                    if (Convert.ToInt32(selection[2].ToString()) < 6 && Convert.ToInt32(selection[0].ToString()) < Convert.ToInt32(selection[2].ToString()))
                    {
                        for (int i = Convert.ToInt32(selection[0].ToString()) - 1; i < Convert.ToInt32(selection[2].ToString()); i++)
                        {
                            numbers.Add(i);
                        }
                        Sorting(arr, numbers);
                    }
                    else
                    {
                        throw new Exception("The selection went wrong, try again");
                    }
                }
                else
                {
                    throw new Exception("The selection went wrong, try again");
                }
            }

            else if (selection.Contains(','))
            {
                selection = selection.Replace(",", "");
                for (int i = 0; i < selection.Length; i++)
                {
                    if (Convert.ToInt32(selection[i].ToString()) > 0 && Convert.ToInt32(selection[i].ToString()) < 6)
                    {
                        numbers.Add(Convert.ToInt32(selection[i].ToString()) - 1);
                    }
                    else
                    {
                        throw new Exception("The selection went wrong, try again");
                    }
                }
                Sorting(arr, numbers);
            }
            
            else
            {
                throw new Exception("The selection went wrong, try again");
            }
        }

        private static void Sorting(double[] arr, List<int> numbers)
        {

            var InsertionSort = new Insertion();
            var BubbleSort = new Bubble();
            var QuickSort = new Quick();
            var HeapSort = new Heap();
            var MergeSort = new Merge();
            ISorter[] Sorters = new ISorter[] { InsertionSort, BubbleSort, QuickSort, HeapSort, MergeSort };
            TimeSpan minTime;

            List<TimeSpan> timeList = new List<TimeSpan>();
            List<long> memoryList = new List<long>();

            foreach (var i in numbers)
            {
                Sorters[i].Sort(arr, out TimeSpan sortTime, out long usedMemory);
                timeList.Add(sortTime);
                memoryList.Add(usedMemory);
            }
            minTime = timeList[0];
            foreach (var time in timeList)
            {
                if (minTime > time)
                {
                    minTime = time;
                }
            }
            int j = 0;
            foreach (var i in numbers)
            {
                Console.WriteLine(Sorters[i].ToString());
                Console.Write("Running Time: ");
                    if (timeList[j] == minTime && numbers.Count != 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("The Fastest! ");
                    Console.WriteLine(timeList[j]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(timeList[j]);
                }
                Console.WriteLine($"Memory Used {memoryList[j]}");
                Console.WriteLine();
                j++;
            }
        }
    }

    interface ISorter
    {   
        public void Sort(double[] arr, out TimeSpan sortTime, out long usedMemory);
    }

    class Insertion : ISorter
    {
        public void Sort(double[] arr, out TimeSpan sortTime, out long usedMemory)
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

        public override string ToString()
        {
            return "Insertion Sort";
        }
    }

    class Bubble : ISorter
    {
        public void Sort(double[] arr, out TimeSpan sortTime, out long usedMemory)
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

        public override string ToString()
        {
            return "Bubble Sort";
        }
    }

    class Merge : ISorter
    {
        public void Sort(double[] arr, out TimeSpan sortTime, out long usedMemory)
        {
            Stopwatch stopwatch = new Stopwatch();
            var startMemory = GC.GetTotalMemory(false);
            double[] newarr = new double[arr.Length];
            arr.CopyTo(newarr, 0);
            stopwatch.Start();
            Devide(newarr, out long usedMemoryInner).CopyTo(newarr,0);
            stopwatch.Stop();
            sortTime = stopwatch.Elapsed;
            var endMemory = GC.GetTotalMemory(false);
            usedMemory = endMemory - startMemory + usedMemoryInner;
        }

        private double[] Devide(double[] arr, out long usedMemory)
        {
            var startMemory = GC.GetTotalMemory(false);
            if (arr.Length == 1)
            {
                var endMemory = GC.GetTotalMemory(false);
                usedMemory = endMemory - startMemory;
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

                arr1 = Devide(arr1, out long usedMemoryInner1);
                arr2 = Devide(arr2, out long usedMemoryInner2);

                double[] c =  merge(arr1, arr2, out long usedMemoryInner3);

                var endMemory = GC.GetTotalMemory(false);
                usedMemory = endMemory - startMemory + usedMemoryInner1 + usedMemoryInner2 + usedMemoryInner3;
                return c;
            }
        }

        private double[] merge(double[] a, double[] b, out long usedMemory)
        {
            var startMemory = GC.GetTotalMemory(false);
            double[] c = new double[a.Length + b.Length];
            int i = 0;
            int j = 0;
            while (i < a.Length && j < b.Length)
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
            var endMemory = GC.GetTotalMemory(false);
            usedMemory = endMemory - startMemory;
            return c;
        }

        public override string ToString()
        {
            return "Merge Sort";
        }
    }

    class Quick : ISorter
    {
        public void Sort(double[] arr, out TimeSpan sortTime, out long usedMemory)
        {
            Stopwatch stopwatch = new Stopwatch();
            var startMemory = GC.GetTotalMemory(false);
            double[] newarr = new double[arr.Length];
            arr.CopyTo(newarr, 0);
            stopwatch.Start();
            Devide(newarr, 0, newarr.Length-1, out long usedMemoryDevision);
            stopwatch.Stop();
            sortTime = stopwatch.Elapsed;
            var endMemory = GC.GetTotalMemory(false);
            usedMemory = endMemory - startMemory + usedMemoryDevision;
        }

        private void Devide(double[] arr, int low , int high, out long usedMemoryDevision)
        {
            if (low < high)
            {
                var startMemory = GC.GetTotalMemory(false);
                int pivotLoc = Partition(arr, low, high, out long usedMemoryPartioning);
                Devide(arr, low, pivotLoc-1, out long usedMemory1);
                Devide(arr, pivotLoc+1, high, out long usedMemory2);
                var endMemory = GC.GetTotalMemory(false);
                usedMemoryDevision = endMemory - startMemory + usedMemoryPartioning + usedMemory1 + usedMemory2;
            }
            else
            {
                usedMemoryDevision = 0;
            }
        }

        private int Partition(double[] arr, int low, int high, out long usedMemoryPartioning)
        {
            var startMemory = GC.GetTotalMemory(false);
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

                        var endMemory = GC.GetTotalMemory(false);
                        usedMemoryPartioning = endMemory - startMemory;
                        return high;
                    }
                    double current = arr[low];
                    arr[low] = arr[high];
                    arr[high] = current;
                }
                else
                {
                    var endMemory = GC.GetTotalMemory(false);
                    usedMemoryPartioning = endMemory - startMemory;
                    return high;
                }
            }
        }

        public override string ToString()
        {
            return "Quick Sort";
        }
    }

    class Heap : ISorter
    {
        public void Sort(double[] arr, out TimeSpan sortTime, out long usedMemory)
        {
            Stopwatch stopwatch = new Stopwatch();
            var startMemory = GC.GetTotalMemory(false);
            long usedMemoryInner = 0;
            double[] newarr = new double[arr.Length];
            arr.CopyTo(newarr, 0);
            stopwatch.Start();
            int n = arr.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(newarr, n, i, out long usedMemory1);
                usedMemoryInner += usedMemory1;
            }
            for (int i = n - 1; i >= 0; i--)
            {
                double current = newarr[0];
                newarr[0] = newarr[i];
                newarr[i] = current;
                Heapify(newarr, i, 0, out long usedMemory2);
                usedMemoryInner += usedMemory2;
            }
            stopwatch.Stop();
            sortTime = stopwatch.Elapsed;
            var endMemory = GC.GetTotalMemory(false);
            usedMemory = endMemory - startMemory + usedMemoryInner;
        }

        private void Heapify(double[] arr, int n, int i, out long usedMemoryHeapify)
        {
            var startMemory = GC.GetTotalMemory(false);
            long usedMemoryInner = 0;
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
                Heapify(arr, n , max, out long usedMemory1);
                usedMemoryInner += usedMemory1;
            }
            var endMemory = GC.GetTotalMemory(false);
            usedMemoryHeapify = endMemory - startMemory + usedMemoryInner;
        }

        public override string ToString()
        {
            return "Heap Sort";
        }
    }
}
