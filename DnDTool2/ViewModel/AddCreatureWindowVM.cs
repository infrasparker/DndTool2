using DnDTool2.Model;
using DnDTool2.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDTool2.ViewModel
{
    public class AddCreatureWindowVM : ViewModel
    {
        public List<CreatureSize> CreatureSizes { get; set; }
        public List<CreatureType> CreatureTypes { get; set; }
        public List<Alignment> Alignments { get; set; }
        public List<Armor> Armors { get; set; }
        public List<ChallengeRating> CRs { get; set; }
        public List<SkillType> Skills { get; set; }
        public List<DamageType> Damages { get; set; }
        public List<ConditionType> Conditions { get; set; }

        private SkillType displayedSkill;
        public SkillType DisplayedSkill { get => displayedSkill; set { displayedSkill = value; OnPropertyChanged("DisplayedSkill"); } }

        private DamageType displayedDamage;
        public DamageType DisplayedDamage { get => displayedDamage; set { displayedDamage = value; OnPropertyChanged("DisplayedDamage"); } }

        private ConditionType displayedCondition;
        public ConditionType DisplayedCondition { get => displayedCondition; set { displayedCondition = value; OnPropertyChanged("DisplayedCondition"); } }

        private string langBox;
        public string LangBox { get => langBox; set { langBox = value; OnPropertyChanged("LangBox"); } }
        public RelayCommand AddLanguageCommand { get; set; }
        public RelayCommand OpenLanguagesCommand { get; set; }

        private string abilityNameBox, abilityDescBox;
        public string AbilityNameBox { get => abilityNameBox; set { abilityNameBox = value; OnPropertyChanged("AbilityNameBox"); } }
        public string AbilityDescBox { get => abilityDescBox; set { abilityDescBox = value; OnPropertyChanged("AbilityDescBox"); } }

        public ObservableCollection<Creature> creatures;

        public Creature Creature { get; set; }

        public AddCreatureWindowVM(ObservableCollection<Creature> creatures)
        {
            this.creatures = creatures;
            CreatureSizes = Enum.GetValues(typeof(CreatureSize)).Cast<CreatureSize>().ToList();
            CreatureTypes = Enum.GetValues(typeof(CreatureType)).Cast<CreatureType>().ToList();
            Alignments = Enum.GetValues(typeof(Alignment)).Cast<Alignment>().ToList();
            Armors = Armor.GetStandardArmors();
            CRs = ChallengeRating.GetStandardCRs();
            Skills = Enum.GetValues(typeof(SkillType)).Cast<SkillType>().ToList();
            DisplayedSkill = SkillType.ATHLETICS;
            Damages = Enum.GetValues(typeof(DamageType)).Cast<DamageType>().ToList();
            DisplayedDamage = DamageType.ACID;
            Conditions = Enum.GetValues(typeof(ConditionType)).Cast<ConditionType>().ToList();
            DisplayedCondition = ConditionType.BLINDED;

            AddLanguageCommand = new RelayCommand(AddLanguage);
            OpenLanguagesCommand = new RelayCommand(OpenLanguages);

            this.Creature = new Creature("Creature Name", 10, 10, 10, 10, 10, 10);
        }

        private void AddLanguage(object parameter)
        {
            if (LangBox != "" && LangBox != null)
            {
                string lang = LangBox.Substring(0, 1).ToUpper() + LangBox.Substring(1).ToLower();
                if (!this.Creature.Languages.Contains(lang))
                {
                    this.Creature.AddLanguage(lang);
                }
                LangBox = "";
            }
        }

        private void OpenLanguages(object parameter)
        {
            (new AddLanguageWindow(Creature)).Show();
        }
    }
}
