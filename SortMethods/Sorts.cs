using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortMethods
{
    public enum SortType
    {
        BubbleSort,
        InsertionSort,
        //MergeSort,
        SelectionSort,
        ShellSort
    }

    //Master copies of original sort methods for reference
    public class Sorts
    {

        public int[] BubbleSort(int[] array)
        {
            int length = array.Length;
            bool swapMade = true;

            while (swapMade)
            {
                swapMade = false;

                for (int i = 0; i < length-1; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        array[i + 1] ^= array[i];
                        array[i] ^= array[i + 1];
                        array[i + 1] ^= array[i];

                        swapMade = true;
                    }
                }
            }

            return array;
        }

        public int[] InsertionSort(int[] array)
        {
            int length = array.Length;

            for (int i = 0; i < length; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    if (array[j] < array[j - 1])
                    {
                        array[j] ^= array[i];
                        array[i] ^= array[j];
                        array[j] ^= array[i];
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return array;
        }

        public int[] MergeSort(int[] array)
        {
            MergeSort_Sort(array, 0, array.Length - 1);

            void MergeSort_Merge(int[] arr, int firstIndex, int middleIndex, int lastIndex)
            {
                int leftSize = middleIndex - firstIndex + 1;
                int rightSize = lastIndex - middleIndex;

                int[] tempLeft = new int[leftSize];
                int[] tempRight = new int[rightSize];

                //Split array into left and right halves
                for (int i = 0; i < leftSize; i++)
                {
                    tempLeft[i] = arr[firstIndex + i];
                }

                for (int j = 0; j < rightSize; j++)
                {
                    tempRight[j] = arr[middleIndex + 1 + j];
                }

                //When
                int leftFirst = 0; // Initial index of first subarray 
                int rightFirst = 0; // Initial index of second subarray 

                while (leftFirst < leftSize && rightFirst < rightSize)
                {
                    if (tempLeft[leftFirst] <= tempRight[rightFirst])
                    {
                        arr[firstIndex] = tempLeft[leftFirst];
                        leftFirst++;
                    }
                    else
                    {
                        arr[firstIndex] = tempRight[rightFirst];
                        rightFirst++;
                    }
                    firstIndex++;
                }

                while (leftFirst < leftSize)
                {
                    arr[firstIndex] = tempLeft[leftFirst];
                    leftFirst++;
                    firstIndex++;
                }

                while (rightFirst < rightSize)
                {
                    arr[firstIndex] = tempRight[rightFirst];
                    rightFirst++;
                    firstIndex++;
                }
            }

            void MergeSort_Sort(int[] arr, int firstIndex, int lastIndex)
            {
                if (firstIndex < lastIndex)
                {
                    int middleIndex = firstIndex + (lastIndex - firstIndex) / 2;

                    MergeSort_Sort(arr, firstIndex, middleIndex);
                    MergeSort_Sort(arr, middleIndex + 1, lastIndex);

                    MergeSort_Merge(arr, firstIndex, middleIndex, lastIndex);
                }
            }

            return array;
        }

        //public int[] MergeSort(int[] array)
        //{
        //    int cap = 7; //Starts insertion sort on arrays 8 or less items long.

        //    int[] arrayDestination = new int[array.Length];

        //    //for (int a = 0; a < array.Length; a++)
        //    //{
        //    //    arrayDestination[a] = array[a];
        //    //}

        //    Sort(arrayDestination, array, 0, array.Length - 1);

        //    void Merge(int[] source, int[] destination, int lo, int mid, int hi)
        //    {
        //        int i = lo;
        //        int j = mid + 1;

        //        for (int k = lo; k <= hi; k++)
        //        {
        //            if (i > mid)
        //            {
        //                destination[k] = source[j++];
        //            }
        //            else if(j > hi)
        //            {
        //                destination[k] = source[i];
        //            }
        //            else if(source[j] < source[i])
        //            {
        //                destination[k] = source[j++];
        //            }
        //            else
        //            {
        //                destination[k] = source[i++];
        //            }
        //        }
        //    }

        //    void Sort(int[] source, int[] destination, int lo, int hi)
        //    {
        //        if (hi <= lo + cap)
        //        {
        //            InternalInsertionSort(destination, lo, hi);
        //            return;
        //        }

        //        int mid = lo + (hi - lo) / 2;

        //        Sort(source, destination, lo, mid);
        //        Sort(source, destination, mid + 1, hi);

        //        if (source[mid+1] >= source[mid])
        //        {
        //            for (int i = lo; i <= hi; i++)
        //            {
        //                destination[i] = source[i];
        //                return;
        //            }
        //        }

        //        Merge(source, destination, lo, mid, hi);
        //    }

        //    void InternalInsertionSort(int[] smallArray, int lo, int hi)
        //    {
        //        for (int i = lo; i <= hi; i++)
        //        {
        //            for (int j = i; j > lo && smallArray[j] < smallArray[j-1]; j--)
        //            {
        //                smallArray[j] ^= smallArray[j - 1];
        //                smallArray[j - 1] ^= smallArray[j];
        //                smallArray[j] ^= smallArray[j - 1];
        //            }
        //        }
        //    }

        //    return arrayDestination;
        //}

        public int[] SelectionSort(int[] array)
        {
            int length = array.Length;

            for (int i = 0; i < length; i++)
            {
                int minimum = i;
                for (int j = i+1; j < length; j++)
                {
                    if (array[j] < array[i])
                    {
                        minimum = j;
                        array[j] ^= array[i];
                        array[i] ^= array[j];
                        array[j] ^= array[i];
                    }
                }
            }

            return array;
        }


        public int[] ShellSort(int[] array)
        {
            int length = array.Length;

            int step = 1;
            while (step < length / 3)
            {
                //Using Knuth's increment sequence (1, 4, 13, 40, ...)
                step = 3 * step + 1;
            }
                while (step >= 1)
                {
                    for (int i = step; i < length; i++)
                    {
                        for (int j = i; j >= step && array[j] < array[j - step]; j -= step)
                        {
                            array[j] ^= array[j - step];
                            array[j - step] ^= array[j];
                            array[j] ^= array[j - step];
                        }
                    }
                    step /= 3;
                }
                
            return array;
        }
    }
}
