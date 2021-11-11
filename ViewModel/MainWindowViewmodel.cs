using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace TestClient.ViewModel
{
    class MainWindowViewmodel : BaseViewModel
    {
        public MainWindowViewmodel()
        {
            CloseAppCommand = new Comand.BaseComand.LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecuted);
        }

        #region Commands
        public ICommand CloseAppCommand { get; }

        private bool CanCloseAppCommandExecuted(object p) => true;
        
        private void OnCloseAppCommandExecuted(object p)
        { 
            
           //Page.NavigationService.Navigate(new Page1());
        }

        #endregion

        private string login;
        public string Login
        {
            get { return "Привет, " + login; }
            set { Set(ref login, value); }
        }
    }
}
