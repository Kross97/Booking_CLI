using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Threading;
using Domain;

namespace Booking.Repository
{
    public class HotelRepository
    {
        private string pathLog = @"D:\DataBooking\HotelsLog.txt";
        private string pathHotels = @"D:\DataBooking\HotelsObj.txt";
        private BinaryFormatter formatter = new BinaryFormatter();
        private Mutex mutexRepo = new Mutex();
        public HotelRepository()
        {
            if (!Directory.Exists(@"D:\DataBooking"))
            {
                Directory.CreateDirectory(@"D:\DataBooking");
            }
        }
        public void AddHotel (Hotel hotel)
        {
            mutexRepo.WaitOne();
            using (FileStream fs = new FileStream(pathHotels, FileMode.Append))
            {
                formatter.Serialize(fs, hotel);
            }

            mutexRepo.ReleaseMutex();
        }

        public void UpdatingListHotels(List<Hotel> hotels)
        {
            mutexRepo.WaitOne();
            using (FileStream fs = new FileStream(pathHotels, FileMode.Create))
            {
                foreach(Hotel h in hotels)
                {
                    formatter.Serialize(fs, h);
                }
            }
            mutexRepo.ReleaseMutex();
        }

        public List<Hotel> GetAllHotels()
        {
            List<Hotel> hotels = new List<Hotel>();
            mutexRepo.WaitOne();
            using (FileStream fs = new FileStream(pathHotels, FileMode.OpenOrCreate))
            {
                bool flag = true;
                while (flag)
                {
                    try
                    {
                        Hotel hotel = (Hotel)formatter.Deserialize(fs);
                        hotels.Add(hotel);
                    } 
                    catch {
                        flag = false;
                    }
                }
            }
            mutexRepo.ReleaseMutex();
            return hotels;
        }

        public void CreateHotelsLog()
        {
            List<Hotel> hotels = new List<Hotel>();
            mutexRepo.WaitOne();
            using (FileStream fs = new FileStream(pathHotels, FileMode.Open))
            {
                bool flag = true;
                while (flag)
                {
                    try
                    {
                        Hotel hotel = (Hotel)formatter.Deserialize(fs);
                        hotels.Add(hotel);
                    } catch
                    {
                        flag = false;
                    }
                }
            }
            mutexRepo.ReleaseMutex();
            using (StreamWriter writer = new StreamWriter(pathLog))
            {
                foreach(Hotel h in hotels)
                {
                    writer.WriteLine(h.HotelAsString());
                }
            }
        }
    }
}
