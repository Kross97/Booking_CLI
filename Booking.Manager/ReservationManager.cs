using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Booking.Repository;
using System.Threading.Tasks;

namespace Booking.Manager
{
    public class ReservationManager
    {
        public Reservation reservation = new Reservation();

        public List<Client> clients = new List<Client>();

        public List<Hotel> hotels = new List<Hotel>();

        public List<Reservation> reservations = new List<Reservation>();

        private ClientRepository repoClients = new ClientRepository();

        private HotelRepository repoHotels = new HotelRepository();

        private ReservationRepository repoReservs = new ReservationRepository();

        private Task flowGetClients;

        private Task flowGetHotels;

        private Task flowGetReservations;
        
        public ReservationManager()
        {
            flowGetClients = Task.Run(GetClients);
            flowGetHotels = Task.Run(GetHotels);
            flowGetReservations = Task.Run(GetReservations);
        }

        private void GetClients()
        {
            clients = repoClients.GetAllClients();
        } 

        private void GetHotels()
        {
            hotels = repoHotels.GetAllHotels();
        }

        public void AddClient (Guid id)
        {
            foreach(Client client in clients)
            {
                if(client.IdClient == id)
                {
                    reservation.ClientCurrent = client;
                }
            }
        }

        public void AddHotel(Guid id)
        {
            foreach (Hotel hotel in hotels)
            {
                if (hotel.IdHotel == id)
                {
                    reservation.HotelCurrent = hotel;
                }
            }
        }

        public void AddDateReserv(DateTime inDate, DateTime outDate)
        {
            reservation.CheckInDate = inDate;
            reservation.CheckOutDate = outDate;
        }

        public void AddReservation ()
        {
            repoReservs.AddNewReservation(reservation);
        }

        public void CreateReservationsLog()
        {
            repoReservs.CreateReservationsLog();
        }

        private void GetReservations ()
        {
            reservations = repoReservs.GetReservations();
        }

        public void RemoveReservation (Guid id)
        {
            repoReservs.RemoveReservation(id);
        }
    }
}
