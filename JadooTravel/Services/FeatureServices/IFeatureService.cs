using JadooTravel.Dtos.FeatureDtos;

namespace JadooTravel.Services.FeatureServices
{
    public interface IFeatureService
    {
        Task<List<ResultFeatureDto>> GetAllFeatureAsync();
        Task<GetFeatureByIdDto> GetFeatureAsync(string id);
        Task CreateFeatureAsync(CreateFeatureDto createFeatureDto);
        Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto);
        Task DeleteFeatureAsync(string id);
    }
}
