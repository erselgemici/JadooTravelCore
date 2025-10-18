using JadooTravel.Dtos.ServiceDtos;

namespace JadooTravel.Services.ServiceServices
{
    public interface IServiceService
    {
        Task<List<ResultServiceDto>> GetAllServiceAsync();
        Task<GetServiceByIdDto> GetServiceByIdAsync(string id);
        Task CreateServiceAsync(CreateServiceDto createServiceDto);
        Task UpdateServiceAsync(UpdateServiceDto updateServiceDto);
        Task DeleteServiceAsync(string id);
    }
}
