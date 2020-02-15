using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Pomodedouche.Controleur
{
    /// <summary>
    /// Logique d'interaction pour Pomodoro.xaml
    /// </summary>
    public partial class Pomodoro : UserControl
    {
        private String name;
        public List<Tag> Tags { get; private set; }

        public Pomodoro(String name = "Pomodoro n°x")
        {
            this.name = name;
            this.Tags = new List<Tag>();
            InitializeComponent();
            root.DataContext = this;
        }

        public void setTags(List<Tag> tags)
        {
            this.Tags = tags;
        }

        public void addTag(Tag tag)
        {
            this.Tags.Add(tag);
        }

        public string getSubName()
        {
            if (name.Length > 23)
            {
                return (name.Substring(0, 19) + "...");
            }
            return name;
        }

        public string SubName => getSubName();
        public string Cls => "Pomodoro";
    }
}
