using NewShoreAirline.DataAccess.Interfaces;
using NewShoreAirline.Entidades.ModelosConfiguracion;
using Microsoft.Extensions.Configuration;

namespace NewShoreAirline.DataAccess.Dacs
{
    public class ConexionDac : IConexionDac
    {
        private readonly IConfiguration Configuration;
        private readonly ConnectionStrings ConnectionStringsModel;
        public ConexionDac(IConfiguration IConfiguration)
        {
            this.Configuration = IConfiguration;

            var settings = this.Configuration.GetSection("ConnectionStrings");
            this.ConnectionStringsModel = settings.Get<ConnectionStrings>();

            if (this.ConnectionStringsModel == null)
                this.ConnectionStringsModel = new();
        }
        public string Cn()
        {
            if (this.ConnectionStringsModel.ConexionBDPredeterminada.Equals("ConexionBDAzure"))
                return this.ConnectionStringsModel.ConexionBDAzure;
            else
                return this.ConnectionStringsModel.ConexionBDLocal;
        }
    }
}
