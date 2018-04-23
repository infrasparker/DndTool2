﻿using DnDTool2.Model;
using DnDTool2.ViewModel;
using MahApps.Metro.Controls;
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
    /// Interaction logic for AddCreatureWindow.xaml
    /// </summary>
    public partial class AddCreatureWindow : MetroWindow
    {

        public AddCreatureWindow(ObservableCollection<Creature> creatures)
        {
            InitializeComponent();
            this.DataContext = new AddCreatureWindowVM(creatures);
        }
    }
}
