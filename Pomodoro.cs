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

        public Pomodoro(IContainer container, String name)
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
    }
}
