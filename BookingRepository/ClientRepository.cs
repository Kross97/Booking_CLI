using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using Domain;

namespace Booking.Repository
{
    public class ClientRepository
    {
        private string pathClientLog = @"D:\DataBooking\clientsLog.txt";
        private string pathClient = @"D:\DataBooking\clientObj.txt";
        private BinaryFormatter formatter = new BinaryFormatter();
        private Mutex mutexClient = new Mutex();
        public ClientRepository()
        {
            if (!Directory.Exists(@"D:\DataBooking"))
            {
                Directory.CreateDirectory(@"D:\DataBooking");
            }
        }

        public List<Client> GetAllClients()
        {
            List<Client> clients = new List<Client>();
            mutexClient.WaitOne();
            using(FileStream fs = new FileStream(pathClient, FileMode.OpenOrCreate))
            {
                bool flag = true;
                while (flag)
                {
                    try
                    {
                        Client client = (Client)formatter.Deserialize(fs);
                        clients.Add(client);
                    } catch
                    {
                        flag = false;
                    }
                }
            }
            mutexClient.ReleaseMutex();
            return clients;
        }

        public void AddClient(Client client)
        {
            mutexClient.WaitOne();
            using(FileStream fs = new FileStream(pathClient, FileMode.Append))
            {
                formatter.Serialize(fs, client);
            }
            mutexClient.ReleaseMutex();
        }

        public void UpdateListClients(List<Client> clients)
        {
            mutexClient.WaitOne();
            using(FileStream fs = new FileStream(pathClient, FileMode.Create))
            {
                foreach(Client c in clients)
                {
                    formatter.Serialize(fs, c);
                }
            }
            mutexClient.ReleaseMutex();
        }

        public void CreateClientsLog()
        {
            List<Client> clients = new List<Client>();
            mutexClient.WaitOne();
            using(FileStream fs = new FileStream(pathClient, FileMode.Open))
            {
                bool flag = true;
                while (flag)
                {
                    try
                    {
                        Client client = (Client)formatter.Deserialize(fs);
                        clients.Add(client);
                    } catch
                    {
                        flag = false;
                    }
                }
            }
            mutexClient.ReleaseMutex();

            using(StreamWriter writer = new StreamWriter(pathClientLog))
            {
                foreach(Client c in clients)
                {
                    writer.WriteLine(c.ClientAsToString());
                }
            }
        }
    }
}
