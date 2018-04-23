using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDTool2.Model
{
    public abstract class Item : ObservableClass
    {
        protected string name;
        protected int cost; // In copper pieces
        protected double weight;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public int Cost
        {
            get => cost;
            set
            {
                cost = value;
                OnPropertyChanged("Cost");
            }
        }
        public double Weight {
            get => weight;
            set
            {
                weight = value;
                OnPropertyChanged("Weight");
            }
        }

        public Item(string name, int cost, double weight)
        {
            this.name = name;
            this.cost = cost;
            this.weight = weight;
        }

        public Item(string name) : this(name, 0, 0) { }
    }
}
