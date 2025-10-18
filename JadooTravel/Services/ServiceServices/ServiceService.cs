using AutoMapper;
using Humanizer;
using JadooTravel.Dtos.ServiceDtos;
using JadooTravel.Entities;
using JadooTravel.Settings;
using MongoDB.Driver;

namespace JadooTravel.Services.ServiceServices
{
    public class ServiceService : IServiceService
    {
        private readonly IMongoCollection<Service> _servicesCollection;
        private readonly IMapper _mapper;
        public ServiceService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _servicesCollection = database.GetCollection<Service>(_databaseSettings.ServiceCollectionName);
            _mapper = mapper;
        }
        public async Task CreateServiceAsync(CreateServiceDto createServiceDto)
        {
            var value = _mapper.Map<Service>(createServiceDto);
            await _servicesCollection.InsertOneAsync(value);
        }

        public Task DeleteServiceAsync(string id)
        {
            return _servicesCollection.DeleteOneAsync(x => x.ServiceId == id);
        }

        public async Task<List<ResultServiceDto>> GetAllServiceAsync()
        {
            var values = await _servicesCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultServiceDto>>(values);
        }

        public async Task<GetServiceByIdDto> GetServiceByIdAsync(string id)
        {
            var value = await _servicesCollection.Find(x => x.ServiceId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetServiceByIdDto>(value);
        }

        public async Task UpdateServiceAsync(UpdateServiceDto updateServiceDto)
        {
            var value = _mapper.Map<Service>(updateServiceDto);
            await _servicesCollection.FindOneAndReplaceAsync(x => x.ServiceId == updateServiceDto.ServiceId, value);
        }
    }
}
