using NewShoreAirline.Entities.ModelsConfiguration;
using System.Data;

namespace NewShoreAirline.Entities.Models
{
    public class Cities
    {
        #region CONSTRUCTORS
        public Cities()
        {
            this.Name_city = string.Empty;
        }
        public Cities(DataRow row)
        {
            this.Id_city = ConvertValueHelper.ConvertIntValue(row["Id_city"]);
            this.Name_city = ConvertValueHelper.ConvertStringValue(row["Name_city"]);
        }
        #endregion

        #region PROPERTIES
        public int Id_city { get; set; }
        public string Name_city { get; set; }
        #endregion
    }
}
