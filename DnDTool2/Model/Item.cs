using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDTool2.Model
{
    public abstract class Item
    {
        private string name;
        private int cost; // In copper pieces
        private double weight;

        public string Name { get; set; }
        public int Cost { get; set; }
        public double Weight { get; set; }

        public Item(string name, int cost, double weight)
        {
            this.Name = name;
            this.Cost = cost;
            this.Weight = weight;
        }

        public Item(string name) : this(name, 0, 0) { }
    }
}
