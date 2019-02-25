using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;

namespace SortMethods
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<GraphItem> allGraphItems = new List<GraphItem>();
        public int[] masterArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
        public int maxBarHeight;
        public int totalItems;
        public int speed = 5;
        public int waitShort = 10;
        public int waitLong = 60;

        public MainWindow()
        {
            InitializeComponent();
            InitializeAllGraphItems();
            maxBarHeight = (int)rBar00.Height;
            totalItems = masterArray.Length;

            ResetAllBarColors();
            ShuffleArray();
            UpdateValues();
            UpdateAllBarHeights();
        }

        public void InitializeAllGraphItems()
        {
            allGraphItems.Add(new GraphItem(rBar00, lblItemIndex00, rIndexBackground00, lblItemValue00, rValueBackground00));
            allGraphItems.Add(new GraphItem(rBar01, lblItemIndex01, rIndexBackground01, lblItemValue01, rValueBackground01));
            allGraphItems.Add(new GraphItem(rBar02, lblItemIndex02, rIndexBackground02, lblItemValue02, rValueBackground02));
            allGraphItems.Add(new GraphItem(rBar03, lblItemIndex03, rIndexBackground03, lblItemValue03, rValueBackground03));
            allGraphItems.Add(new GraphItem(rBar04, lblItemIndex04, rIndexBackground04, lblItemValue04, rValueBackground04));
            allGraphItems.Add(new GraphItem(rBar05, lblItemIndex05, rIndexBackground05, lblItemValue05, rValueBackground05));
            allGraphItems.Add(new GraphItem(rBar06, lblItemIndex06, rIndexBackground06, lblItemValue06, rValueBackground06));
            allGraphItems.Add(new GraphItem(rBar07, lblItemIndex07, rIndexBackground07, lblItemValue07, rValueBackground07));
            allGraphItems.Add(new GraphItem(rBar08, lblItemIndex08, rIndexBackground08, lblItemValue08, rValueBackground08));
            allGraphItems.Add(new GraphItem(rBar09, lblItemIndex09, rIndexBackground09, lblItemValue09, rValueBackground09));
            allGraphItems.Add(new GraphItem(rBar10, lblItemIndex10, rIndexBackground10, lblItemValue10, rValueBackground10));
            allGraphItems.Add(new GraphItem(rBar11, lblItemIndex11, rIndexBackground11, lblItemValue11, rValueBackground11));
            allGraphItems.Add(new GraphItem(rBar12, lblItemIndex12, rIndexBackground12, lblItemValue12, rValueBackground12));
            allGraphItems.Add(new GraphItem(rBar13, lblItemIndex13, rIndexBackground13, lblItemValue13, rValueBackground13));
            allGraphItems.Add(new GraphItem(rBar14, lblItemIndex14, rIndexBackground14, lblItemValue14, rValueBackground14));
            allGraphItems.Add(new GraphItem(rBar15, lblItemIndex15, rIndexBackground15, lblItemValue15, rValueBackground15));
            allGraphItems.Add(new GraphItem(rBar16, lblItemIndex16, rIndexBackground16, lblItemValue16, rValueBackground16));
            allGraphItems.Add(new GraphItem(rBar17, lblItemIndex17, rIndexBackground17, lblItemValue17, rValueBackground17));
            allGraphItems.Add(new GraphItem(rBar18, lblItemIndex18, rIndexBackground18, lblItemValue18, rValueBackground18));
            allGraphItems.Add(new GraphItem(rBar19, lblItemIndex19, rIndexBackground19, lblItemValue19, rValueBackground19));
        }

        public void ShuffleArray()
        {
            int n = masterArray.Length;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);

                if (k != n) //If k = n, bitwise swap would result in 0, so skip.
                {
                    masterArray[k] ^= masterArray[n];
                    masterArray[n] ^= masterArray[k];
                    masterArray[k] ^= masterArray[n];
                }
            }
        }

        public void ReverseValues()
        {
            Array.Reverse(masterArray);
            int length = masterArray.Length;
            for (int i = 0; i < length; i++)
            {
                allGraphItems[i]._itemValue.Content = masterArray[i];
            }
        }

        public void UpdateAllBarHeights()
        {
            int length = masterArray.Length;
            for (int i = 0; i < length; i++)
            {
                allGraphItems[i]._bar.Height = masterArray[i] * (maxBarHeight / length);
            }
        }

        public void ResetAllBarColors()
        {
            foreach (GraphItem x in allGraphItems)
            {
                x.SetAllColors((Color)Application.Current.FindResource("UnvisitedItemColor"));
            }
        }

        private void UpdateValues()
        {
            int length = masterArray.Length;
            for (int i = 0; i < length; i++)
            {
                allGraphItems[i]._itemValue.Content = masterArray[i];
            }
        }

        private void btnShuffle_Click(object sender, RoutedEventArgs e)
        {
            ShuffleArray();
            UpdateValues();
            UpdateAllBarHeights();
            ResetAllBarColors();
        }

        private void btnReverse_Click(object sender, RoutedEventArgs e)
        {
            ReverseValues();
            UpdateAllBarHeights();
            ResetAllBarColors();
        }

        public void btnRun_Click(object sender, RoutedEventArgs e)
        {
            Sorts newSort = new Sorts(); //For debugging
            switch (cbSelectSortType.SelectedValue)
            {
                case SortType.InsertionSort:
                    {
                        SwitchTaskFactory(GraphicalInsertionSort);
                        break;
                    }
                //case SortType.MergeSort:
                //    {
                //        int[] newArray = newSort.MergeSort(masterArray);

                //        string s = "";
                //        foreach (var item in masterArray)
                //        {
                //            s += $"{item}\n";
                //        }
                //        MessageBox.Show(s);


                //        string s2 = "";
                //        foreach (var item in newArray)
                //        {
                //            s2 += $"{item}\n";
                //        }
                //        MessageBox.Show(s2);

                //        break;
                //    }
                case SortType.SelectionSort:
                    {
                        SwitchTaskFactory(GraphicalSelectionSort);
                        break;
                    }
                case SortType.ShellSort:
                    {
                        SwitchTaskFactory(GraphicalShellSort);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            void SwitchTaskFactory(Action selectedSort)
            {
                Task.Factory.StartNew(() =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        btnRun.IsEnabled = false;
                        btnShuffle.IsEnabled = false;
                        btnReverse.IsEnabled = false;
                    });

                    selectedSort();

                    Dispatcher.Invoke(() =>
                    {
                        btnRun.IsEnabled = true;
                        btnShuffle.IsEnabled = true;
                        btnReverse.IsEnabled = true;
                    });
                });
            }
        }

        //SORTS WITH SPECIAL STEPS
        #region Sorts

        public void GraphicalInsertionSort()
        {
            int length = masterArray.Length;

            for (int i = 0; i < length; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    int k = j - 1;

                    Dispatcher.Invoke(() =>
                    {
                        allGraphItems[j].SetAllColors((Color)Application.Current.FindResource("ActiveItemColor1"));
                        allGraphItems[k].SetAllColors((Color)Application.Current.FindResource("ActiveItemColor2"));
                    });

                    Thread.Sleep(waitShort * speed);

                    if (masterArray[j] < masterArray[k])
                    {
                        Thread.Sleep(waitShort * speed);

                        masterArray[j] ^= masterArray[k];
                        masterArray[k] ^= masterArray[j];
                        masterArray[j] ^= masterArray[k];

                        SwapForUI(j, k);

                        Thread.Sleep(waitLong * speed);

                        FinalizeColors(k, j);
                    }
                    else
                    {
                        FinalizeColors(k, j);
                        break;
                    }
                }
            }

            void FinalizeColors(int k, int j)
            {
                Dispatcher.Invoke(() =>
                {
                    if (masterArray[k] == k)
                    {
                        //If array[k] = k, color sorted
                        allGraphItems[k].SetAllColors((Color)Application.Current.FindResource("SortedItemColor"));
                    }
                    else
                    {
                        //Set k to unsorted instead of active.
                        allGraphItems[k].SetAllColors((Color)Application.Current.FindResource("UnsortedItemColor"));
                    }

                    if (masterArray[j] == j)
                    {
                        //If array[j] = j, color sorted
                        allGraphItems[j].SetAllColors((Color)Application.Current.FindResource("SortedItemColor"));
                    }
                    else
                    {
                        //Set j to unsorted instead of active.
                        allGraphItems[j].SetAllColors((Color)Application.Current.FindResource("UnsortedItemColor"));
                    }
                });
            }
        }


        public void GraphicalSelectionSort()
        {
            int length = masterArray.Length;

            for (int i = 0; i < length; i++)
            {
                int minimum = i;
                for (int j = i + 1; j < length; j++)
                {
                    Dispatcher.Invoke(() =>
                    {
                        allGraphItems[i].SetAllColors((Color)Application.Current.FindResource("ActiveItemColor1"));
                        allGraphItems[j].SetAllColors((Color)Application.Current.FindResource("ActiveItemColor2"));
                    });

                    Thread.Sleep(waitShort * speed);

                    if (masterArray[j] < masterArray[i])
                    {
                        Thread.Sleep(waitShort * speed);
                        minimum = j;

                        masterArray[j] ^= masterArray[i];
                        masterArray[i] ^= masterArray[j];
                        masterArray[j] ^= masterArray[i];

                        SwapForUI(i, j);

                        Thread.Sleep(waitLong * speed);
                    }

                    Dispatcher.Invoke(() =>
                    {
                            //Set j to unsorted instead of active.
                            allGraphItems[j].SetAllColors((Color)Application.Current.FindResource("UnsortedItemColor"));
                    });
                }

                Dispatcher.Invoke(() =>
                {
                    if (masterArray[i] == i)
                    {
                            //If array[i] = i, color sorted
                            allGraphItems[i].SetAllColors((Color)Application.Current.FindResource("SortedItemColor"));
                    }
                    else
                    {
                            //Set i to unsorted instead of active.
                            allGraphItems[i].SetAllColors((Color)Application.Current.FindResource("UnsortedItemColor"));
                    }
                });
            }
        }

        public void GraphicalShellSort()
        {
            int length = masterArray.Length;
            int step = 1;
            HashSet<int> indexesInSet = new HashSet<int>();

            while (step < length / 3)
            {
                //Using Knuth's increment sequence (1, 4, 13, 40, ...)
                step = 3 * step + 1;
            }

            while (step >= 1)
            {
                for (int i = step; i < length; i++)
                {
                    indexesInSet.Add(i);
                    indexesInSet.Add(i - step);

                    Dispatcher.Invoke(() =>
                    {
                        allGraphItems[i].SetAllColors((Color)Application.Current.FindResource("ActiveItemColor1"));
                        allGraphItems[i - step].SetAllColors((Color)Application.Current.FindResource("ActiveItemColor2"));
                    });

                    Thread.Sleep(waitShort * speed);
                    for (int j = i; j >= step && masterArray[j] < masterArray[j - step]; j -= step)
                    {
                        indexesInSet.Add(j - step);

                        masterArray[j - step] ^= masterArray[j];
                        masterArray[j] ^= masterArray[j - step];
                        masterArray[j - step] ^= masterArray[j];

                        SwapForUI(j, j - step);

                        //If not in order swap colors and values
                        //Dispatcher.Invoke(() =>
                        //{
                        //    int temp = (int)allGraphItems[j - step]._itemValue.Content;

                        //    allGraphItems[j - step]._itemValue.Content = allGraphItems[j]._itemValue.Content;
                        //    allGraphItems[j - step].SetAllColors((Color)Application.Current.FindResource("ActiveItemColor2"));
                        //    allGraphItems[j - step].UpdateBarHeight();

                        //    allGraphItems[j]._itemValue.Content = temp;
                        //    allGraphItems[j].SetAllColors((Color)Application.Current.FindResource("ActiveItemColor1"));
                        //    allGraphItems[j].UpdateBarHeight();
                        //});
                        Thread.Sleep(waitLong * speed);
                    }

                    //Reset colors for all examined indexes
                    Dispatcher.Invoke(() =>
                    {
                        foreach (int x in indexesInSet)
                        {
                            if (masterArray[x] == x)
                            {
                                allGraphItems[x].SetAllColors((Color)Application.Current.FindResource("SortedItemColor"));
                            }
                            else
                            {
                                allGraphItems[x].SetAllColors((Color)Application.Current.FindResource("UnsortedItemColor"));
                            }
                        }
                    });
                    indexesInSet.Clear();
                }
                step /= 3;
            }
        }

        #endregion

        #region Sort Helpers
        private void SwapForUI(int a, int b)
        {
            Dispatcher.Invoke(() =>
            {
                allGraphItems[a].SetAllColors((Color)Application.Current.FindResource("ActiveItemColor2"));
                allGraphItems[a].UpdateBarHeight(masterArray[a], maxBarHeight, totalItems);
                allGraphItems[a].UpdateValue(masterArray[a]);

                allGraphItems[b].SetAllColors((Color)Application.Current.FindResource("ActiveItemColor1"));
                allGraphItems[b].UpdateBarHeight(masterArray[b], maxBarHeight, totalItems);
                allGraphItems[b].UpdateValue(masterArray[b]);
            });
        }
        #endregion

        private void sldrSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            speed = (int)sldrSpeed.Value;
        }
    }

    public static class ThreadSafeRandom
    {
        [ThreadStatic] private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }
}
