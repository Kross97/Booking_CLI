using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using System.IO;
using System.Threading;
using System.Xml.Serialization;

namespace Booking.Repository
{
    public class ReservationRepository
    {
        private string pathAllReservation = @"D:\DataBooking\AllReservation.xml";
        private string pathReservationLog = @"D:\DataBooking\ReservationLog.txt";
        private List<Reservation> reservations = new List<Reservation>();
        private XmlSerializer formatter = new XmlSerializer(typeof(List<Reservation>));
        private Mutex mutexRepo = new Mutex();
        private Thread flowReservs;
        public ReservationRepository()
        {
            if (!Directory.Exists(@"D:\DataBooking"))
            {
                Directory.CreateDirectory(@"D:\DataBooking");
            }

           flowReservs = new Thread(GetAllReservations);
           flowReservs.Start();
        }

        public void GetAllReservations ()
        {
            mutexRepo.WaitOne();
            try
            {
               
                using (FileStream fs = new FileStream(pathAllReservation, FileMode.Open))
                {

                    reservations = (List<Reservation>)formatter.Deserialize(fs);

                }
            } catch
            {
                Console.WriteLine("Первая Резервация! В списке пока нет Резерваций");
            }
            mutexRepo.ReleaseMutex();
        }

        public void AddNewReservation (Reservation reserv)
        {
            bool flag = true;
            foreach(Reservation reservation in reservations)
            {
                
                if (reservation.HotelCurrent.Name == reserv.HotelCurrent.Name && reservation.HotelCurrent.Country == reserv.HotelCurrent.Country)
                { 
                    if((reserv.CheckInDate < reservation.CheckOutDate && reserv.CheckInDate > reservation.CheckInDate) ||
                       (reserv.CheckOutDate > reservation.CheckInDate && reserv.CheckOutDate < reservation.CheckOutDate) ||
                       (reserv.CheckInDate == reservation.CheckInDate && reserv.CheckOutDate == reservation.CheckOutDate))
                    {
                     
                        flag = false;
                    }
                }
            }

            if(flag)
            {
                reservations.Add(reserv);
                mutexRepo.WaitOne();
                using (FileStream fs = new FileStream(pathAllReservation, FileMode.Create))
                {
                    formatter.Serialize(fs, reservations);
                }
                mutexRepo.ReleaseMutex();
            }  else
            {
                Console.WriteLine("На введенные даты отель уже забронирован!");
            }  
        }

        public void CreateReservationsLog()
        {
            if(reservations.Count != 0)
            {
                using(StreamWriter writer = new StreamWriter(pathReservationLog))
                {
                    foreach(Reservation reservation in reservations)
                    {
                        writer.WriteLine(reservation.ReservAsString());
                    }
                }
            }
        }

        public List<Reservation> GetReservations()
        {
            Thread.Sleep(200);
            return reservations;
        }

        public void RemoveReservation (Guid id)
        {
            Reservation currentReserv = reservations.Find((r) => r.IdReservation == id);
            reservations.Remove(currentReserv);
            mutexRepo.WaitOne();
            using (FileStream fs = new FileStream(pathAllReservation, FileMode.Create))
            {
                formatter.Serialize(fs, reservations);
            }
            mutexRepo.ReleaseMutex();
        }

    }
}
