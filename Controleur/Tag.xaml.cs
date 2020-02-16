using System;
using System.Windows.Controls;

namespace Pomodedouche.Controleur
{
    /// <summary>
    /// Logique d'interaction pour Tag.xaml
    /// </summary>
    public partial class Tag : UserControl
    {
        private String name;
        private String color;

        public Tag()
        {
            this.name = "";
            this.color = "FF5DEA84";
            InitializeComponent();
            root.DataContext = this;
        }

        public Tag(String name, String color)
        {
            this.name = name;
            this.color = color;
            InitializeComponent();
            root.DataContext = this;
        }

        public string getSubName()
        {
            if (name.Length > 15)
            {
                return (name.Substring(0, 12) + "...");
            }
            return name;
        }

        public string Cls => "Tag";
        public string SubName => getSubName();
        public string Color => "#" + color;
    }
}
