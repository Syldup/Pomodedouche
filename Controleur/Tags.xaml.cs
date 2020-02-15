using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Pomodedouche.Controleur
{
    /// <summary>
    /// Logique d'interaction pour Tags.xaml
    /// </summary>
    public partial class Tags : UserControl
    {
        public List<Tag> List { get; private set; }

        public Tags()
        {
            List = new List<Tag>();
            InitializeComponent();
        }

        public void addTag(Tag tag)
        {
            List.Add(tag);
            root.DataContext = new ObservableCollection<Tag>(List);
        }

        public void clear()
        {
            List = new List<Tag>();
            root.DataContext = new ObservableCollection<Tag>(List);
        }

        public string Cls => "Tags";
    }
}
