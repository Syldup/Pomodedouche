using System;
using System.Windows;
using System.Windows.Threading;
using MySql.Data.MySqlClient;
using System.Data;


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

        ///  DB test
        string connString = "SERVER=127.0.0.1; DATABASE=pomodedouche; UID=root; PASSWORD=rootroot";
        private MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
        private DataSet ds = new DataSet();

        public MainWindow()
        {
            InitializeComponent();

            update_lbTimer();
            timer.Interval = TimeSpan.FromMilliseconds(5);
            timer.Tick += timer_Tick;
            
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
                // Nouvelle connexion
                MySqlConnection conn = new MySqlConnection(connString);
                // On ouvre la connexion
                conn.Open();
                // Creation de la commande pour sql
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO tag(nom_tag) VALUES(?nom)";
                cmd.Parameters.Add("?nom", MySqlDbType.VarChar).Value = tbTag.Text;
                cmd.ExecuteNonQuery();
                conn.Close();

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

            // Nouvelle connexion
            MySqlConnection conn = new MySqlConnection(connString);
            // On ouvre la connexion
            conn.Open();
            // Creation de la commande pour sql
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO pomodoro(nom) VALUES(?nom)";
            cmd.Parameters.Add("?nom", MySqlDbType.VarChar).Value = tbPomoName.Text;
            cmd.ExecuteNonQuery();
            conn.Close();

            tbPomoName.Text = "";

            pomo.setTags(tmpTags.List);
            listPomos.addPomodoro(pomo);
            tmpTags.clear();
        }
        public string Cls => "MainWindow";
    }
}
