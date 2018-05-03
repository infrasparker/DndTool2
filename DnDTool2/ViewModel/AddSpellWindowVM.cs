using DnDTool2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DnDTool2.ViewModel
{
    public class AddSpellWindowVM : ViewModel
    {
        public SpellList SpellList { get; set; }

        public List<School> Schools { get; set; }

        public List<CastTimeType> CastingTimes { get; set; }
        private CastTimeType castingTimeType;
        public CastTimeType CastingTimeType { get => castingTimeType; set { castingTimeType = value; OnPropertyChanged("CastingTimeType"); } }
        
        public List<RangeType> Ranges { get; set; }
        private RangeType rangeType;
        public RangeType RangeType { get => rangeType; set { rangeType = value; OnPropertyChanged("RangeType"); } }

        public List<DurationType> Durations { get; set; }
        private DurationType durationType;
        public DurationType DurationType { get => durationType; set { durationType = value; OnPropertyChanged("DurationType"); } }

        public Spell Spell { get; set; }

        public RelayCommand CreateSpellCommand { get; set; }

        public AddSpellWindowVM(Window window, SpellList SpellList, Spell spell) : base(window)
        {
            this.window = window;
            this.SpellList = SpellList;
            this.Spell = spell;
            Schools = Enum.GetValues(typeof(School)).Cast<School>().ToList();
            CastingTimes = Enum.GetValues(typeof(CastTimeType)).Cast<CastTimeType>().ToList();
            Ranges = Enum.GetValues(typeof(RangeType)).Cast<RangeType>().ToList();
            Durations = Enum.GetValues(typeof(DurationType)).Cast<DurationType>().ToList();
            CreateSpellCommand = new RelayCommand(CreateSpell);
        }

        private void CreateSpell(object parameter)
        {
            if (!SpellList.Contains(Spell))
            {
                SpellList.Add(Spell);
                FileIO.WriteJson("\\archive\\spells", Spell.Name, Spell);
                window.Close();
            }
                
        }
    }
}
