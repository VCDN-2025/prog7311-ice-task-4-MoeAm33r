namespace Hotel_Booking_App.Models
{
    // Models/Hotel.cs
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public decimal PricePerNight { get; set; }
        public int AvailableRooms { get; set; }
    }

    // Models/Booking.cs
    public class Booking
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string GuestName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfRooms { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
    }

    // Models/IHotelRepository.cs (Interface for data access)
    public interface IHotelRepository
    {
        List<Hotel> GetAllHotels();
        Hotel GetHotelById(int id);
        void AddHotel(Hotel hotel);
        void UpdateHotel(Hotel hotel);
        void DeleteHotel(int id);

        List<Booking> GetAllBookings();
        Booking GetBookingById(int id);
        void AddBooking(Booking booking);
        void CancelBooking(int id);
    }

    // Models/HotelRepository.cs (Concrete implementation)
    public class HotelRepository : IHotelRepository
    {
        private readonly List<Hotel> _hotels;
        private readonly List<Booking> _bookings;
        private int _nextHotelId = 1;
        private int _nextBookingId = 1;

        public HotelRepository()
        {
            _hotels = new List<Hotel>
        {
            new Hotel { Id = _nextHotelId++, Name = "Grand Plaza", Location = "New York", PricePerNight = 199.99m, AvailableRooms = 10 },
            new Hotel { Id = _nextHotelId++, Name = "Beach Resort", Location = "Miami", PricePerNight = 249.99m, AvailableRooms = 5 },
            new Hotel { Id = _nextHotelId++, Name = "Mountain Lodge", Location = "Denver", PricePerNight = 179.99m, AvailableRooms = 8 }
        };

            _bookings = new List<Booking>();
        }

        public List<Hotel> GetAllHotels() => _hotels;

        public Hotel GetHotelById(int id) => _hotels.FirstOrDefault(h => h.Id == id);

        public void AddHotel(Hotel hotel)
        {
            hotel.Id = _nextHotelId++;
            _hotels.Add(hotel);
        }

        public void UpdateHotel(Hotel hotel)
        {
            var index = _hotels.FindIndex(h => h.Id == hotel.Id);
            if (index != -1)
            {
                _hotels[index] = hotel;
            }
        }

        public void DeleteHotel(int id)
        {
            var hotel = GetHotelById(id);
            if (hotel != null)
            {
                _hotels.Remove(hotel);
            }
        }

        public List<Booking> GetAllBookings() => _bookings;

        public Booking GetBookingById(int id) => _bookings.FirstOrDefault(b => b.Id == id);

        public void AddBooking(Booking booking)
        {
            var hotel = GetHotelById(booking.HotelId);
            if (hotel == null || hotel.AvailableRooms < booking.NumberOfRooms)
            {
                throw new InvalidOperationException("Not enough rooms available");
            }

            booking.Id = _nextBookingId++;
            booking.TotalPrice = hotel.PricePerNight * booking.NumberOfRooms *
                                (decimal)(booking.CheckOutDate - booking.CheckInDate).TotalDays;
            _bookings.Add(booking);

            // Update available rooms
            hotel.AvailableRooms -= booking.NumberOfRooms;
        }

        public void CancelBooking(int id)
        {
            var booking = GetBookingById(id);
            if (booking != null)
            {
                var hotel = GetHotelById(booking.HotelId);
                if (hotel != null)
                {
                    hotel.AvailableRooms += booking.NumberOfRooms;
                }
                _bookings.Remove(booking);
            }
        }
    }
}
