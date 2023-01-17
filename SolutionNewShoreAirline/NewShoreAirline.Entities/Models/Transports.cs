using NewShoreAirline.Entities.ModelsConfiguration;
using System.Data;

namespace NewShoreAirline.Entities.Models
{
    public class Transports
    {
        #region CONSTRUCTORS
        public Transports()
        {
            this.FlightCarrier = string.Empty;
            this.FlightNumber = string.Empty;
        }
        public Transports(DataRow row)
        {
            this.Id_transport = ConvertValueHelper.ConvertIntValue(row["Id_journey"]);
            this.FlightCarrier = ConvertValueHelper.ConvertStringValue(row["Origin"]);
            this.FlightNumber = ConvertValueHelper.ConvertStringValue(row["Destination"]);
        }
        #endregion

        #region PROPERTIES
        public int Id_transport { get; set; }
        public string FlightCarrier { get; set; }
        public string FlightNumber { get; set; }
        #endregion
    }
}
