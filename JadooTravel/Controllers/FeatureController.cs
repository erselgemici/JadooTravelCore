using AutoMapper;
using JadooTravel.Dtos.DestinationDtos;
using JadooTravel.Dtos.FeatureDtos;
using JadooTravel.Services.FeatureServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers
{
    public class FeatureController : Controller
    {
        private readonly IFeatureService _featureService;
        private readonly IMapper _mapper;

        public FeatureController(IFeatureService featureService, IMapper mapper)
        {
            _featureService = featureService;
            _mapper = mapper;
        }
        public async Task<IActionResult> FeatureList()
        {
            var values = await _featureService.GetAllFeatureAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateFeature()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            await _featureService.CreateFeatureAsync(createFeatureDto);
            return RedirectToAction("FeatureList");

        }

        [HttpGet]
        public async Task<IActionResult> UpdateFeature(string id)
        {
            var value = await _featureService.GetFeatureAsync(id);
            var updateModel = _mapper.Map<UpdateFeatureDto>(value);
            return View(updateModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto dto)
        {
            await _featureService.UpdateFeatureAsync(dto);
            return RedirectToAction("FeatureList");
        }

        public async Task<ActionResult> DeleteFeature(string id)
        {
            await _featureService.DeleteFeatureAsync(id);
            return RedirectToAction("FeatureList");
        }
    }
}
