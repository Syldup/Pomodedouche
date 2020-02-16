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
        public readonly long Id;
        private String name;
        private bool long_pause = false;
        public List<Tag> Tags { get; private set; }

        public Pomodoro()
        {
            this.name = "Pomodoro n°x";
            this.Tags = new List<Tag>();
            InitializeComponent();
            root.DataContext = this;
        }

        public Pomodoro(long id, String name)
        {
            this.Id = id;
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

        public string getStrTime()
        {
            return Long_pause ? "25 min" : "5 min";
        }

        public string SubName => getSubName();
        public string StrTime => getStrTime();
        public string Cls => "Pomodoro";

        public bool Long_pause { get => long_pause; set => long_pause = value; }
    }
}
