using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Pomodedouche.Controleur
{
    /// <summary>
    /// Logique d'interaction pour Pomodoros.xaml
    /// </summary>
    public partial class Pomodoros : UserControl
    {
        public List<Pomodoro> List { get; private set; }

        public Pomodoros()
        {
            List = new List<Pomodoro>();
            InitializeComponent();
        }

        public void addPomodoro(Pomodoro pomo)
        {
            List.Add(pomo);
            if (List.Count % 4 == 0)
                pomo.Long_pause = true;
            root.DataContext = new ObservableCollection<Pomodoro>(List);
        }


        public void clear()
        {
            List = new List<Pomodoro>();
            root.DataContext = new ObservableCollection<Pomodoro>(List);
        }
        public string Cls => "Pomodoros";
    }
}
