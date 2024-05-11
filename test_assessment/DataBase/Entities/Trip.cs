namespace test_assessment.DataBase.Entities
{
    public class Trip
    {
        public int? TripID { get; set; }
        public DateTime PickupDatetime { get; set; }
        public DateTime DropoffDatetime { get; set; }
        public int PassangerCount { get; set; }
        public decimal TripDistance { get; set; }
        public string StoreAndFwdFlag { get; set; }
        public decimal FareAmount { get; set; }
        public decimal TipAmount { get; set; }

        public int PULocationID { get; set; }
        public Location PickupLocation { get; set; }

        public int DOLocationID { get; set; }
        public Location DropoffLocation { get; set; }
    }
}
