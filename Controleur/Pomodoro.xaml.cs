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

        public Pomodoro()
        {
            this.name = "Pomodoro n°x";
            this.Tags = new List<Tag>();
            InitializeComponent();
            root.DataContext = this;
        }

        public Pomodoro(String name)
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

        public string getName()
        {
            if (this.name.Length > 23)
            {
                return (this.name.Substring(0, 19) + "...");
            }
            return this.name;
        }

        public string Name => getName();
    }
}
