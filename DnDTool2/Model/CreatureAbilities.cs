using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDTool2.Model
{
    public abstract class InfoClass
    {
        private string name;
        private string description;

        public InfoClass(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public string GetName()
        {
            return name;
        }
        public string GetDescription()
        {
            return description;
        }
        public virtual string GetText()
        {
            return name + ". " + description;
        }
    }
}
