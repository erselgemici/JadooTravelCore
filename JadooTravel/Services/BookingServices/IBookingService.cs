using JadooTravel.Dtos.BookingDtos;

namespace JadooTravel.Services.BookingStepServices
{
    public interface IBookingService
    {
        Task<List<ResultBookingDto>> GetAllBookingAsync();
        //Task<CreateBookingDto> GetBookingByIdAsync(string id);
        Task CreateBookingAsync(CreateBookingDto dto);
        Task UpdateBookingAsync(UpdateBookingDto dto);
        Task DeleteBookingAsync(string id);
        Task ApproveBookingAsync(string id);
    }
}
