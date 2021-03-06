﻿using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DnDTool2.View
{
    /// <summary>
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        private Window window;

        public MenuPage(Window window)
        {
            InitializeComponent();
            this.window = window;
        }

        private void MonsterManualButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MonsterManual(window));
        }

        private void CharacterVaultButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SpellTomeButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SpellTome(window));
        }

        private void ItemCompendium_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
