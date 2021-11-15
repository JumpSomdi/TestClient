using System;

namespace TestClient.ViewModel
{
    public class MessageData
    {
        public string MessageText { get;}
        public DateTime MessageTime { get;}
        public MessageData(string mes)
        {
            MessageText = mes;
            MessageTime = DateTime.Now;
        }
    }
}
