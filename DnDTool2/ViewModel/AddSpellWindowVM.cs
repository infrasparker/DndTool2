using DnDTool2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDTool2.ViewModel
{
    public class AddSpellWindowVM
    {
        public ObservableCollection<Spell> Spells { get; set; }

        public AddSpellWindowVM(ObservableCollection<Spell> spells)
        {
            this.Spells = spells;
        }
    }
}
