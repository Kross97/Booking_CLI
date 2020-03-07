using System;
using System.Collections.Generic;
using System.Text;
using Booking.Repository;
using System.Threading;
using Domain;
using Domain.Models;

namespace Booking.Manager
{
    public class ClientManager
    {
        public List<Client> clients = new List<Client>();

        private ClientRepository repo = new ClientRepository();

        private Thread flow;
        public ClientManager()
        {
            flow = new Thread(GetClients);
            flow.Start();
        }

        public ClientManager(ClientRepository repo) : this()
        {
            this.repo = repo;
        }

        private void GetClients()
        {
            clients = repo.GetAllClients();
        }

        public void AddClient(string name, string surname, int age)
        {
            if (!String.IsNullOrWhiteSpace(name) || !String.IsNullOrWhiteSpace(surname) || age != 0)
            {
                try
                {
                    Client newClient = new Client(name, surname, age);
                    repo.AddClient(newClient);
                    Thread newFlow = new Thread(GetClients);
                    newFlow.Start();

                } catch(Exception e) {
                    Console.WriteLine(e.Message);
                }
            } else
            {
                Console.WriteLine("Не все данные переданны! Пользователь не создастся!");
                Console.WriteLine("Введите имя, фамилию, возраст");
            }
               
        }

        public void CreateClientsLog()
        {
            repo.CreateClientsLog();
        }

        public void RemoveClient(Guid id)
        {
            Client client = clients.Find(c => c.IdClient == id);
            Console.WriteLine(client.SurName);
            Console.WriteLine(client.Name);
            clients.Remove(client);
            repo.UpdateListClients(clients);
        }

        public void UpdateClient(Guid id, ClientParams param)
        {
            int currentIndex = clients.FindIndex(client => client.IdClient == id);
            if (String.IsNullOrWhiteSpace(param.Name) || String.IsNullOrWhiteSpace(param.SurName) || param.Age == 0)
            {
                Console.WriteLine("Не все поля заполнены! Для изменения клиента заполните все поля!");
                Console.WriteLine("Обязательные поля Имя, Фамилия, Возраст");
                return;
            }
            clients[currentIndex].Name = param.Name;
            clients[currentIndex].SurName = param.SurName;
            clients[currentIndex].Age = param.Age;
            repo.UpdateListClients(clients);
        }
    }
}
