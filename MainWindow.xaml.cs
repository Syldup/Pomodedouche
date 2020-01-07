using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;

namespace Pomodedouche
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private int total_time = 60 * 25;
        private int left_time = 60 * 25;
        private bool timer_start = false;
        private bool pause = false;

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
                if (pause)
                    total_time = 60 * 5;
                else
                    total_time = 60 * 25;
                left_time = total_time;
                pause = !pause;

            } else {
                left_time--;
            }
            update_lbTimer();
        }

        void update_lbTimer()
        {
            String sec = (left_time % 60).ToString().PadLeft(2, '0');
            lbTime.Content = $"{left_time / 60}:{sec}";
        }

        private void Button_Play(object sender, RoutedEventArgs e)
        {
            if (timer_start)
            {
                timer.Stop();

            } else {
                timer.Start();
            }
            timer_start = !timer_start;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void append_xml()
        {
            string id = "";
            string name = "";
            string age = "";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            //Create a root element
            XmlNode rootNode = xmlDoc.CreateElement("Users");
            //Create a sub element
            XmlNode subNode = xmlDoc.CreateElement("Usesr");
            //Create subNode's attribute
            XmlAttribute idAttribute = xmlDoc.CreateAttribute("Id");
            idAttribute.Value = id;

            XmlAttribute nameAttribute = xmlDoc.CreateAttribute("Name");
            nameAttribute.Value = name;

            XmlAttribute ageAttribute = xmlDoc.CreateAttribute("Age");
            ageAttribute.Value = age;

            subNode.Attributes.Append(idAttribute);
            subNode.Attributes.Append(nameAttribute);
            subNode.Attributes.Append(ageAttribute);

            rootNode.AppendChild(subNode);
            xmlDoc.AppendChild(rootNode);

            xmlDoc.Save("D:/User.xml");
        }
        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            lbTime.Content = "#" + ClrPcker_Background.SelectedColor.R.ToString() + ClrPcker_Background.SelectedColor.G.ToString() + ClrPcker_Background.SelectedColor.B.ToString();
        }
    }
}
