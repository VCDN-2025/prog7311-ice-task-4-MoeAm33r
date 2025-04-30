using Hotel_Booking_App.Models;

namespace Hotel_Booking_App.Views
{
    // Views/HotelView.cs
    public class HotelView
    {
        public void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Hotel Booking System");
            Console.WriteLine("1. View All Hotels");
            Console.WriteLine("2. View Hotel Details");
            Console.WriteLine("3. Make a Booking");
            Console.WriteLine("4. View All Bookings");
            Console.WriteLine("5. Cancel a Booking");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");
        }

        public void DisplayHotels(List<Hotel> hotels)
        {
            Console.Clear();
            Console.WriteLine("Available Hotels:");
            Console.WriteLine("--------------------------------------------------");
            foreach (var hotel in hotels)
            {
                Console.WriteLine($"ID: {hotel.Id}");
                Console.WriteLine($"Name: {hotel.Name}");
                Console.WriteLine($"Location: {hotel.Location}");
                Console.WriteLine($"Price per night: {hotel.PricePerNight:C}");
                Console.WriteLine($"Available rooms: {hotel.AvailableRooms}");
                Console.WriteLine("--------------------------------------------------");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void DisplayHotelDetails(Hotel hotel)
        {
            Console.Clear();
            Console.WriteLine("Hotel Details:");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"ID: {hotel.Id}");
            Console.WriteLine($"Name: {hotel.Name}");
            Console.WriteLine($"Location: {hotel.Location}");
            Console.WriteLine($"Price per night: {hotel.PricePerNight:C}");
            Console.WriteLine($"Available rooms: {hotel.AvailableRooms}");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public Booking GetBookingDetails()
        {
            Console.Clear();
            Console.WriteLine("Create New Booking");
            Console.WriteLine("--------------------------------------------------");

            Console.Write("Enter Hotel ID: ");
            int hotelId = int.Parse(Console.ReadLine());

            Console.Write("Enter Guest Name: ");
            string guestName = Console.ReadLine();

            Console.Write("Enter Check-In Date (yyyy-mm-dd): ");
            DateTime checkIn = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Check-Out Date (yyyy-mm-dd): ");
            DateTime checkOut = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Number of Rooms: ");
            int rooms = int.Parse(Console.ReadLine());

            return new Booking
            {
                HotelId = hotelId,
                GuestName = guestName,
                CheckInDate = checkIn,
                CheckOutDate = checkOut,
                NumberOfRooms = rooms
            };
        }

        public void DisplayBookings(List<Booking> bookings)
        {
            Console.Clear();
            Console.WriteLine("All Bookings:");
            Console.WriteLine("--------------------------------------------------");
            foreach (var booking in bookings)
            {
                Console.WriteLine($"ID: {booking.Id}");
                Console.WriteLine($"Guest: {booking.GuestName}");
                Console.WriteLine($"Hotel ID: {booking.HotelId}");
                Console.WriteLine($"Dates: {booking.CheckInDate:d} to {booking.CheckOutDate:d}");
                Console.WriteLine($"Rooms: {booking.NumberOfRooms}");
                Console.WriteLine($"Total Price: {booking.TotalPrice:C}");
                Console.WriteLine("--------------------------------------------------");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public int GetBookingIdToCancel()
        {
            Console.Write("Enter Booking ID to cancel: ");
            return int.Parse(Console.ReadLine());
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
