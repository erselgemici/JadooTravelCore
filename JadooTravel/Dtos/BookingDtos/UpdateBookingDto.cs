namespace JadooTravel.Dtos.BookingDtos
{
    public class UpdateBookingDto
    {
        public string BookingId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DestinationName { get; set; }
        public int PersonCount { get; set; }
        public DateTime BookingDate { get; set; }
        public string Note { get; set; }
        public bool IsApproved { get; set; }
    }
}
