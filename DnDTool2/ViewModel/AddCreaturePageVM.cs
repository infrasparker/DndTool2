using DnDTool2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDTool2.ViewModel
{
    public class AddCreaturePageVM : ViewModel
    {
        public List<CreatureSize> CreatureSizes { get; set; }
        public List<CreatureType> CreatureTypes { get; set; }
        public List<Alignment> Alignments { get; set; }
        public List<Armor> Armors { get; set; }
        public List<ChallengeRating> CRs { get; set; }

        public ObservableCollection<Creature> creatures;

        public Creature Creature { get; set; }

        public AddCreaturePageVM(ObservableCollection<Creature> creatures)
        {
            this.creatures = creatures;
            CreatureSizes = Enum.GetValues(typeof(CreatureSize)).Cast<CreatureSize>().ToList();
            CreatureTypes = Enum.GetValues(typeof(CreatureType)).Cast<CreatureType>().ToList();
            Alignments = Enum.GetValues(typeof(Alignment)).Cast<Alignment>().ToList();
            Armors = Armor.GetStandardArmors();
            CRs = ChallengeRating.GetStandardCRs();

            this.Creature = new Creature("Creature Name");
        }
    }
}
