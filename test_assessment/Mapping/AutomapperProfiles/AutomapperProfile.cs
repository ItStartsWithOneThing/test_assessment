
using AutoMapper;
using test_assessment.DataBase.Entities;
using test_assessment.Models;

namespace test_assessment.Mapping.AutomapperProfiles
{
    public static class AutomapperProfile
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TaxiTripModel, Trip>()
                            .ForMember(dest => dest.PickupDatetime, opt => opt.MapFrom(src => src.tpep_pickup_datetime))
                            .ForMember(dest => dest.DropoffDatetime, opt => opt.MapFrom(src => src.tpep_dropoff_datetime))
                            .ForMember(dest => dest.PassangerCount, opt => opt.MapFrom(src => src.passenger_count))
                            .ForMember(dest => dest.TripDistance, opt => opt.MapFrom(src => src.trip_distance))
                            .ForMember(dest => dest.StoreAndFwdFlag, opt => opt.MapFrom(src => src.store_and_fwd_flag))
                            .ForMember(dest => dest.PULocationID, opt => opt.MapFrom(src => src.PULocationID))
                            .ForMember(dest => dest.DOLocationID, opt => opt.MapFrom(src => src.DOLocationID))
                            .ForMember(dest => dest.FareAmount, opt => opt.MapFrom(src => src.fare_amount))
                            .ForMember(dest => dest.TipAmount, opt => opt.MapFrom(src => src.tip_amount));
            });

            return config.CreateMapper();
        }
    }
}
