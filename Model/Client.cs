using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TestClient.Model
{
    class Client
    {
        private TcpClient tcpClient = new TcpClient("127.0.0.1", 7000);
        private NetworkStream networkStream;

        public delegate void ReciveMsg(string message);
        public event ReciveMsg ReciveMsgEvent;

        public Client(string login)
        {
            networkStream = tcpClient.GetStream();

            string userName = login;
            byte[] name = Encoding.UTF8.GetBytes(userName);
            networkStream.Write(name, 0, name.Length);

            Thread thread = new Thread(new ThreadStart(ReciveMessage));
            thread.Start();
        }

        public void SendMessage(string mess)
        {
            string wroteMessage = mess;
            byte[] sentMessage = Encoding.UTF8.GetBytes(wroteMessage);
            networkStream.Write(sentMessage, 0, sentMessage.Length);
        }

        private void ReciveMessage()
        {
            while (true)
            {
                byte[] recivedMessage = new byte[64];
                int countRecivedMessage = 0;

                do 
                {
                    try
                    {
                        countRecivedMessage = networkStream.Read(recivedMessage, 0, recivedMessage.Length);
                    }
                    catch(Exception ex)
                    {
                        Environment.Exit(1);
                    }
                }
                while (networkStream.DataAvailable);

                ReciveMsgEvent?.Invoke(Encoding.UTF8.GetString(recivedMessage, 0, countRecivedMessage));
              //Console.WriteLine(Encoding.UTF8.GetString(recivedMessage, 0, countRecivedMessage));
            }
        }

        public void Disconect()
        {
            tcpClient.Close();
            networkStream.Close();
        }
    }
}
