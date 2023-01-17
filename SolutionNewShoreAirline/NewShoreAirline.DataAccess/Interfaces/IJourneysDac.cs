using NewShoreAirline.Entities.Models;
using System.Data;

namespace NewShoreAirline.DataAccess.Interfaces
{
    public interface IJourneysDac
    {
        string InsertJourney(Journeys journey);
        string UpdateJourney(Journeys journey);
        string SearchJourney(string type_search, string value_search,
            out DataTable dtJourney);
    }
}
