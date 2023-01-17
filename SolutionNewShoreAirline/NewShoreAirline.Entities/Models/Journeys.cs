using NewShoreAirline.Entities.ModelsConfiguration;
using System.Data;

namespace NewShoreAirline.Entities.Models
{
    public class Journeys
    {
        #region CONSTRUCTORS
        public Journeys()
        {
            this.Origin = string.Empty;
            this.Destination = string.Empty;
            this.Flights = new();
        }
        public Journeys(DataRow row)
        {
            this.Id_journey = ConvertValueHelper.ConvertIntValue(row["Id_journey"]);
            this.Origin = ConvertValueHelper.ConvertStringValue(row["Origin"]);
            this.Destination = ConvertValueHelper.ConvertStringValue(row["Destination"]);
            this.Price = ConvertValueHelper.ConvertDecimalValue(row["Price"]);
            this.Flights = new();
        }
        #endregion

        #region PROPERTIES
        public int Id_journey { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }

        public List<Flights> Flights { get; set; }
        #endregion
    }
}
