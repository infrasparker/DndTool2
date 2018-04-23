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
    public class SpellTomeVM
    {
        private ObservableCollection<Spell> spells;

        public ObservableCollection<Spell> Spells { get => spells; set => spells = value; }

        public RelayCommand OpenAddSpellCommand { get; set; }

        public SpellTomeVM()
        {
            Spells = new ObservableCollection<Spell>
            {
                
            };
            OpenAddSpellCommand = new RelayCommand(OpenAddCreature);
        }

        private void OpenAddCreature(object parameter)
        {
            (new AddSpellWindow(spells)).Show();
        }
    }
}
