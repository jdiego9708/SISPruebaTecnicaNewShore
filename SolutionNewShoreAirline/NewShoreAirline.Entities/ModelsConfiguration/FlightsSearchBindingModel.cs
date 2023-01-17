namespace NewShoreAirline.Entities.ModelsConfiguration
{
    public class FlightsSearchBindingModel
    {
        public FlightsSearchBindingModel()
        {
            this.Origin = string.Empty;
            this.Destination = string.Empty;
        }
        public string Origin { get; set; }  
        public string Destination { get; set; }
    }
}
