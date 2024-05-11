
using CsvHelper.Configuration;
using System.Globalization;
using test_assessment.Models;

namespace test_assessment.Mapping.CSVMappers
{
    public class TaxiTripModelMapper : ClassMap<TaxiTripModel>
    {
        public TaxiTripModelMapper()
        {
            Map(x => x.VendorID).Convert(args =>
            {
                var vendorIDString = args.Row.GetField("VendorID").Trim();
                if (String.IsNullOrEmpty(vendorIDString))
                {
                    return 0;
                }
                else
                {
                    int.TryParse(vendorIDString, out int value);
                    return value;
                }
            });

            Map(m => m.tpep_pickup_datetime).TypeConverterOption.DateTimeStyles(DateTimeStyles.None);
            Map(m => m.tpep_dropoff_datetime).TypeConverterOption.DateTimeStyles(DateTimeStyles.None);

            Map(m => m.passenger_count).Convert(args =>
            {
                var valueString = args.Row.GetField("passenger_count").Trim();

                if (String.IsNullOrEmpty(valueString))
                {
                    return 0;
                }
                else
                {
                    int.TryParse(valueString, out int value);
                    return value;
                }
            });

            Map(m => m.trip_distance).Convert(args =>
            {
                var valueString = args.Row.GetField("trip_distance").Trim();

                if (String.IsNullOrEmpty(valueString))
                {
                    return 0;
                }
                else
                {
                    decimal.TryParse(valueString.Replace('.', ','), out decimal value);
                    return value;
                }
            });

            Map(m => m.RatecodeID).Convert(args =>
            {
                var valueString = args.Row.GetField("RatecodeID").Trim();

                if (String.IsNullOrEmpty(valueString))
                {
                    return 0;
                }
                else
                {
                    int.TryParse(valueString, out int value);
                    return value;
                }
            });

            Map(x => x.store_and_fwd_flag).Convert(args =>
            {
                var value = args.Row.GetField("store_and_fwd_flag").Trim();

                switch (value)
                {
                    case "N": return "Нет"; break;
                    case "Y": return "Да"; break;
                    default: return "Нет"; break;
                }
            });

            Map(m => m.PULocationID).Convert(args =>
            {
                var valueString = args.Row.GetField("PULocationID").Trim();

                if (String.IsNullOrEmpty(valueString))
                {
                    return 0;
                }
                else
                {
                    int.TryParse(valueString, out int value);
                    return value;
                }
            });

            Map(m => m.DOLocationID).Convert(args =>
            {
                var valueString = args.Row.GetField("DOLocationID").Trim();

                if (String.IsNullOrEmpty(valueString))
                {
                    return 0;
                }
                else
                {
                    int.TryParse(valueString, out int value);
                    return value;
                }
            });

            Map(m => m.payment_type).Convert(args =>
            {
                var valueString = args.Row.GetField("payment_type").Trim();

                if (String.IsNullOrEmpty(valueString))
                {
                    return 0;
                }
                else
                {
                    int.TryParse(valueString, out int value);
                    return value;
                }
            });

            Map(m => m.fare_amount).Convert(args =>
            {
                var valueString = args.Row.GetField("fare_amount").Trim();

                if (String.IsNullOrEmpty(valueString))
                {
                    return 0;
                }
                else
                {
                    decimal.TryParse(valueString.Replace('.', ','), out decimal value);
                    return value;
                }
            });

            Map(m => m.extra).Convert(args =>
            {
                var valueString = args.Row.GetField("extra").Trim();

                if (String.IsNullOrEmpty(valueString))
                {
                    return 0;
                }
                else
                {
                    decimal.TryParse(valueString.Replace('.', ','), out decimal value);
                    return value;
                }
            });

            Map(m => m.mta_tax).Convert(args =>
            {
                var valueString = args.Row.GetField("mta_tax").Trim();

                if (String.IsNullOrEmpty(valueString))
                {
                    return 0;
                }
                else
                {
                    decimal.TryParse(valueString.Replace('.', ','), out decimal value);
                    return value;
                }
            });

            Map(m => m.tip_amount).Convert(args =>
            {
                var valueString = args.Row.GetField("tip_amount").Trim();

                if (String.IsNullOrEmpty(valueString))
                {
                    return 0;
                }
                else
                {
                    decimal.TryParse(valueString.Replace('.', ','), out decimal value);
                    return value;
                }
            });

            Map(m => m.tolls_amount).Convert(args =>
            {
                var valueString = args.Row.GetField("tolls_amount").Trim();

                if (String.IsNullOrEmpty(valueString))
                {
                    return 0;
                }
                else
                {
                    decimal.TryParse(valueString.Replace('.', ','), out decimal value);
                    return value;
                }
            });

            Map(m => m.improvement_surcharge).Convert(args =>
            {
                var valueString = args.Row.GetField("improvement_surcharge").Trim();

                if (String.IsNullOrEmpty(valueString))
                {
                    return 0;
                }
                else
                {
                    decimal.TryParse(valueString.Replace('.', ','), out decimal value);
                    return value;
                }
            });

            Map(m => m.total_amount).Convert(args =>
            {
                var valueString = args.Row.GetField("total_amount").Trim();

                if (String.IsNullOrEmpty(valueString))
                {
                    return 0;
                }
                else
                {
                    decimal.TryParse(valueString.Replace('.', ','), out decimal value);
                    return value;
                }
            });

            Map(m => m.congestion_surcharge).Convert(args =>
            {
                var valueString = args.Row.GetField("congestion_surcharge").Trim();

                if (String.IsNullOrEmpty(valueString))
                {
                    return 0;
                }
                else
                {
                    decimal.TryParse(valueString.Replace('.', ','), out decimal value);
                    return value;
                }
            });
        }
    }
}
