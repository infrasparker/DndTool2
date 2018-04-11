﻿using DnDTool2.Model;
using DnDTool2.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DnDTool2.View
{
    /// <summary>
    /// Interaction logic for AddCreaturePage.xaml
    /// </summary>
    public partial class AddCreaturePage : Page
    {
        public AddCreaturePage(ObservableCollection<Creature> creatures)
        {
            InitializeComponent();
            this.DataContext = new AddCreaturePageVM(creatures);
        }
    }
}
