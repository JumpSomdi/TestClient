using System;
using System.Windows.Input;

namespace TestClient.Comand.BaseComand
{
    public abstract class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly EventHandler requerySuggested;

        protected Command()
        {
            requerySuggested = (_, __) => RaiseCanExecuteChanged();
            CommandManager.RequerySuggested += requerySuggested;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);
    }
}
