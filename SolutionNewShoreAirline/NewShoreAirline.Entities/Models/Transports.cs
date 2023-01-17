using NewShoreAirline.Entities.ModelsConfiguration;
using System.Data;
using System.Text.Json.Serialization;

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
            this.Id_transport = ConvertValueHelper.ConvertIntValue(row["Id_transport"]);
            this.FlightCarrier = ConvertValueHelper.ConvertStringValue(row["FlightCarrier"]);
            this.FlightNumber = ConvertValueHelper.ConvertStringValue(row["FlightNumber"]);
        }
        #endregion

        #region PROPERTIES
        [JsonIgnore]
        public int Id_transport { get; set; }
        public string FlightCarrier { get; set; }
        public string FlightNumber { get; set; }
        #endregion
    }
}
