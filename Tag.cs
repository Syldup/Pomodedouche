using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Pomodedouche
{

    class Tag
    {
        private static int count = 0;
        private String name;
        private String color;

        public Tag(String name, String color)
        {
            this.name = name;
            this.color = color;
            //container.Add();
        }

        public string Name => this.name;
        public string Color => "#" + this.color;
    }
}
