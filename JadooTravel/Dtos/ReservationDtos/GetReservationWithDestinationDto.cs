namespace JadooTravel.Dtos.ReservationDtos
{
    public class GetReservationWithDestinationDto
    {
        public string ReservationId { get; set; }
        public string NameSurname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string DestinationId { get; set; }

        // Tur (Destination) bilgileri
        public string CityCountry { get; set; }
        public decimal Price { get; set; }
        public string DayNight { get; set; }
        public int Capacity { get; set; }
    }
}
