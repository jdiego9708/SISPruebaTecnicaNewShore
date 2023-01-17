using NewShoreAirline.Entities.ModelsConfiguration;
using System.Data;

namespace NewShoreAirline.Entities.Models
{
    public class Flights
    {
        #region CONSTUCTORS
        public Flights()
        {
            this.Origin = string.Empty;
            this.Destination = string.Empty;

            this.Transport = new();
        }
        public Flights(DataRow row)
        {
            this.Id_flight = ConvertValueHelper.ConvertIntValue(row["Id_flight"]);
            this.Id_transport = ConvertValueHelper.ConvertIntValue(row["Id_transport"]);
            this.Origin = ConvertValueHelper.ConvertStringValue(row["Origin"]);
            this.Destination = ConvertValueHelper.ConvertStringValue(row["Destination"]);
            this.Price = ConvertValueHelper.ConvertDecimalValue(row["Price"]);

            this.Transport = new();
        }
        #endregion

        #region PROPERTIES
        public int Id_flight { get; set; }
        public int Id_transport { get; set; }
        public Transports Transport { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
        #endregion
    }
}
