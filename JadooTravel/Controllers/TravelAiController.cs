using JadooTravel.Services.TravelAiServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers
{
    public class TravelAiController : Controller 
    {
        private readonly ITravelAiService _service;

        public TravelAiController(ITravelAiService service)
        {
            _service = service;
        }

        [HttpPost] 
        public async Task<IActionResult> GetPlaces(string city, string country)
        {
            try
            {
                var result = await _service.GetPlacesAsync(city, country);
                return PartialView("_TravelAiResultPartial", result);
            }
            catch (Exception ex)
            {
                return Content($"<div class='alert alert-danger'>Bir hata oluştu: {ex.Message}</div>");
            }
        }
    }
}
