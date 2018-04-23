using DnDTool2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDTool2.ViewModel
{
    public class AddLanguageWindowVM : ViewModel
    {
        public Creature Creature { get; set; }

        private string langEdit;

        private string langBox;
        public string LangBox { get => langBox; set { langBox = value; OnPropertyChanged("LangBox"); } }

        public RelayCommand AddLanguageCommand { get; set; }
        public RelayCommand RemoveLanguageCommand { get; set; }
        public RelayCommand EditLanguageCommand { get; set; }
        public RelayCommand ConfirmEditLanguageCommand { get; set; }

        private bool editing;
        public bool Editing { get => editing; set { editing = value; OnPropertyChanged("Editing"); } }

        public AddLanguageWindowVM(Creature creature)
        {
            this.Creature = creature;
            AddLanguageCommand = new RelayCommand(AddLanguage);
            RemoveLanguageCommand = new RelayCommand(RemoveLanguage);
            EditLanguageCommand = new RelayCommand(EditLanguage);
            ConfirmEditLanguageCommand = new RelayCommand(ConfirmEditLanguage);
            Editing = false;
        }

        private void AddLanguage(object parameter)
        {
            if (LangBox != "" && LangBox != null)
            {
                string lang = LangBox.Substring(0, 1).ToUpper() + LangBox.Substring(1).ToLower();
                if (!this.Creature.Languages.Contains(lang))
                {
                    this.Creature.AddLanguage(lang);
                    this.Creature.SortLanguages();
                }
                LangBox = "";
            }
        }

        private void RemoveLanguage(object parameter)
        {
            if (parameter == null)
                throw new NotSupportedException();
            else
            {
                string lang = (string)parameter;
                Creature.RemoveLanguage(lang);
            }
        }

        private void EditLanguage(object parameter)
        {
            langEdit = (string)parameter;
            LangBox = langEdit;
            this.Editing = true;
        }

        private void ConfirmEditLanguage(object parameter)
        {
            RemoveLanguage(langEdit);
            AddLanguage(null);
            this.Editing = false;
        }
    }
}
