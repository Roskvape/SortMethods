using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortMethods
{
    public enum SortType
    {
        InsertionSort,
        SelectionSort,
        ShellSort
    }

    //Master copies of original sort methods for reference
    public class Sorts
    {
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
