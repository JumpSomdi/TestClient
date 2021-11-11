using System;
using TestClient.Model;

namespace TestClient.Model
{
    static class Manager
    {
        public static Client Client;

        public static void SendMessage(string message)
        {
            Client.SendMessage(message);
        }
    }
}
