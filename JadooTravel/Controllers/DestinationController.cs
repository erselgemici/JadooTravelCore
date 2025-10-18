using AutoMapper;
using JadooTravel.Dtos.DestinationDtos;
using JadooTravel.Services.DestinationServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers
{
    public class DestinationController : Controller
    {
        private readonly IDestinationService _destinationService;
        private readonly IMapper _mapper;
        public DestinationController(IDestinationService destinationService, IMapper mapper)
        {
            _destinationService = destinationService;
            _mapper = mapper;
        }

        public async Task<IActionResult> DestinationList()
        {
            var values = await _destinationService.GetAllDestinationAsync();
            return View(values);
        }
        public IActionResult CreateDestination()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateDestination(CreateDestinationDto createDestinationDto)
        {
            await _destinationService.CreateDestinationAsync(createDestinationDto);
            return RedirectToAction("DestinationList");
        }
        public async Task<IActionResult> DeleteDestination(string id)
        {
            await _destinationService.DeleteDestinationAsync(id);
            return RedirectToAction("DestinationList");
        }
        public async Task<IActionResult> UpdateDestination(string id)
        {
            var value = await _destinationService.GetDestinationByIdAsync(id);
            var updateModel = _mapper.Map<UpdateDestinationDto>(value);
            return View(updateModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDestination(UpdateDestinationDto updateDestinationDto)
        {
            await _destinationService.UpdateDestinationAsync(updateDestinationDto);
            return RedirectToAction("DestinationList");
        }
    }
}
