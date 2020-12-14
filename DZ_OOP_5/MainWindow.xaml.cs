using System;
using System.Timers;
using System.Windows;


namespace DZ_OOP_5
{
    public partial class MainWindow : Window
    {
        class Timer
        {
            private int hour;
            private int minute;
            private int second;

            public int Hour
            {
                get
                {
                    return hour;
                }
                set
                {
                    if (value >= 0 && value <= 23)
                    {
                        hour = value;
                    }
                    else
                    {
                        throw new Exception("Часы должны принадлежать диапазону от 0 до 23");
                    }
                }
            }
            public int Minute
            {
                get
                {
                    return minute;
                }
                set
                {
                    if (value >= 0 && value <= 59)
                    {
                        minute = value;
                    }
                    else
                    {
                        throw new Exception("Минуты должны принадлежать диапазону от 0 до 59");
                    }
                }
            }
            public int Second
            {
                get
                {
                    return second;
                }
                set
                {
                    if (value >= 0 && value <= 59)
                    {
                        second = value;
                    }
                    else
                    {
                        throw new Exception("Секунды должны принадлежать диапазону от 0 до 59");
                    }
                }
            }
            public Timer(int h, int m, int s)
            {
                hour = h;
                minute = m;
                second = s;
            }

            public override string ToString()
            {
                string time = "";
                if (hour < 10)
                {
                    time += "0" + hour;
                }
                else
                {
                    time += hour;
                }

                time += ":";

                if (minute < 10)
                {
                    time += "0" + minute;
                }
                else
                {
                    time += minute;
                }

                time += ":";

                if (second < 10)
                {
                    time += "0" + second;
                }
                else
                {
                    time += second;
                }
                return time;
            }

            public void IncreaseTime(int h, int m, int s)
            {
                if (Second + s > 59)
                {
                    Second = (Second + s) % 59 - 1;
                    Minute++;
                }
                else
                {
                    Second += s;
                }

                if (Minute + m > 59)
                {
                    Minute = (Minute + m) % 59 - 1;
                    Hour++;
                }
                else
                {
                    Minute += m;
                }

                if (Hour + h > 23)
                {
                    Hour = (Hour + h) % 23 - 1;
                }
                else
                {
                    Hour += h;
                }
            }
            public void DecreaseTime(int h, int m, int s)
            {
                if (Second - s < 0)
                {
                    Second = 59 - (s - Second - 1);
                    Minute--;
                }
                else
                {
                    Second -= s;
                }

                if (Minute - m < 0)
                {
                    Minute = 59 - (m - Minute - 1);
                    Hour--;
                }
                else
                {
                    Minute -= m;
                }

                if (Hour - h < 0)
                {
                    Hour = 23 - (h - Hour - 1);
                }
                else
                {
                    Hour -= h;
                }
            }
        }

        Timer timer = new Timer(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

        public MainWindow()
        {
            InitializeComponent();
            TimeLb.Content = timer.ToString();
        }
        public void UpdateTime()
        {
            TimeLb.Content = timer.ToString();
        }
        public void CheckData(int h, int m, int s)
        {
            if (h < 0 || m < 0 || s < 0)
            {
                throw new Exception("Значение числа, на которое нужно изменить время должно быть положительным");
            }
        }

        private void ConfirmInputBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                timer.Hour = int.Parse(InputHourTB.Text);
                timer.Minute = int.Parse(InputMinuteTB.Text);
                timer.Second = int.Parse(InputSecondTB.Text);
                UpdateTime();
            }
            catch (FormatException)
            {
                MessageBox.Show("Тип данных не совпадает с предполагаемыми.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Текст:\r\n" + ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ConfirmChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int h = int.Parse(ChangeHourTB.Text);
                int m = int.Parse(ChangeMinuteTB.Text);
                int s = int.Parse(ChangeSecondTB.Text);
                CheckData(h, m, s);
                if (IncreaseRB.IsChecked == true)
                {
                    timer.IncreaseTime(h % 24, m % 60, s % 60);
                }
                else if (DecreaseRB.IsChecked == true)
                {
                    timer.DecreaseTime(h % 24, m % 60, s % 60);
                }
                else
                {
                    throw new Exception("Не был выбран вариант изменения времени.");
                }
                UpdateTime();
            }
            catch (FormatException)
            {
                MessageBox.Show("Тип данных не совпадает с предполагаемыми.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Текст:\r\n" + ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
