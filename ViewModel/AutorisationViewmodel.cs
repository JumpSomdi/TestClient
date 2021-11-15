using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Collections.ObjectModel;
using TestClient.Comand.BaseComand;
using System.Windows.Data;
using System.Collections;

namespace TestClient.ViewModel
{
    class AutorisationViewModel : BaseViewModel
    {
        public AutorisationViewModel()
        {
            // Нужно синхронизировать коллекцию с привязками.
            BindingOperations.EnableCollectionSynchronization(Messages, ((ICollection)Messages).SyncRoot);

            Autorisation = LambdaCommand.Create<string>(OnAutorisation, CanAutorisation);
            SendMessage = LambdaCommand.Create<string>(OnSendMessage, OnCanSendMessage);

            Messages.Add(new MessageData("Авторизируйтесь."));
        }

        private void Client_ReciveMsgEvent(string message)
        {
            // Если коллкция синхронизированно, то переход в UI поток не нужен.
            // Application.Current.Dispatcher.Invoke(() => Messages.Add(new MessageData(message)));

            Messages.Add(new MessageData(message));
        }

        //Команды
        public LambdaCommand SendMessage { get; }
        public bool CanSendMessage(object p) => true;
        private void OnSendMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Model.Manager.SendMessage(message.Trim());
                //Messages.Add(new MessageData(_message)); Сообщение должно добавлять через чат
                Message = "";
            }
        }
        private bool OnCanSendMessage(string message)
        {
            return !string.IsNullOrEmpty(message);
        }

        public LambdaCommand Autorisation { get; }
        private bool CanAutorisation(string login) => !string.IsNullOrWhiteSpace(login);

        private void OnAutorisation(string login)
        {
            Model.Manager.Client = new Model.Client(login.Trim());
            Model.Manager.Client.ReciveMsgEvent += Client_ReciveMsgEvent;
        }

        //Свойства

        public ObservableCollection<MessageData> Messages { get; } = new ObservableCollection<MessageData>();

        private string _login;
        public string Login
        {
            get => _login;
            set => Set(ref _login, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => Set(ref _message, value);
        }
    }
}
