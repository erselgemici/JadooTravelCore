using JadooTravel.Dtos.BookingDtos;
using JadooTravel.Dtos.ReservationDtos;
using JadooTravel.Services.BookingStepServices;
using JadooTravel.Services.ReservationServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IReservationService _reservationService;

        public BookingController(IBookingService bookingService, IReservationService reservationService)
        {
            _bookingService = bookingService;
            _reservationService = reservationService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _bookingService.GetAllBookingAsync();
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDto dto)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Default");
            }

            await _bookingService.CreateBookingAsync(dto);
            TempData["BookingMessage"] = $"{dto.NameSurname}, rezervasyonunuz başarıyla oluşturuldu!";
            return RedirectToAction("Index", "Default");
        }
        [HttpGet]
        public async Task<IActionResult> Approve(string id)
        {
            await _bookingService.ApproveBookingAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await _bookingService.DeleteBookingAsync(id);
            return RedirectToAction("Index");
        }

    }
}



