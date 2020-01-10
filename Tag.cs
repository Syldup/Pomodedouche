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
        }

        public string getName() {
            if(this.name.Length > 15){
                return (this.name.Substring(0, 12) + "...");
            }
            return this.name;
        }

        public string Name => getName();
        public string Color => "#" + this.color;
    }
}
