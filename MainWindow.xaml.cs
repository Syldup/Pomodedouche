using System;
using System.Windows;
using System.Windows.Threading;
using MySql.Data.MySqlClient;

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
        private int nb_pomo = 0;
        private int cpt_pomo = 0;
        string connString = "SERVER=127.0.0.1; DATABASE=pomodedouche; UID=root; PASSWORD=rootroot";

        public MainWindow()
        {
            InitializeComponent();

            update_lbTimer();
            timer.Interval = TimeSpan.FromMilliseconds(5);
            timer.Tick += timer_Tick;

            Console.WriteLine("Getting Connection ...");
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                Console.WriteLine("Openning Connection ...");
                conn.Open();
                Console.WriteLine("Connection successful!");

            } catch(Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            Console.Read();

        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (left_time <= 0)
            {
                total_time = 60 * 5;

                if (pause || nb_pomo % 4 == 0)
                {
                    nb_pomo += 1;
                    total_time *= 5;
                }
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
            if (tbTag.Text.Trim() != "")
            {
                tmpTags.addTag(new Controleur.Tag(tbTag.Text.Trim(), "FF5DEA84"));
                tbTag.Text = "";
            }
        }

        private void Button_Add_Pomodoro(object sender, RoutedEventArgs e)
        {
            if (tbPomoName.Text.Trim() == "")
            {
                cpt_pomo += 1;
                tbPomoName.Text = "Pomodoro n°" + cpt_pomo;
            }
            Controleur.Pomodoro pomo = new Controleur.Pomodoro(tbPomoName.Text.Trim());
            tbPomoName.Text = "";

            pomo.setTags(tmpTags.List);
            listPomos.addPomodoro(pomo);
            tmpTags.clear();
        }
        public string Cls => "MainWindow";
    }
}
