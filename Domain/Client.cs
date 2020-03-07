using System;

namespace Domain
{
    [Serializable]
    public class Client
    {
        public Guid IdClient { get; set; }
        public string Name { get; set; }

        public string SurName { get; set; }

        private int age;
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if (value < 18)
                {
                    throw new Exception("Без родителей вам нельзя бронировать отель!");
                   
                }
                else
                {
                    age = value;
                }
            }
        }

        public Client()
        {
            this.IdClient = Guid.NewGuid();
            this.Name = "unknow";
            this.SurName = "unknow";
            this.Age = 18;
        }
        public Client(string name): this()
        {
            this.Name = name;
        }
        public Client(string name, string surname) : this(name)
        {
            this.SurName = surname;
        }
        public Client(string name, string surname, int age) : this(name, surname)
        {
            this.Age = age;
        }

        public string ClientAsToString()
        {
            return $"ID: {IdClient}, Name: {Name}, Surname: {SurName}, Age: {Age}";
        }
    }
}
