using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace TestClient
{
    /// <summary>
    /// Логика взаимодействия для Autorisation.xaml
    /// </summary>
    public partial class Autorisation : Page
    {
        public Autorisation()
        {
            InitializeComponent();
        }

        private void Registration(object sender, RoutedEventArgs e)
        {
        }

        private void Auto(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page1());
        }
    }
}
