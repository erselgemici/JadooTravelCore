using JadooTravel.Services.BookingStepServices;
using Microsoft.AspNetCore.Mvc;
namespace JadooTravel.ViewComponents
{
    public class _DefaultBookingComponentPartial : ViewComponent
    {
        private readonly IBookingService _bookingService;

        public _DefaultBookingComponentPartial(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public IViewComponentResult Invoke()
        {
            
            return View();
        }
       
    }
}

