using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Pomodedouche
{
    class Pomodoro
    {
        private String name;
        private List<Tag> tags;

        public Pomodoro(String name)
        {
            this.name = name;
            this.tags = new List<Tag>();
            //container.Add();
        }

        public void setTags(List<Tag> tags)
        {
            this.tags = tags;
        }

        public void addTag(Tag tag)
        {
            this.tags.Add(tag);
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
        public List<Tag> Tags => this.tags;
    }
}
