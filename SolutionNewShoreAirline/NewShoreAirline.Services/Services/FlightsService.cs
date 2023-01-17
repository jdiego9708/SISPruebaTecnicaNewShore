using NewShoreAirline.DataAccess.Interfaces;
using NewShoreAirline.Entities.Models;
using NewShoreAirline.Entities.ModelsConfiguration;
using Newtonsoft.Json;
using System.Data;

namespace NewShoreAirline.Services.Services
{
    public class FlightsService
    {
        public IFlightsDac IFlightsDac { get; set; }
        public FlightsService(IFlightsDac IFlightsDac)
        {
            this.IFlightsDac = IFlightsDac;
        }
        private static bool ValidationInsert(Flights flight, out string rpta)
        {
            rpta = "OK";
            try
            {
                if (flight.Id_transport == 0)
                    throw new Exception("Verify Id_transport");

                if (string.IsNullOrEmpty(flight.Origin))
                    throw new Exception("Verify Origin");

                if (string.IsNullOrEmpty(flight.Destination))
                    throw new Exception("Verify Destination");

                if (flight.Price == 0)
                    throw new Exception("Verify Price");

                return true;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                return false;
            }
        }
        public RestResponseModel InsertFlight(Flights flight)
        {
            RestResponseModel response = new();
            try
            {
                if (!ValidationInsert(flight, out string rpta))
                    throw new Exception(rpta);

                rpta = this.IFlightsDac.InsertFlight(flight);

                if (!rpta.Equals("OK"))
                    throw new Exception($"Error inserting flight | {rpta}");

                response.IsSucess = true;
                response.Response = JsonConvert.SerializeObject(flight);
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Response = ex.Message;
            }
            return response;
        }
        public RestResponseModel SearchFlights(SearchBindingModel search)
        {
            RestResponseModel response = new();
            try
            {
                if (string.IsNullOrEmpty(search.Type_search))
                    throw new Exception("Verify Tipo_busqueda");

                if (string.IsNullOrEmpty(search.Value_search))
                    throw new Exception("Verify Texto_busqueda");

                string rpta =
                    this.IFlightsDac.SearchFlight(search.Type_search,
                    search.Value_search, out DataTable dtFlight);

                List<Flights> flights = new();

                if (dtFlight == null)
                {
                    if (rpta.Equals("OK"))
                    {
                        flights.Add(new Flights()
                        {
                            Origin = "Find empty"
                        });

                        response.IsSucess = true;
                        response.Response = JsonConvert.SerializeObject(flights);
                        return response;
                    }
                    else
                        throw new Exception($"Error | {rpta}");
                }

                flights = (from DataRow row in dtFlight.Rows
                           select new Flights(row)).ToList();

                response.IsSucess = true;
                response.Response = JsonConvert.SerializeObject(flights);
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Response = ex.Message;
            }
            return response;
        }
        public RestResponseModel VerifyFlights(FlightsSearchBindingModel search)
        {
            RestResponseModel response = new();
            try
            {
                if (string.IsNullOrEmpty(search.Origin))
                    throw new Exception("Verify Origin");

                if (string.IsNullOrEmpty(search.Destination))
                    throw new Exception("Verify Destination");

                //Búsqueda del primer Origen
                string rpta =
                    this.IFlightsDac.SearchFlight("ORIGIN",
                    search.Origin, out DataTable dtFlightsOrigin);

                List<Flights> flightsOrigin = new();

                if (dtFlightsOrigin == null)
                    throw new Exception($"Not origin Flights Origin | {rpta}");

                flightsOrigin = (from DataRow row in dtFlightsOrigin.Rows
                           select new Flights(row)).ToList();

                //Búsqueda del destino
                rpta =
                   this.IFlightsDac.SearchFlight("DESTINATION",
                   search.Origin, out DataTable dtFlightsDestination);

                List<Flights> flightsDestination = new();

                if (dtFlightsDestination == null)
                    throw new Exception($"Not origin Flights Destination | {rpta}");

                flightsDestination = (from DataRow row in dtFlightsOrigin.Rows
                                 select new Flights(row)).ToList();

                foreach(Flights flight in flightsDestination)
                {

                }



                response.IsSucess = true;
                response.Response = JsonConvert.SerializeObject(flights);
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Response = ex.Message;
            }
            return response;
        }
    }
}
