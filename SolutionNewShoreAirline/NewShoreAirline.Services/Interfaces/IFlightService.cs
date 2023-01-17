using NewShoreAirline.Entities.Models;
using NewShoreAirline.Entities.ModelsConfiguration;

namespace NewShoreAirline.Services.Interfaces
{
    public interface IFlightService
    {
        RestResponseModel InsertFlight(Flights flight);
        RestResponseModel SearchFlights(SearchBindingModel search);
        RestResponseModel VerifyFlights(FlightsSearchBindingModel search);
    }
}
