using Hotel_Booking_App.Controllers;
using Hotel_Booking_App.Models;
using Hotel_Booking_App.Views;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Program.cs
class Program
{
    static void Main(string[] args)
    {
        // Initialize components
        IHotelRepository repository = new HotelRepository();
        HotelView view = new HotelView();
        HotelController controller = new HotelController(repository, view);

        bool running = true;

        while (running)
        {
            view.DisplayMainMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": // View All Hotels
                    controller.ShowAllHotels();
                    break;

                case "2": // View Hotel Details
                    Console.Write("Enter Hotel ID: ");
                    if (int.TryParse(Console.ReadLine(), out int hotelId))
                    {
                        controller.ShowHotelDetails(hotelId);
                    }
                    else
                    {
                        view.DisplayMessage("Invalid Hotel ID.");
                    }
                    break;

                case "3": // Make a Booking
                    try
                    {
                        var bookingDetails = view.GetBookingDetails();
                        controller.CreateBooking(
                            bookingDetails.HotelId,
                            bookingDetails.GuestName,
                            bookingDetails.CheckInDate,
                            bookingDetails.CheckOutDate,
                            bookingDetails.NumberOfRooms);
                    }
                    catch (Exception ex)
                    {
                        view.DisplayMessage($"Error: {ex.Message}");
                    }
                    break;

                case "4": // View All Bookings
                    controller.ShowAllBookings();
                    break;

                case "5": // Cancel a Booking
                    try
                    {
                        int bookingId = view.GetBookingIdToCancel();
                        controller.CancelBooking(bookingId);
                    }
                    catch (Exception ex)
                    {
                        view.DisplayMessage($"Error: {ex.Message}");
                    }
                    break;

                case "6": // Exit
                    running = false;
                    break;

                default:
                    view.DisplayMessage("Invalid choice. Please try again.");
                    break;
            }
        }

        view.DisplayMessage("Thank you for using the Hotel Booking System. Goodbye!");
    }
}