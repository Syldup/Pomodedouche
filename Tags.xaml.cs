using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Pomodedouche
{
    /// <summary>
    /// Logique d'interaction pour Tags.xaml
    /// </summary>
    public partial class Tags : UserControl
    {
        public List<Tag> tags { get; private set; }

        public Tags()
        {
            InitializeComponent();
            tags = new List<Tag>();
        }

        public void addTag(String name, String color)
        {
            tags.Add(new Tag(name, color));
            DataContext = new ObservableCollection<Tag>(tags);
        }


        public void clear()
        {
            tags = new List<Tag>();
            DataContext = new ObservableCollection<Tag>(tags);
        }
    }
}
