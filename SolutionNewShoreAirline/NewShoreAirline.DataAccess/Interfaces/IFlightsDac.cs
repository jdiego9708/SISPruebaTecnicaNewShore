﻿using NewShoreAirline.Entities.Models;
using System.Data;

namespace NewShoreAirline.DataAccess.Interfaces
{
    public interface IFlightsDac
    {
        string InsertFlight(Flights flight);
        string UpdateFlight(Flights flight);
        string SearchFlight(string type_search, string value_search,
            out DataTable dtFlight);
    }
}
