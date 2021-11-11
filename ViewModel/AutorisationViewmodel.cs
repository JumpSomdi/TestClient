using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Collections.ObjectModel;

namespace TestClient.ViewModel
{
    class AutorisationViewmodel : BaseViewModel
    {
        public AutorisationViewmodel()
        {
            Autorisation = new Comand.BaseComand.LambdaCommand(OnAutorisation, CanAutorisation);
            SendMessage = new Comand.BaseComand.LambdaCommand(OnSendMessage, CanAutorisation);
            
            msg = new ObservableCollection<MessageData>();
            msg.Add(new MessageData("Вы вошли в чат"));
        }

        private void Client_ReciveMsgEvent(string message)
        {
            Application.Current.Dispatcher.Invoke(() => msg.Add(new MessageData(message)));
        }

//Команды
        public ICommand SendMessage { get; }
        public bool CanSendMessage(object p) => true;
        public void OnSendMessage(object p)
        {
            if (_Message != "")
            {
                Model.Manager.SendMessage(_Message);
                msg.Add(new MessageData(_Message));
                Message = "";
            }
        }

        public ICommand Autorisation { get; }
        private bool CanAutorisation(object p) => true;

        private void OnAutorisation(object p)
        {
            Model.Manager.Client = new Model.Client(_Login); 
            Model.Manager.Client.ReciveMsgEvent += Client_ReciveMsgEvent;
        }

        //Свойства

        private ObservableCollection<MessageData> _Msg;
        public ObservableCollection<MessageData> msg 
        { set { Set(ref _Msg, value); } 
            get {return _Msg; } 
        }

        private string _Login;
        public string Login
        {
            set { Set(ref _Login, value); }
            get { return _Login; }
        }

        private string _Message;
        public string Message
        {
            set { Set(ref _Message, value); }
            get { return _Message; }
        }
    }

    class MessageData
    {
        public string MessageText { get; set; }
        public DateTime MessageTime { get; set; }
        public MessageData(string mes)
        {
            MessageText = mes;
            MessageTime = DateTime.Now;
        }
    }
}
