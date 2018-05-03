using DnDTool2.Model;
using DnDTool2.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DnDTool2.ViewModel
{
    public class SpellTomeVM : ViewModel
    {
        public SpellList SpellList { get; set; }

        public RelayCommand OpenAddSpellCommand { get; set; }
        public RelayCommand RemoveSpellCommand { get; set; }
        public RelayCommand EditSpellCommand { get; set; }

        public SpellTomeVM(Window window) : base(window)
        {
            SpellList = new SpellList(Spell.GetSpells());
            OpenAddSpellCommand = new RelayCommand(OpenAddSpell);
            RemoveSpellCommand = new RelayCommand(RemoveSpell);
            EditSpellCommand = new RelayCommand(EditSpell);
        }

        private void OpenAddSpell(object parameter)
        {
            if (parameter == null)
                (new AddSpellWindow(SpellList)).Show();
            else
            {
                AddSpellWindow window = new AddSpellWindow(SpellList, (Spell)parameter);
                window.Show();
                window.Focus();
            }
        }

        private void RemoveSpell(object parameter)
        {
            if (parameter == null)
                throw new NotSupportedException();
            else
            {
                Spell spell = (Spell)parameter;
                SpellList.Remove(spell);
                FileIO.Delete("\\archive\\spells", spell.Name + ".json");
            }
        }

        private void EditSpell(object parameter)
        {
            RemoveSpell(parameter);
            OpenAddSpell(parameter);
        }
    }

    public class SpellList : ObservableClass
    {
        private ObservableCollection<Spell> spells;
        public ObservableCollection<Spell> Spells
        {
            get => spells;
            set
            {
                spells = value;
                OnPropertyChanged("Spells");
            }
        }

        public SpellList(List<Spell> spells)
        {
            this.spells = new ObservableCollection<Spell>(spells);
            SortSpells();
        }

        public void SortSpells()
        {
            spells = new ObservableCollection<Spell>(spells.OrderBy(s => s.Level).ThenBy(s => s.Name));
            OnPropertyChanged("Spells");
        }

        public void Add(Spell spell)
        {
            this.spells.Add(spell);
            SortSpells();
            OnPropertyChanged("Spells");
        }

        public void Remove(Spell spell)
        {
            this.spells.Remove(spell);
            OnPropertyChanged("Spells");
        }

        public bool Contains(Spell spell)
        {
            foreach (Spell s in spells)
            {
                if (s.Name.Equals(spell.Name))
                    return true;
            }
            return false;
        }
    }
}
