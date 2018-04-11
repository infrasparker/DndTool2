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
    public class MonsterManualVM
    {
        private ObservableCollection<Creature> creatures;

        public ObservableCollection<Creature> Creatures { get => creatures; set => creatures = value; }

        public RelayCommand OpenAddCreatureCommand { get; set; }

        public MonsterManualVM()
        {
            Creatures = new ObservableCollection<Creature>
            {
                new Creature("Dragon")
            };
            OpenAddCreatureCommand = new RelayCommand(OpenAddCreature);
        }

        public void OpenAddCreature(object parameter)
        {
            (new AddCreatureWindow(creatures)).Show();
        }
    }
}
