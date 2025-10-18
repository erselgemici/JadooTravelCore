using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JadooTravel.Entities
{
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BookingId { get; set; }

        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DestinationName { get; set; }
        public int PersonCount { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public string Note { get; set; }
        public bool IsApproved { get; set; } = false;
    }

}
