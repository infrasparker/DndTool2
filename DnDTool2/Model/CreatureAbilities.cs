using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDTool2.Model
{
    public class InfoClass : ObservableClass
    {
        protected string creatureName;
        protected string description;
        public virtual string Name { get; set; }
        public virtual string Description
        {
            get => description.Replace("[creature]", creatureName);
            set => description = value;
        }

        public InfoClass(string name, string description, string creatureName)
        {
            this.Name = name;
            this.description = description;
            this.creatureName = creatureName;
        }

        public InfoClass(string name, string creatureName) : this(name, "", creatureName) { }

        public virtual string GetText()
        {
            return Name + ". " + Description;
        }
    }

    public class LegendaryResistance : InfoClass
    {
        private int amt;
        public override string Name
        {
            get => "Legendary Resistance (" + amt + "/Day)";
        }

        public override string Description
        {
            get => "If the " + creatureName + " fails a saving throw, it can choose to succeed instead.";
        }

        public LegendaryResistance(int amt, string creatureName) :
            base("Legendary Resistance", creatureName)
        {
            this.amt = amt;
        }
    }

    public class InnateSpellcasting : InfoClass
    {
        private string ability;
        private int bonus;
        private Dictionary<string, List<Spell>> spells;

        public InnateSpellcasting(string ability, int bonus, List<Spell> atWill, Dictionary<string, List<Spell>> spells, string creatureName) :
            base("Innate Spellcasting", creatureName)
        {
            this.ability = ability;
            this.bonus = bonus;
            this.spells = spells;
        }

        public override string Description
        {
            get
            {
                string s = description.Replace("[creature]", creatureName).Replace("[ability]", ability).Replace("[save]", (8 + bonus).ToString()).Replace("[attack]", bonus.ToString()) + "\n\n";
                
                foreach (string key in spells.Keys)
                {
                    string n = "";
                    foreach (Spell spell in spells[key])
                    {
                        n += ", " + spell.Name;
                    }
                    s += (n == "" ? "" : (key + ": " + n.Substring(2) + "\n"));
                }
                return s;
            }
        }
    }

    public class Spellcasting : InfoClass
    {
        private string ability;
        private int bonus;
        private int level;
        private List<Spell>[] spells;
        private int[] spellSlots;

        public Spellcasting(string ability, int bonus, int level, List<Spell>[] spells, string creatureName) : 
            base("Spellcasting", creatureName)
        {
            this.ability = ability;
            this.bonus = bonus;
            this.level = level;
            this.spells = spells;

            this.spellSlots = Spell.GetStandardSlots(level);
        }

        public override string Description
        {
            get
            {
                string s = description.Replace("[creature]", creatureName).Replace("[level]", level.ToString()).Replace("[ability]", ability).Replace("[save]",
                    (8 + bonus).ToString()).Replace("[attack]", bonus.ToString()) + "\n\n";
                
                string n = "";
                foreach (Spell spell in spells[0])
                {
                    n += ", " + spell.Name;
                }
                s += (n == "" ? "" : ("Cantrips (at will): " + n.Substring(2) + "\n"));

                for (int i = 0; i < 9; i++)
                {
                    n = "";
                    foreach (Spell spell in spells[i + 1])
                    {
                        n += ", " + spell.Name;
                    }
                    s += (n == "" ? "" : (Language.NumSuffix(i + 1) + " level (" + spellSlots[i + 1] + " slots): " + n.Substring(2) + "\n"));
                }

                return s;
            }
        }
    }
}
