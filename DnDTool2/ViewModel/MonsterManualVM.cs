using DnDTool2.Model;
using DnDTool2.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DnDTool2.ViewModel
{
    public class MonsterManualVM : ViewModel
    {
        private ObservableCollection<Creature> creatures;

        public ObservableCollection<Creature> Creatures { get => creatures; set => creatures = value; }

        public RelayCommand OpenAddCreatureCommand { get; set; }

        public MonsterManualVM(Window window) : base(window)
        {
            Creatures = new ObservableCollection<Creature>
            {
                new Creature("Dragon", 10, 10, 10, 10, 10, 10)
            };
            OpenAddCreatureCommand = new RelayCommand(OpenAddCreature);
        }

        private void OpenAddCreature(object parameter)
        {
            (new AddCreatureWindow(creatures)).Show();
        }
    }
}
