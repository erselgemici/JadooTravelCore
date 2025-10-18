using AutoMapper;
using JadooTravel.Dtos.BookingDtos;
using JadooTravel.Entities;
using JadooTravel.Settings;
using MongoDB.Driver;

namespace JadooTravel.Services.BookingStepServices
{
    public class BookingService : IBookingService
    {
        private readonly IMongoCollection<Booking> _bookingCollection;
        private readonly IMapper _mapper;
        public BookingService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _bookingCollection = database.GetCollection<Booking>(_databaseSettings.BookingCollectionName);
            _mapper = mapper;
        }

        public async Task ApproveBookingAsync(string id)
        {
            var filter = Builders<Booking>.Filter.Eq(x => x.BookingId, id);
            var update = Builders<Booking>.Update.Set(x => x.IsApproved, true);
            await _bookingCollection.UpdateOneAsync(filter, update);
        }

        public async Task CreateBookingAsync(CreateBookingDto createBookingDto)
        {
            var value = _mapper.Map<Booking>(createBookingDto);
            await _bookingCollection.InsertOneAsync(value);
        }

        public async Task DeleteBookingAsync(string id)
        {
            await _bookingCollection.DeleteOneAsync(x => x.BookingId == id);
        }

        public async Task<List<ResultBookingDto>> GetAllBookingAsync()
        {
            var values = await _bookingCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultBookingDto>>(values);
        }

        //public Task<CreateBookingDto> GetBookingByIdAsync(string id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task UpdateBookingAsync(UpdateBookingDto dto)
        {
            var entity = _mapper.Map<Booking>(dto);
            await _bookingCollection.ReplaceOneAsync(x => x.BookingId == dto.BookingId, entity);
        }

       
    }
}
