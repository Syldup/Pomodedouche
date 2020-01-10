using System;
using System.Collections.Generic;
<<<<<<< Updated upstream
using System.Collections.ObjectModel;
=======
using System.IO;
>>>>>>> Stashed changes
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;

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


            /// Create a grid with some text within
            // Grid
            Grid blocPomo1 = new Grid();
            blocPomo1.Width = 201;
            blocPomo1.Height = 87;
            blocPomo1.HorizontalAlignment = HorizontalAlignment.Left;
            blocPomo1.VerticalAlignment = VerticalAlignment.Top;

            // Rows
            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(45);
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = new GridLength(45);
            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = new GridLength(45);
            blocPomo1.RowDefinitions.Add(gridRow1);
            blocPomo1.RowDefinitions.Add(gridRow2);
            blocPomo1.RowDefinitions.Add(gridRow3);

            // Text
            Label labelPomodoroName1 = new Label();
            labelPomodoroName1.Content = "Pomodoro test 1";
            labelPomodoroName1.FontSize = 18;
            labelPomodoroName1.Foreground = new SolidColorBrush(Colors.White);
            labelPomodoroName1.VerticalAlignment = VerticalAlignment.Top;

            Grid.SetRow(labelPomodoroName1, 0);

            blocPomo1.Children.Add(labelPomodoroName1);

            window.Content = blocPomo1;

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
            this.icPomos.DataContext = new ObservableCollection<Pomodoro>(this.pomodoros);
        }
    }
}
