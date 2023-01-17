using NewShoreAirline.Entities.Models;
using System.Data;

namespace NewShoreAirline.DataAccess.Interfaces
{
    public interface ITransportsDac
    {
        string InsertTransport(Transports transport);
        string UpdateTransport(Transports transport);
        string SearchTransport(string type_search, string value_search,
            out DataTable dtTransport);
    }
}
