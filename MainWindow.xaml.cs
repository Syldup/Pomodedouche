using System;
using System.Windows;
using System.Windows.Threading;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections.Generic;

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

        private string connString = "SERVER=127.0.0.1; DATABASE=pomodedouche; UID=root; PASSWORD=rootroot";
        private List<Controleur.Tag> allTags;

        public MainWindow()
        {
            InitializeComponent();
            bt_addTag.Content = "Ajouter" + Environment.NewLine + "   tag";
            bt_addPomo.Content = "  Ajouter" + Environment.NewLine + "pomodoro";

            allTags = new List<Controleur.Tag>();

            // Nouvelle connexion
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();

            // Get tout les tags
            cmd.CommandText = "SELECT * FROM tag";
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                long tag_id = Convert.ToInt64(rdr[0]);
                string tag_name = (string)rdr[1];
                string tag_color = (string)rdr[2];
                Controleur.Tag tag = new Controleur.Tag(tag_id, tag_name, tag_color);
                allTags.Add(tag);
                cbTags.Items.Add(tag);
            }
            rdr.Close();
            conn.Close();

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
                long tag_id = 0;
                string tag_name = tbTag.Text.Trim();
                string tag_color = "5DEA84";

                // Nouvelle connexion
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();

                // Get le tag si il exist
                cmd.CommandText = "SELECT id, color FROM tag WHERE name=?name";
                cmd.Parameters.AddWithValue("?name", tag_name);
                MySqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    tag_id = Convert.ToInt64(rdr[0]);
                    tag_color = (string)rdr[1];
                    rdr.Close();
                }
                else
                {
                    rdr.Close();
                    // Insetion du tag
                    cmd.CommandText = "INSERT INTO tag (name, color) VALUES (?name, ?color)";
                    cmd.Parameters.AddWithValue("?color", tag_color);
                    cmd.ExecuteNonQuery();

                    tag_id = cmd.LastInsertedId;
                }
                conn.Close();

                tmpTags.addTag(new Controleur.Tag(tag_id, tag_name, tag_color));
            }
            tbTag.Text = "";

        }

        private void Button_Add_Pomodoro(object sender, RoutedEventArgs e)
        {
            string pomo_name = tbPomoName.Text.Trim();
            if (pomo_name == "")
            {
                cpt_pomo += 1;
                pomo_name = "Pomodoro n°" + cpt_pomo;
            }

            // Nouvelle connexion
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();

            // Insetion du pomodoro
            cmd.CommandText = "INSERT INTO pomodoro (name) VALUES (?name)";
            cmd.Parameters.Add("?name", MySqlDbType.VarChar).Value = pomo_name;
            cmd.ExecuteNonQuery();

            long pomo_id = cmd.LastInsertedId;

            // Insetion des tags
            cmd.CommandText = "INSERT INTO pomodoro_tags (id_pomo, id_tag) VALUES (?id_pomo, ?id_tag)";
            cmd.Prepare();
            cmd.Parameters.AddWithValue("?id_pomo", pomo_id);
            cmd.Parameters.AddWithValue("?id_tag", 0);

            tmpTags.List.ForEach(delegate (Controleur.Tag tag)
            {
                cmd.Parameters["?id_tag"].Value = tag.Id;
                cmd.ExecuteNonQuery();
            });
            conn.Close();

            Controleur.Pomodoro pomo = new Controleur.Pomodoro(pomo_id, pomo_name);
            pomo.setTags(tmpTags.List);
            listPomos.addPomodoro(pomo);

            tmpTags.clear();
            tbPomoName.Text = "";
        }

        public string Cls => "MainWindow";

        private void textColorChange(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            tbColor.Text = Regex.Replace(tbColor.Text, @"[^a-fA-F0-9\-]", "");
            if (tbColor.Text.Length == 6)
            {

            }
        }

        private void Search_Text_Changed(object sender, RoutedEventArgs e)
        {
            string searchText = searchBox.Text;

            // Nouvelle connexion
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();

            // Recherche
            cmd.CommandText = "SELECT id,name FROM pomodoro WHERE name=?name";
            cmd.Parameters.AddWithValue("?name", searchText);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                string searchedPomoName = (string)rdr[1];
                searchLabel.Content = searchedPomoName;
            }
            rdr.Close();

        }
    }
}
