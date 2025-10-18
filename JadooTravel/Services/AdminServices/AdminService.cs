using AutoMapper;
using JadooTravel.Dtos.AdminDtos;
using JadooTravel.Entities;
using JadooTravel.Helpers;
using JadooTravel.Settings;
using MongoDB.Driver;

namespace JadooTravel.Services.AdminServices
{
    public class AdminService : IAdminService
    {
        private readonly IMongoCollection<Admin> _adminCollection;
        private readonly IMapper _mapper;

        public AdminService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _adminCollection = database.GetCollection<Admin>(_databaseSettings.AdminCollectionName);
            _mapper = mapper;
        }

        public async Task<List<Admin>> GetAllAdminsAsync() =>
            await _adminCollection.Find(_ => true).ToListAsync();

        public async Task<Admin> GetByIdAsync(string id) =>
            await _adminCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Admin> LoginAsync(LoginAdminDto dto)
        {
            var admin = await _adminCollection.Find(x => x.Username == dto.Username).FirstOrDefaultAsync();
            if (admin == null)
                return null;

            bool isValid = PasswordHasher.VerifyPassword(dto.Password, admin.Password);

            return isValid ? admin : null;
        }

        public async Task CreateAdminAsync(CreateAdminDto dto)
        {
            var admin = new Admin
            {
                Username = dto.Username,
                Password = PasswordHasher.HashPassword(dto.Password)
            };
            await _adminCollection.InsertOneAsync(admin);
        }

        public async Task UpdateAdminAsync(UpdateAdminDto dto)
        {
            var update = Builders<Admin>.Update
                .Set(x => x.Username, dto.Username)
                .Set(x => x.Password, PasswordHasher.HashPassword(dto.Password));

            await _adminCollection.UpdateOneAsync(x => x.Id == dto.Id, update);
        }

        public async Task DeleteAdminAsync(string id)
        {
            await _adminCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}

