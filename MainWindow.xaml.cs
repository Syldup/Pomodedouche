using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
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
        private int left_time = 60 * 25;
        private bool timer_start = false;
        private bool pause = false;
        private List<Pomodoro> pomodoros;
        private List<Tag> tags;

        public MainWindow()
        {
            InitializeComponent();
            this.pomodoros = new List<Pomodoro>();
            this.tags = new List<Tag>();

            var window = new Window();

            update_lbTimer();
            timer.Interval = TimeSpan.FromMilliseconds(5);
            timer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (left_time <= 0)
            {
                if (pause)
                    total_time = 60 * 5;
                else
                    total_time = 60 * 25;
                left_time = total_time;
                pause = !pause;

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
            String name = tbTag.Text;
            String color = "FF5DEA84";
            this.tags.Add(new Tag(name, color));
            this.icTags.DataContext = new ObservableCollection<Tag>(this.tags);
        }

        private void Button_Add_Pomodoro(object sender, RoutedEventArgs e)
        {
            String name = tbPomoName.Text;
            Pomodoro pomo = new Pomodoro(name);

            pomo.setTags(tags);
            tags = new List<Tag>();

            this.pomodoros.Add(pomo);
            this.icTags.DataContext = new ObservableCollection<Tag>(this.tags);
            this.icPomos.DataContext = new ObservableCollection<Pomodoro>(this.pomodoros);
        }
    }
}
