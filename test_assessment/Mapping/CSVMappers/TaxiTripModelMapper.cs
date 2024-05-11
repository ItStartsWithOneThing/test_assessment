
using CsvHelper.Configuration;
using test_assessment.Models;

namespace test_assessment.Mapping.CSVMappers
{
    public class TaxiTripModelMapper : ClassMap<TaxiTripModel>
    {
        public TaxiTripModelMapper()
        {
            Map(x => x.VendorID).Convert(args =>
            {
                var vendorIDString = args.Row.GetField("VendorID");
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
        }
    }
}
