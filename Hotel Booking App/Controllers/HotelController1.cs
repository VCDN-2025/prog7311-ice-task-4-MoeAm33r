using Hotel_Booking_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking_App.Controllers
{
    // Controllers/HotelController.cs
    public class HotelController
    {
        private readonly IHotelRepository _repository;
        private readonly HotelView _view;

        public HotelController(IHotelRepository repository, HotelView view)
        {
            _repository = repository;
            _view = view;
        }

        public void ShowAllHotels()
        {
            var hotels = _repository.GetAllHotels();
            _view.DisplayHotels(hotels);
        }

        public void ShowHotelDetails(int id)
        {
            var hotel = _repository.GetHotelById(id);
            if (hotel != null)
            {
                _view.DisplayHotelDetails(hotel);
            }
            else
            {
                _view.DisplayMessage("Hotel not found.");
            }
        }

        public void CreateBooking(int hotelId, string guestName, DateTime checkIn, DateTime checkOut, int rooms)
        {
            try
            {
                var booking = new Booking
                {
                    HotelId = hotelId,
                    GuestName = guestName,
                    CheckInDate = checkIn,
                    CheckOutDate = checkOut,
                    NumberOfRooms = rooms
                };

                _repository.AddBooking(booking);
                _view.DisplayMessage($"Booking successful! Total price: {booking.TotalPrice:C}");
            }
            catch (Exception ex)
            {
                _view.DisplayMessage($"Error: {ex.Message}");
            }
        }

        public void CancelBooking(int bookingId)
        {
            var booking = _repository.GetBookingById(bookingId);
            if (booking != null)
            {
                _repository.CancelBooking(bookingId);
                _view.DisplayMessage("Booking cancelled successfully.");
            }
            else
            {
                _view.DisplayMessage("Booking not found.");
            }
        }

        public void ShowAllBookings()
        {
            var bookings = _repository.GetAllBookings();
            _view.DisplayBookings(bookings);
        }
    }
}
