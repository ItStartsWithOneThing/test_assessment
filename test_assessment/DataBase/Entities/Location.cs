
namespace test_assessment.DataBase.Entities
{
    public record class Location
    {
        public int LocationID { get; set; }

        public ICollection<Trip> TripsAsPickupLocation { get; set; }

        public ICollection<Trip> TripsAsDropoffLocation { get; set; }
    }
}
