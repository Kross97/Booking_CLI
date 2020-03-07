using System;
using Domain;
using Booking.Manager;
using System.Threading;

namespace Booking
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientManager clientManager;
            HotelManager hotelManager;
            ReservationManager reservationManager;

            string control = "s";
            while(control != "0")
            {
                Console.WriteLine("Чтобы создать клиента нажмите 1");
                Console.WriteLine("Чтобы удалить клиента нажмите 2");
                Console.WriteLine("Чтобы создать отель нажмите 3");
                Console.WriteLine("Чтобы удалить отель нажмите 4");
                Console.WriteLine("Чтобы создать бронь нажмите 5");
                Console.WriteLine("Чтобы удалить бронь нажмите 6");
                Console.WriteLine("Чтобы выйти нажмите 0");
                Console.Write("Ввод: ");
                control = Console.ReadLine();
                if (control == "1")
                {
                    clientManager = new ClientManager();
                    Console.Write("Введите имя: ");
                    string name = Console.ReadLine();
                    Console.Write("Введите Фамилию: ");
                    string surname = Console.ReadLine();
                    Console.Write("Введите возраст: ");
                    string age = Console.ReadLine();
                    clientManager.AddClient(name, surname, int.Parse(age));
                    clientManager.CreateClientsLog();
                }

                if( control == "2")
                {
                    clientManager = new ClientManager();
                    Thread.Sleep(300); 
                    if(clientManager.clients.Count == 0)
                    {
                        Console.WriteLine("Увы сегодня некого удалить!");
                        Console.WriteLine(" ");
                    } else
                    {
                        int numbClient = 0;
                        foreach(Client client in clientManager.clients)
                        {
                            Console.WriteLine($"{numbClient + 1}. {client.Name}");
                        }
                        Console.Write("Выберите номер кого удалить:");
                        string numbClient2 = Console.ReadLine();
                        numbClient = int.Parse(numbClient2);

                        clientManager.RemoveClient(clientManager.clients[numbClient - 1].IdClient);
                        Console.WriteLine("Он больше не забронирует не один отель :) ");
                        Console.WriteLine(" ");
                    }
                }

                if (control == "3")
                {
                    hotelManager = new HotelManager();
                    Console.Write("Введите название: ");
                    string name = Console.ReadLine();
                    Console.Write("Введите рейтинг: ");
                    string rating = Console.ReadLine();
                    Console.Write("Введите страну: ");
                    string country = Console.ReadLine();
                    hotelManager.AddHotel(name, Double.Parse(rating), country);
                    hotelManager.CreateHotelsLog();
                }

                if (control == "4")
                {
                    hotelManager = new HotelManager();
                    Thread.Sleep(300);
                    if(hotelManager.hotels.Count == 0)
                    {
                        Console.WriteLine("Ни одного отеля нет для разрушения");
                        Console.WriteLine(" ");
                    } else
                    {
                        int numbHotel = 0;
                        foreach(Hotel hotel in hotelManager.hotels)
                        {
                            Console.WriteLine($"{numbHotel + 1}. {hotel.Name} - {hotel.Country}");
                        }
                        Console.Write("Введите номер отеля для разрушения: ");
                        string numbHotel2 = Console.ReadLine();
                        numbHotel = int.Parse(numbHotel2);
                        hotelManager.RemoveHotel(hotelManager.hotels[numbHotel - 1].IdHotel);
                        Console.WriteLine("Отель разрушен! Постояльцы переселенны , персонал ищет работу в другом месте :)");
                        Console.WriteLine(" ");
                    }
                }

                if (control == "5")
                {
                    reservationManager = new ReservationManager();
                    Thread.Sleep(300);
                    if(reservationManager.clients.Count == 0 || reservationManager.hotels.Count == 0)
                    {
                        Console.WriteLine("В базе нет клиентов или отелей. Добавьте их!");
                        Console.WriteLine(" ");
                        continue;
                    }
                    Console.WriteLine("Выберите для какого клиента: ");
                    int numbClient = 0;
                    foreach(Client client in reservationManager.clients)
                    {
                        Console.WriteLine($"{numbClient + 1}.{client.Name}");
                    }
                    Console.Write("Введите нормер клиента: ");
                    string numbClient2 = Console.ReadLine();
                    numbClient = int.Parse(numbClient2);
                    reservationManager.AddClient(reservationManager.clients[numbClient - 1].IdClient);
                    Console.WriteLine("Выберите отель: ");
                    int numbHotel = 0;
                    foreach(Hotel hotel in reservationManager.hotels)
                    {
                        Console.WriteLine($"{numbHotel + 1}.{hotel.Name} - {hotel.Country}");
                    }
                    Console.Write("Введите нормер отеля: ");
                    string numbHotel2 = Console.ReadLine();
                    numbHotel = int.Parse(numbHotel2);
                    reservationManager.AddHotel(reservationManager.hotels[numbHotel - 1].IdHotel);

                   try
                    {

                        Console.WriteLine("Введите даты бронирования (от и до) : ");
                        Console.Write("Введите год (дата от):");
                        string d1Year = Console.ReadLine();
                        Console.Write("Введите месяц (дата от):");
                        string d1Month = Console.ReadLine();
                        Console.Write("Введите день (дата от):");
                        string d1Day = Console.ReadLine();

                        DateTime dateIn = new DateTime(int.Parse(d1Year), int.Parse(d1Month), int.Parse(d1Day));

                        Console.Write("Введите год (дата до):");
                        string d2Year = Console.ReadLine();
                        Console.Write("Введите месяц (дата до):");
                        string d2Month = Console.ReadLine();
                        Console.Write("Введите день (дата до):");
                        string d2Day = Console.ReadLine();

                        DateTime dateOut = new DateTime(int.Parse(d2Year), int.Parse(d2Month), int.Parse(d2Day));

                        reservationManager.AddDateReserv(dateIn, dateOut);
                        reservationManager.AddReservation();

                    } catch
                    {
                      Console.WriteLine("Дата некоректно введена попробуйте снова");
                        Console.WriteLine(" ");
                    }
                    
                }

                if (control == "6")
                {
                    reservationManager = new ReservationManager();
                    Thread.Sleep(300);
                    if(reservationManager.reservations.Count == 0)
                    {
                        Console.WriteLine("Ни одной брони нет в списке");
                        Console.WriteLine(" ");
                    } else
                    {
                        int numbReserv = 0;
                        foreach(Reservation reserv in reservationManager.reservations)
                        {
                            Console.WriteLine($"{numbReserv + 1}. {reserv.ClientCurrent.Name} {reserv.HotelCurrent.Name} - {reserv.HotelCurrent.Country}");
                            Console.WriteLine($"с {reserv.CheckInDate} по {reserv.CheckOutDate} ");
                        }

                        Console.Write("Введите номер брони:");
                        string numbReserv2 = Console.ReadLine();
                        numbReserv = int.Parse(numbReserv2);
                        reservationManager.RemoveReservation(reservationManager.reservations[numbReserv - 1].IdReservation);
                        Console.WriteLine("Теперь на эту дату отель забронирует кто то другой :)");
                        Console.WriteLine(" ");
                    }
                }
            }
        }
    }

}