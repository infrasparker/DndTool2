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
    public enum CastTime
    {
        ACTION, BONUSACTION, REACTION, MINUTE, HOUR
    }
    public enum RangeType
    {
        SELF, TOUCH, RANGE, SIGHT
    }
    public enum DurationType
    {
        ROUND, MINUTE, HOUR, DAY
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

        private CastTime castTime;
        public CastTime CastTime
        {
            get => castTime;
            set
            {
                castTime = value;
                OnPropertyChanged("CastTime");
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
        public bool Solmatic
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

        public Spell(string name, int level, School school, CastTime castTime, int range, RangeType rangeType,
                    bool verbal, bool somatic, bool material, string materials,
                    bool concentration, int duration, DurationType durationType, string description)
        {
            this.name = name;
            this.level = level;
            this.school = school;
            this.castTime = castTime;
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
    }
}
