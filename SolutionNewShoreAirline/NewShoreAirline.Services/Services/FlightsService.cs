using NewShoreAirline.DataAccess.Interfaces;
using NewShoreAirline.Entities.Models;
using NewShoreAirline.Entities.ModelsConfiguration;
using NewShoreAirline.Services.Interfaces;
using Newtonsoft.Json;
using System.Data;

namespace NewShoreAirline.Services.Services
{
    public class FlightsService : IFlightService
    {
        #region CONSTRUCTOR AND DEPENDENCY INYECTION
        public IFlightsDac IFlightsDac { get; set; }
        public IJourneysDac IJourneysDac { get; set; }
        public FlightsService(IFlightsDac IFlightsDac,
            IJourneysDac IJourneysDac)
        {
            this.IFlightsDac = IFlightsDac;
            this.IJourneysDac = IJourneysDac;
        }
        #endregion

        #region MÉTODOS
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

                string rpta =
                   this.IJourneysDac.SearchJourney("ORIGIN",
                   search.Origin, out DataTable dtJourney);

                if (dtJourney != null)
                {
                    List<Journeys> journeysOrigin = new();
                    journeysOrigin = (from DataRow row in dtJourney.Rows
                                      select new Journeys(row)).ToList();

                    Journeys journeyDefault =
                    journeysOrigin.Where(x => x.Destination == search.Destination).FirstOrDefault();

                    if (journeyDefault != null)
                    {
                        rpta =
                          this.IJourneysDac.SearchJourney("FLIGHTS JOURNEY",
                          journeyDefault.Id_journey.ToString(), out dtJourney);

                        if (dtJourney != null)
                        {
                            List<Flights> flightsJourney = (from DataRow row in dtJourney.Rows
                                                            select new Flights(row)).ToList();

                            journeyDefault.Flights = flightsJourney;
                            response.IsSucess = true;
                            response.Response = JsonConvert.SerializeObject(new { Journey = journeyDefault });
                            return response;
                        }
                    }
                }

                Journeys journey = new()
                {
                    Origin = search.Origin,
                    Destination = search.Destination,
                    Flights = new()
                };

                //Búsqueda del primer Origen
                rpta =
                    this.IFlightsDac.SearchFlight("ORIGIN",
                    search.Origin, out DataTable dtFlightsOrigin);

                List<Flights> flightsOrigin = new();

                if (dtFlightsOrigin == null)
                    throw new Exception($"Not Flights Origin | {rpta}");

                flightsOrigin = (from DataRow row in dtFlightsOrigin.Rows
                                 select new Flights(row)).ToList();

                //Encontrar el vuelo con el destino correspondiente
                Flights flightDefault =
                    flightsOrigin.Where(x => x.Destination == search.Destination).FirstOrDefault();

                //Si el vuelo no existe vamos a calcular uno nuevo
                if (flightDefault == null)
                {
                    foreach (Flights fl in flightsOrigin)
                    {
                        rpta =
                          this.IFlightsDac.SearchFlight("ORIGIN",
                          fl.Destination, out DataTable dtFlightsDestination);

                        if (dtFlightsDestination == null)
                            continue;

                        List<Flights> flightsDestination = new();

                        flightsDestination = (from DataRow row in dtFlightsDestination.Rows
                                              select new Flights(row)).ToList();

                        Flights flightDefaultDestination =
                            flightsDestination.Where(x => x.Destination == search.Destination).FirstOrDefault();

                        if (flightDefaultDestination == null)
                            continue;

                        journey.Flights.Add(fl);
                        journey.Flights.Add(flightDefaultDestination);
                        break;
                    }

                    journey.Price = journey.Flights.Sum(x => x.Price);
                    Task.Run(() => this.SaveJourneyAndFlights(journey));
                }
                else
                {
                    journey.Flights.Add(flightDefault);
                    journey.Price = journey.Flights.Sum(x => x.Price);
                }

                response.IsSucess = true;
                response.Response = JsonConvert.SerializeObject(new { Journey = journey });
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Response = ex.Message;
            }
            return response;
        }

        private void SaveJourneyAndFlights(Journeys journey)
        {
            try
            {
                string rpta = this.IJourneysDac.InsertJourney(journey);

                if (!rpta.Equals("OK"))
                    throw new Exception("Error save the journey");

                foreach (Flights fl in journey.Flights)
                {
                    rpta = this.IFlightsDac.InsertFlight(fl);

                    if (!rpta.Equals("OK"))
                        throw new Exception("Error save the flight");

                    Details_journeys detail = new()
                    {
                        Id_journey = journey.Id_journey,
                        Id_flight = fl.Id_flight,
                    };

                    rpta = this.IFlightsDac.InsertDetailFlight(detail);
                    if (!rpta.Equals("OK"))
                        throw new Exception("Error save the detail flight");
                }
            }
            catch (Exception)
            {
                //Manejar errores
            }
        }

        #endregion
    }
}
