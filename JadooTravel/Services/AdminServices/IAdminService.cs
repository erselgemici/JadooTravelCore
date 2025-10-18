using JadooTravel.Dtos.AdminDtos;
using JadooTravel.Entities;

namespace JadooTravel.Services.AdminServices
{
    public interface IAdminService
    {
        Task<List<Admin>> GetAllAdminsAsync();
        Task<Admin> LoginAsync(LoginAdminDto dto);
        Task CreateAdminAsync(CreateAdminDto dto);
        Task UpdateAdminAsync(UpdateAdminDto dto);
        Task DeleteAdminAsync(string id);
        Task<Admin> GetByIdAsync(string id);
    }
}
