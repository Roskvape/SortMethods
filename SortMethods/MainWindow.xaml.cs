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
        public int maxBarHeight;
        public int speed = 5;
        public int waitShort = 10;
        public int waitLong = 60;
        public MainWindow()
        {
            InitializeComponent();
            InitializeAllGraphItems();
            maxBarHeight = (int)rBar00.Height;

            ResetAllBarColors();
            ShuffleValues();
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

        public void ShuffleValues()
        {
            int[] shuffledArray = MyExtensions.ShuffledArray();
            int count = allGraphItems.Count;
            for (int i = 0; i < count; i++)
            {
                allGraphItems[i]._itemValue.Content = shuffledArray[i];
            }
        }

        public void ReverseValues()
        {
            int[] array = GetArrayFromGraph();
            Array.Reverse(array);
            int count = allGraphItems.Count;
            for (int i = 0; i < count; i++)
            {
                allGraphItems[i]._itemValue.Content = array[i];
            }
        }

        public void UpdateAllBarHeights()
        {
            foreach (GraphItem x in allGraphItems)
            {
                x._bar.Height = (int)x._itemValue.Content * (maxBarHeight / allGraphItems.Count);
            }
        }

        public void ResetAllBarColors()
        {
            foreach (GraphItem x in allGraphItems)
            {
                x.SetAllColors((Color)Application.Current.FindResource("UnvisitedItemColor"));
            }
        }

        private void UpdateValues(int[] array)
        {
            int count = allGraphItems.Count;
            for (int i = 0; i < count; i++)
            {
                allGraphItems[i]._itemValue.Content = array[i];
            }
        }

        private int[] GetArrayFromGraph()
        {
            int length = allGraphItems.Count;
            int[] array = new int[length];

            for (int i = 0; i < length; i++)
            {
                array[i] = (int)allGraphItems[i]._itemValue.Content;
            }

            return array;
        }

        private void btnShuffle_Click(object sender, RoutedEventArgs e)
        {
            ShuffleValues();
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
            Sorts newSort = new Sorts();
            switch (cbSelectSortType.SelectedValue)
            {
                case SortType.SelectionSort:
                    {
                        SwitchTaskFactory(GraphicalSelectionSort);
                        break;
                    }
                case SortType.InsertionSort:
                    {
                        SwitchTaskFactory(GraphicalInsertionSort);
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
                    });

                    selectedSort();

                    Dispatcher.Invoke(() =>
                    {
                        btnRun.IsEnabled = true;
                        btnShuffle.IsEnabled = true;
                    });
                });
            }
        }

        //SORTS WITH SPECIAL STEPS
        #region Sorts

        public void GraphicalInsertionSort()
        {
            int length = allGraphItems.Count;

            for (int i = 0; i < length; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    int jValue = 50; //Dummy value
                    int kValue = 50; //Dummy value
                    int k = j - 1;

                    Dispatcher.Invoke(() =>
                    {
                        allGraphItems[j].SetAllColors((Color)Application.Current.FindResource("ActiveItemColor1"));
                        allGraphItems[k].SetAllColors((Color)Application.Current.FindResource("ActiveItemColor2"));

                        jValue = (int)allGraphItems[j]._itemValue.Content;
                        kValue = (int)allGraphItems[k]._itemValue.Content;
                    });

                    Thread.Sleep(waitShort * speed);

                    if (jValue < kValue)
                    {
                        Thread.Sleep(waitShort * speed);

                        Dispatcher.Invoke(() =>
                        {
                            allGraphItems[j]._itemValue.Content = kValue;
                            allGraphItems[j].SetAllColors((Color)Application.Current.FindResource("ActiveItemColor2"));
                            allGraphItems[j].UpdateBarHeight();

                            allGraphItems[k]._itemValue.Content = jValue;
                            allGraphItems[k].SetAllColors((Color)Application.Current.FindResource("ActiveItemColor1"));
                            allGraphItems[k].UpdateBarHeight();


                        });

                        Thread.Sleep(waitLong * speed);

                        Dispatcher.Invoke(() =>
                        {
                            if ((int)allGraphItems[k]._itemValue.Content == k)
                            {
                                //If array[k] = k, color sorted
                                allGraphItems[k].SetAllColors((Color)Application.Current.FindResource("SortedItemColor"));
                            }
                            else
                            {
                                //Set k to unsorted instead of active.
                                allGraphItems[k].SetAllColors((Color)Application.Current.FindResource("UnsortedItemColor"));
                            }

                            if ((int)allGraphItems[j]._itemValue.Content == j)
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
                    else
                    {
                        Dispatcher.Invoke(() =>
                        {
                            if ((int)allGraphItems[k]._itemValue.Content == k)
                            {
                                //If array[k] = k, color sorted
                                allGraphItems[k].SetAllColors((Color)Application.Current.FindResource("SortedItemColor"));
                            }
                            else
                            {
                                //Set k to unsorted instead of active.
                                allGraphItems[k].SetAllColors((Color)Application.Current.FindResource("UnsortedItemColor"));
                            }

                            if ((int)allGraphItems[j]._itemValue.Content == j)
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
                        break;
                    }
                }
            }
        }

        public void GraphicalSelectionSort()
        {
            int length = allGraphItems.Count;

            for (int i = 0; i < length; i++)
            {
                int minimum = i;
                for (int j = i + 1; j < length; j++)
                {
                    int iValue = 50; //Dummy value
                    int jValue = 50; //Dummy value

                    Dispatcher.Invoke(() =>
                    {
                        allGraphItems[i].SetAllColors((Color)Application.Current.FindResource("ActiveItemColor1"));
                        allGraphItems[j].SetAllColors((Color)Application.Current.FindResource("ActiveItemColor2"));

                        iValue = (int)allGraphItems[i]._itemValue.Content;
                        jValue = (int)allGraphItems[j]._itemValue.Content;
                    });

                    Thread.Sleep(waitShort * speed);

                    if (jValue < iValue)
                    {
                        Thread.Sleep(waitShort * speed);
                        minimum = j;

                        Dispatcher.Invoke(() =>
                        {
                            allGraphItems[i]._itemValue.Content = jValue;
                            allGraphItems[i].SetAllColors((Color)Application.Current.FindResource("ActiveItemColor2"));
                            allGraphItems[i].UpdateBarHeight();

                            allGraphItems[j]._itemValue.Content = iValue;
                            allGraphItems[j].SetAllColors((Color)Application.Current.FindResource("ActiveItemColor1"));
                            allGraphItems[j].UpdateBarHeight();
                        });

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
                    if ((int)allGraphItems[i]._itemValue.Content == i)
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
            int length = allGraphItems.Count;
            int step = 1;
            //List<int> indexesInSet = new List<int>();
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
                    for (int j = i; j >= step && jIsLess(j); j -= step)
                    {
                        indexesInSet.Add(j - step);

                        //If not in order swap colors and values
                        Dispatcher.Invoke(() =>
                        {
                            int temp = (int)allGraphItems[j - step]._itemValue.Content;

                            allGraphItems[j - step]._itemValue.Content = allGraphItems[j]._itemValue.Content;
                            allGraphItems[j - step].SetAllColors((Color)Application.Current.FindResource("ActiveItemColor2"));
                            allGraphItems[j - step].UpdateBarHeight();

                            allGraphItems[j]._itemValue.Content = temp;
                            allGraphItems[j].SetAllColors((Color)Application.Current.FindResource("ActiveItemColor1"));
                            allGraphItems[j].UpdateBarHeight();
                        });
                        Thread.Sleep(waitLong * speed);
                    }

                    //Reset colors for all examined indexes
                    Dispatcher.Invoke(() =>
                    {
                        foreach (int x in indexesInSet)
                        {
                            if ((int)allGraphItems[x]._itemValue.Content == x)
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

            bool jIsLess(int j)
            {
                int iValue = 50; //Dummy value
                int jValue = 50; //Dummy value

                Dispatcher.Invoke(() =>
                {
                    iValue = (int)allGraphItems[j - step]._itemValue.Content;
                    jValue = (int)allGraphItems[j]._itemValue.Content;
                });

                return jValue < iValue;
            }
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

    static class MyExtensions
    {
        public static int[] ShuffledArray()
        {
            int[] newArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
            int n = 20;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);

                int x = newArray[k];
                newArray[k] = newArray[n];
                newArray[n] = x;
            }

            return newArray;
        }
    }
}
