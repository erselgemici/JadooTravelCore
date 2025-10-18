using JadooTravel.Services.BookingStepServices;
using JadooTravel.Services.DestinationServices;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace JadooTravel.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IDestinationService _destinationService;

        public DashboardController(IBookingService bookingService, IDestinationService destinationService)
        {
            _bookingService = bookingService;
            _destinationService = destinationService;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingService.GetAllBookingAsync();
            var destinations = await _destinationService.GetAllDestinationAsync();

            // 🟢 İSTATİSTİK KUTULARI
            ViewBag.TotalTours = destinations.Count();
            ViewBag.TotalBookings = bookings.Count();
            ViewBag.TotalPeople = bookings.Sum(x => x.PersonCount);
            ViewBag.AvgPrice = destinations.Any() ? destinations.Average(x => x.Price).ToString("0.00") : "0";

            // 🔵 SON 7 GÜN GRAFİĞİ
            var last7Days = bookings
     .Where(x => x.BookingDate >= DateTime.Now.AddDays(-7))
     .GroupBy(x => new { x.BookingDate.Date, x.DestinationName })
     .Select(g => new
     {
         Date = g.Key.Date,
         Destination = g.Key.DestinationName,
         TotalPersons = g.Sum(x => x.PersonCount)
     })
     .OrderBy(x => x.Date)
     .ToList();

            ViewBag.WeekLabels = last7Days
                .Select(x => $"{x.Date:dd.MM} - {x.Destination}")
                .ToArray();

            ViewBag.WeekValues = last7Days
                .Select(x => x.TotalPersons)
                .ToArray();


            // 📊 1. Grafik – Turlara göre kişi sayısı
            var tourStats = bookings
                .GroupBy(x => x.DestinationName)
                .Select(g => new
                {
                    Tour = g.Key,
                    PersonCount = g.Sum(b => b.PersonCount)
                })
                .OrderByDescending(x => x.PersonCount)
                .Take(6)
                .ToList();

            ViewBag.TourLabels = tourStats.Select(x => x.Tour).ToArray();
            ViewBag.TourValues = tourStats.Select(x => x.PersonCount).ToArray();

            // 📊 2. Grafik – Aylık rezervasyon sayısı
            var monthlyStats = bookings
                .GroupBy(x => x.BookingDate.Month)
                .Select(g => new
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key),
                    Count = g.Count()
                })
                .OrderBy(g => g.Month)
                .ToList();

            ViewBag.MonthLabels = monthlyStats.Select(x => x.Month).ToArray();
            ViewBag.MonthValues = monthlyStats.Select(x => x.Count).ToArray();

            // 📊 3. Grafik – Şehirlere göre rezervasyon oranı
            var cityStats = bookings
                .GroupBy(x => x.DestinationName)
                .Select(g => new
                {
                    City = g.Key,
                    Count = g.Count()
                })
                .ToList();

            ViewBag.CityLabels = cityStats.Select(x => x.City).ToArray();
            ViewBag.CityValues = cityStats.Select(x => x.Count).ToArray();

            // 🧾 Son eklenen 5 tur
            var lastDestinations = destinations
                .OrderByDescending(x => x.DestinationId)
                .Take(5)
                .ToList();
            ViewBag.LastDestinations = lastDestinations;

            // 🖼️ Son 4 tur (kart görünümü)
            var lastFour = destinations
                .OrderByDescending(x => x.DestinationId)
                .Take(4)
                .ToList();
            ViewBag.LastFour = lastFour;

            return View();
        }
    }
}
