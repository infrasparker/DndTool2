using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDTool2.Model
{
    public enum ArmorType
    {
        LIGHT, MEDIUM, HEAVY, SHIELD, NONE
    }

    public class Armor : Item
    {
        private ArmorType type;
        private int baseAC, strReq;
        private bool stealthDisadv;

        public ArmorType Type { get => type; set => type = value; }
        public int BaseAC { get => baseAC; set => baseAC = value; }
        public int StrReq { get => strReq; set => strReq = value; }
        public bool StealthDisadv { get => stealthDisadv; set => stealthDisadv = value; }

        public Armor(string name, int cost, double weight, ArmorType type, int baseAC, int strReq, bool stealthDisadv) : base(name, cost, weight)
        {
            this.Type = type;
            this.BaseAC = baseAC;
            this.StrReq = strReq;
            this.StealthDisadv = stealthDisadv;
        }

        // No stealth disadvantage
        public Armor(string name, int cost, double weight, ArmorType type, int baseAC, int strReq) : this(name, cost, weight, type, baseAC, strReq, false) { }

        // No strength requirement
        public Armor(string name, int cost, double weight, ArmorType type, int baseAC, bool stealthDisadv) : this(name, cost, weight, type, baseAC, 0, stealthDisadv) { }

        // No strength requirement or stealth disadvantage
        public Armor(string name, int cost, double weight, ArmorType type, int baseAC) : this(name, cost, weight, type, baseAC, 0, false) { }

        // No details including cost and weight, used for generic creation
        public Armor(string name, ArmorType type, int baseAC) : this(name, 0, 0, type, baseAC) { }

        public override string ToString()
        {
            return Name;
        }

        public static List<Armor> GetStandardArmors()
        {
            return new List<Armor>
            {
                new Armor("None", ArmorType.NONE, 10),
                new Armor("Padded", 500, 8, ArmorType.LIGHT, 11, true),
                new Armor("Leather", 1000, 10, ArmorType.LIGHT, 11),
                new Armor("Studded Leather", 4500, 13, ArmorType.LIGHT, 12),
                new Armor("Hide", 1000, 12, ArmorType.MEDIUM, 12),
                new Armor("Chain Shirt", 5000, 20, ArmorType.MEDIUM, 13),
                new Armor("Scale Mail", 5000, 45, ArmorType.MEDIUM, 14, true),
                new Armor("Breastplate", 40000, 20, ArmorType.MEDIUM, 14),
                new Armor("Half Plate", 75000, 40, ArmorType.MEDIUM, 15, true),
                new Armor("Ring Mail", 3000, 40, ArmorType.HEAVY, 14, true),
                new Armor("Chain Mail", 7500, 55, ArmorType.HEAVY, 16, 13, true),
                new Armor("Splint", 20000, 60, ArmorType.HEAVY, 17, 15, true),
                new Armor("Plate", 150000, 65, ArmorType.HEAVY, 18, 15, true)
            };
        }
    }
}
