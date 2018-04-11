using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDTool2.Model
{
    public class Die
    {
        private int faces;
        private double average;

        public int Faces { get { return this.faces; } set { this.faces = value; } }
        public double Average { get { return this.average; } set { this.average =value; } }

        public Die(int faces)
        {
            this.faces = faces;
            this.average = 0;
            for (int i = 1; i <= this.faces; i++)
            {
                this.average += i / (double)this.faces;
            }
            this.average = Math.Round(this.average * 2) / 2;
        }

        public int Roll()
        {
            return new Random().Next(1, faces + 1);
        }

        public override string ToString()
        {
            return "d" + this.faces;
        }
    }
}
