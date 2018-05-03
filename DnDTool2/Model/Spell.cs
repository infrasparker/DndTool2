using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDTool2.Model
{
    public enum School
    {
        ABJURATION, CONJURATION, DIVINATION, ENCHANTMENT, EVOCATION, ILLUSION, NECROMANCY, TRANSMUTATION
    }
    public enum CastTimeType
    {
        ACTION, BONUS, REACTION, MINUTES, HOURS
    }
    public enum RangeType
    {
        SELF, TOUCH, SIGHT, FEET, MILES
    }
    public enum DurationType
    {
        INSTANTANEOUS, ROUNDS, MINUTES, HOURS, DAYS
    }

    public class Spell : ObservableClass
    {
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private int level;
        public int Level
        {
            get => level;
            set
            {
                level = value;
                OnPropertyChanged("Level");
            }
        }

        private School school;
        public School School
        {
            get => school;
            set
            {
                school = value;
                OnPropertyChanged("School");
            }
        }


        private int castTime;
        public int CastTime
        {
            get => castTime;
            set
            {
                castTime = value;
                OnPropertyChanged("CastTime");
            }
        }
        private CastTimeType castTimeType;
        public CastTimeType CastTimeType
        {
            get => castTimeType;
            set
            {
                castTimeType = value;
                OnPropertyChanged("CastTimeType");
            }
        }

        private int range;
        public int Range
        {
            get => range;
            set
            {
                range = value;
                OnPropertyChanged("Range");
            }
        }

        private RangeType rangeType;
        public RangeType RangeType
        {
            get => rangeType;
            set
            {
                rangeType = value;
                OnPropertyChanged("RangeType");
            }
        }
        private string trigger;
        private string Trigger
        {
            get => trigger;
            set
            {
                trigger = value;
                OnPropertyChanged("Trigger");
            }
        }

        private bool verbal, somatic, material;
        public bool Verbal
        {
            get => verbal;
            set
            {
                verbal = value;
                OnPropertyChanged("Verbal");
            }
        }
        public bool Somatic
        {
            get => somatic;
            set
            {
                somatic = value;
                OnPropertyChanged("Somatic");
            }
        }
        public bool Material
        {
            get => material;
            set
            {
                material = value;
                OnPropertyChanged("Material");
            }
        }

        private string materials;
        public string Materials
        {
            get => materials;
            set
            {
                materials = value;
                OnPropertyChanged("Materials");
            }
        }

        private bool concentration;
        public bool Concentration
        {
            get => concentration;
            set
            {
                concentration = value;
                OnPropertyChanged("Concentration");
            }
        }

        private int duration; // 0 is instantaneous, -1 is until dispelled
        public int Duration
        {
            get => duration;
            set
            {
                duration = value;
                OnPropertyChanged("Duration");
            }
        }

        private DurationType durationType;
        public DurationType DurationType
        {
            get => durationType;
            set
            {
                durationType = value;
                OnPropertyChanged("DurationType");
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public Spell(string name, int level, School school,
                    int castTime, CastTimeType castTimeType, string trigger, int range, RangeType rangeType,
                    bool verbal, bool somatic, bool material, string materials,
                    bool concentration, int duration, DurationType durationType, string description)
        {
            this.name = name;
            this.level = level;
            this.school = school;
            this.castTime = castTime;
            this.trigger = trigger;
            this.range = range;
            this.rangeType = rangeType;
            this.verbal = verbal;
            this.somatic = somatic;
            this.material = material;
            this.materials = materials;
            this.concentration = concentration;
            this.duration = duration;
            this.durationType = durationType;
            this.description = description;
        }

        public Spell(string name, int level) : this(name, level, School.ABJURATION,
                    1, CastTimeType.ACTION, "", 0, RangeType.SELF,
                    true, true, false, "",
                    false, 0, DurationType.INSTANTANEOUS, "")
        { }

        public Spell() : this("", 0) { }

        public static List<Spell> GetSpells()
        {
            List<Spell> spells = new List<Spell>();
            string[] files = FileIO.GetFiles("\\archive\\spells");
            foreach (string s in files)
            {
                spells.Add(FileIO.LoadJson<Spell>(s));
            }
            return spells;
        }

        public static int[] GetStandardSlots(int level)
        {
            switch (level)
            {
                case 1:
                    return new int[9] { 2, 0, 0, 0, 0, 0, 0, 0, 0 };
                case 2:
                    return new int[9] { 3, 0, 0, 0, 0, 0, 0, 0, 0 };
                case 3:
                    return new int[9] { 4, 2, 0, 0, 0, 0, 0, 0, 0 };
                case 4:
                    return new int[9] { 4, 3, 0, 0, 0, 0, 0, 0, 0 };
                case 5:
                    return new int[9] { 4, 3, 2, 0, 0, 0, 0, 0, 0 };
                case 6:
                    return new int[9] { 4, 3, 3, 0, 0, 0, 0, 0, 0 };
                case 7:
                    return new int[9] { 4, 3, 3, 1, 0, 0, 0, 0, 0 };
                case 8:
                    return new int[9] { 4, 3, 3, 2, 0, 0, 0, 0, 0 };
                case 9:
                    return new int[9] { 4, 3, 3, 3, 1, 0, 0, 0, 0 };
                case 10:
                    return new int[9] { 4, 3, 3, 3, 2, 0, 0, 0, 0 };
                case 11:
                    return new int[9] { 4, 3, 3, 3, 2, 1, 0, 0, 0 };
                case 12:
                    return new int[9] { 4, 3, 3, 3, 2, 1, 0, 0, 0 };
                case 13:
                    return new int[9] { 4, 3, 3, 3, 2, 1, 1, 0, 0 };
                case 14:
                    return new int[9] { 4, 3, 3, 3, 2, 1, 1, 0, 0 };
                case 15:
                    return new int[9] { 4, 3, 3, 3, 2, 1, 1, 1, 0 };
                case 16:
                    return new int[9] { 4, 3, 3, 3, 2, 1, 1, 1, 0 };
                case 17:
                    return new int[9] { 4, 3, 3, 3, 2, 1, 1, 1, 1 };
                case 18:
                    return new int[9] { 4, 3, 3, 3, 3, 1, 1, 1, 1 };
                case 19:
                    return new int[9] { 4, 3, 3, 3, 3, 2, 1, 1, 1 };
                case 20:
                    return new int[9] { 4, 3, 3, 3, 3, 2, 2, 1, 1 };
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
