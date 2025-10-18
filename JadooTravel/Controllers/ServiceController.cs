using AutoMapper;
using JadooTravel.Dtos.ServiceDtos;
using JadooTravel.Dtos.TestimonialDtos;
using JadooTravel.Services.ServiceServices;
using JadooTravel.Services.TestimonialServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IMapper _mapper;

        public ServiceController(IServiceService serviceService, IMapper mapper)
        {
            _serviceService = serviceService;
            _mapper = mapper;
        }

        public async Task<IActionResult> ServiceList()
        {
            var value = await _serviceService.GetAllServiceAsync();
            return View(value);
        }

        public async Task<IActionResult> DeleteService(string id)
        {
            await _serviceService.DeleteServiceAsync(id);
            return RedirectToAction("ServiceList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateService(string id)
        {
            var value = await _serviceService.GetServiceByIdAsync(id);
            var updateModel = _mapper.Map<UpdateServiceDto>(value);
            return View(updateModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateService(UpdateServiceDto updateServiceDto)
        {
            await _serviceService.UpdateServiceAsync(updateServiceDto);
            return RedirectToAction("ServiceList");

        }

        [HttpGet]
        public IActionResult CreateService()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceDto createServiceDto)
        {
            await _serviceService.CreateServiceAsync(createServiceDto);
            return RedirectToAction("ServiceList");

        }
    }
}
