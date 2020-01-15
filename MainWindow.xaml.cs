using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace Pomodedouche
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private int total_time = 60 * 25;
        private int left_time = 0;
        private bool timer_start = false;
        private bool pause = true;
        private List<Pomodoro> pomodoros;

        public MainWindow()
        {
            InitializeComponent();
            this.pomodoros = new List<Pomodoro>();

            var window = new Window();

            update_lbTimer();
            timer.Interval = TimeSpan.FromMilliseconds(5);
            timer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (left_time <= 0)
            {
                total_time = 60 * 5;

                if (pause)
                    total_time *= 5;
                pause = !pause;

                left_time = total_time;

            }
            else
            {
                left_time--;
            }
            update_lbTimer();
        }

        void update_lbTimer()
        {
            String sec = (left_time % 60).ToString().PadLeft(2, '0');
            lbTime.Content = $"{left_time / 60}:{sec}";
            TimerBar.Value = left_time * 100 / total_time;
        }

        private void Button_Play(object sender, RoutedEventArgs e)
        {
            if (timer_start)
            {
                timer.Stop();
            }
            else
            {
                timer.Start();
            }
            timer_start = !timer_start;
        }

        private void Button_Add_Tag(object sender, RoutedEventArgs e)
        {
            tmpTag.addTag(tbTag.Text, "FF5DEA84");
            tbTag.Text = "";
        }

        private void Button_Add_Pomodoro(object sender, RoutedEventArgs e)
        {
            String name = tbPomoName.Text;
            tbPomoName.Text = "";
            Pomodoro pomo = new Pomodoro(name);

            pomo.setTags(tmpTag.tags);

            this.pomodoros.Add(pomo);
            tmpTag.clear();
            this.icPomos.DataContext = new ObservableCollection<Pomodoro>(this.pomodoros);
        }
    }
}
