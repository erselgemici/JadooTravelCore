using AutoMapper;
using JadooTravel.Dtos.TestimonialDtos;
using JadooTravel.Dtos.TripPlanDtos;
using JadooTravel.Services.TripPlanServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers
{
    public class TripPlanController : Controller
    {
        private readonly ITripPlanService _tripPlanService;
        private readonly IMapper _mapper;

        public TripPlanController(ITripPlanService tripPlanService, IMapper mapper)
        {
            _tripPlanService = tripPlanService;
            _mapper = mapper;
        }

        public async Task<IActionResult> TripPlanList()
        {
            var values = await _tripPlanService.GetAllTripPlanAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateTripPlan()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTripPlan(CreateTripPlanDto createTripPlanDto)
        {
            await _tripPlanService.CreateTripPlanAsync(createTripPlanDto);
            return RedirectToAction("TripPlanList");
        }

        public async Task<IActionResult> DeleteTripPlan(string id)
        {
            await _tripPlanService.DeleteTripPlanAsync(id);
            return RedirectToAction("TripPlanList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTripPlan(string id)
        {
            var value = await _tripPlanService.GetTripPlanByIdAsync(id);
            var updateModel = _mapper.Map<UpdateTripPlanDto>(value);
            return View(updateModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTripPlan(UpdateTripPlanDto updateTripPlanDto)
        {
            await _tripPlanService.UpdateTripPlanAsync(updateTripPlanDto);
            return RedirectToAction("TripPlanList");
        }
    }
}
