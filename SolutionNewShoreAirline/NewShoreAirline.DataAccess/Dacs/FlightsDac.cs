using System.Data.SqlClient;
using System.Data;
using NewShoreAirline.DataAccess.Interfaces;
using NewShoreAirline.Entities.Models;

namespace NewShoreAirline.DataAccess.Dacs
{
    public class FlightsDac : IFlightsDac
    {
        #region CONSTRUCTOR AND DEPENDENCY INJECTION
        private readonly IConexionDac Conexion;
        public FlightsDac(IConexionDac Conexion)
        {
            this.Error_message = string.Empty;

            this.Conexion = Conexion;
        }
        #endregion

        #region SQL ERROR MESSAGE
        private void SqlCon_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            string mensaje_error = e.Message;
            if (e.Errors != null)
            {
                if (e.Errors.Count > 0)
                {
                    mensaje_error += string.Join("|", e.Errors);
                }
            }
            this.Error_message = mensaje_error;
        }
        #endregion

        #region PROPERTIES
        public string Error_message { get; set; }
        #endregion

        #region METHOD INSERT FLIGHT
        public string InsertFlight(Flights flight)
        {
            //Inicializamos la respuesta que vamos a devolver
            string rpta = "OK";
            SqlConnection SqlCon = new();
            try
            {
                //Asignamos un evento SqlInfoMessage para obtener errores con severidad < 10 y > 11 desde SQL
                SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
                SqlCon.FireInfoMessageEventOnUserErrors = true;
                //Asignamos la cadena de conexión desde un método estático que lee el archivo de configuracion
                SqlCon.ConnectionString = Conexion.Cn();
                //Abrimos la conexión.
                SqlCon.Open();
                //Creamos un comando para ejecutar un procedimiento almacenado
                SqlCommand SqlCmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Flights_i",
                    CommandType = CommandType.StoredProcedure
                };
                //Creamos cada parámetro y lo agregamos a la lista de parámetros del comando
                //El primer comando es el id del usuario que es parámetro de salida
                SqlParameter Id_flight = new()
                {
                    ParameterName = "@Id_flight",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output,
                };
                SqlCmd.Parameters.Add(Id_flight);
                
                #region PARÁMETROS

                SqlParameter Id_transport = new()
                {
                    ParameterName = "@Id_transport",
                    SqlDbType = SqlDbType.Int,
                    Value = flight.Id_transport,
                };
                SqlCmd.Parameters.Add(Id_transport);

                //Los parámetros varchar se les asigna una propiedad extra y es el Size
                SqlParameter Origin = new()
                {
                    ParameterName = "@Origin",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = flight.Origin,
                };
                SqlCmd.Parameters.Add(Origin);

                SqlParameter Destination = new()
                {
                    ParameterName = "@Destination",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = flight.Destination,
                };
                SqlCmd.Parameters.Add(Destination);

                SqlParameter Price = new()
                {
                    ParameterName = "@Price",
                    SqlDbType = SqlDbType.Decimal,
                    Value = flight.Price,
                };
                SqlCmd.Parameters.Add(Price);
                #endregion

                //Ejecutamos nuestro comando cuando agreguemos todos los parámetros requeridos
                rpta = SqlCmd.ExecuteNonQuery() > 0 ? "OK" : "ERROR";

                //Comprobamos la variable de respuesta Mensaje_error que guarda el mensaje específico
                //De cualquier error generado en SQL procedimiento almacenado
                if (!rpta.Equals("OK"))
                    if (!string.IsNullOrEmpty(this.Error_message))
                        rpta = this.Error_message;
                //Obtenemos el id y lo asignamos a la propiedad existente para usarlo después
                flight.Id_flight = Convert.ToInt32(SqlCmd.Parameters["@Id_flight"].Value);
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return rpta;
        }
        #endregion

        #region MÉTODO UPDATE FLIGHT
        public string UpdateFlight(Flights flight)
        {
            //Inicializamos la respuesta que vamos a devolver
            string rpta = "OK";
            SqlConnection SqlCon = new();
            try
            {
                //Asignamos un evento SqlInfoMessage para obtener errores con severidad < 10 y > 11 desde SQL
                SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
                SqlCon.FireInfoMessageEventOnUserErrors = true;
                //Asignamos la cadena de conexión desde un método estático que lee el archivo de configuracion
                SqlCon.ConnectionString = Conexion.Cn();
                //Abrimos la conexión.
                SqlCon.Open();
                //Creamos un comando para ejecutar un procedimiento almacenado
                SqlCommand SqlCmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Flights_u",
                    CommandType = CommandType.StoredProcedure
                };
                //Creamos cada parámetro y lo agregamos a la lista de parámetros del comando
                //El primer comando es el id del usuario que es parámetro de salida
                SqlParameter Id_flight = new()
                {
                    ParameterName = "@Id_flight",
                    SqlDbType = SqlDbType.Int,
                    Value = flight.Id_flight,
                };
                SqlCmd.Parameters.Add(Id_flight);

                #region PARÁMETROS

                SqlParameter Id_transport = new()
                {
                    ParameterName = "@Id_transport",
                    SqlDbType = SqlDbType.Int,
                    Value = flight.Id_transport,
                };
                SqlCmd.Parameters.Add(Id_transport);

                //Los parámetros varchar se les asigna una propiedad extra y es el Size
                SqlParameter Origin = new()
                {
                    ParameterName = "@Origin",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = flight.Origin,
                };
                SqlCmd.Parameters.Add(Origin);

                SqlParameter Destination = new()
                {
                    ParameterName = "@Destination",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = flight.Destination,
                };
                SqlCmd.Parameters.Add(Destination);

                SqlParameter Price = new()
                {
                    ParameterName = "@Price",
                    SqlDbType = SqlDbType.Decimal,
                    Value = flight.Price,
                };
                SqlCmd.Parameters.Add(Price);
                #endregion

                //Ejecutamos nuestro comando cuando agreguemos todos los parámetros requeridos
                rpta = SqlCmd.ExecuteNonQuery() > 0 ? "OK" : "ERROR";

                //Comprobamos la variable de respuesta Mensaje_error que guarda el mensaje específico
                //De cualquier error generado en SQL procedimiento almacenado
                if (!rpta.Equals("OK"))
                    if (!string.IsNullOrEmpty(this.Error_message))
                        rpta = this.Error_message;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return rpta;
        }
        #endregion

        #region SEARCH FLIGHT
        public string SearchFlight(string type_search, string value_search,
            out DataTable dtFlight)
        {
            //Inicializamos la respuesta que vamos a devolver
            dtFlight = new();
            string rpta = "OK";
            SqlConnection SqlCon = new();
            try
            {
                //Asignamos un evento SqlInfoMessage para obtener errores con severidad < 10 desde SQL
                SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
                SqlCon.FireInfoMessageEventOnUserErrors = true;
                //Asignamos la cadena de conexión desde un método estático que lee el archivo de configuracion
                SqlCon.ConnectionString = Conexion.Cn();
                //Abrimos la conexión.
                SqlCon.Open();
                //Creamos un comando para ejecutar un procedimiento almacenado
                SqlCommand SqlCmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Flights_g",
                    CommandType = CommandType.StoredProcedure
                };
                //Creamos cada parámetro y lo agregamos a la lista de parámetros del comando
                //El primer comando es el id del usuario que es parámetro de salida
                SqlParameter Type_search = new()
                {
                    ParameterName = "@Type_search",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = type_search
                };
                SqlCmd.Parameters.Add(Type_search);
                //Los parámetros varchar se les asigna una propiedad extra y es el Size
                SqlParameter Value_search = new()
                {
                    ParameterName = "@Value_search",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = value_search,
                };
                SqlCmd.Parameters.Add(Value_search);

                //Ejecutamos nuestro comando cuando agreguemos todos los parámetros requeridos
                SqlDataAdapter SqlData = new(SqlCmd);
                SqlData.Fill(dtFlight);

                //Comprobamos la variable de respuesta Mensaje_error que guarda el mensaje específico
                //De cualquier error generado en SQL procedimiento almacenado
                if (dtFlight == null)
                {
                    if (!string.IsNullOrEmpty(this.Error_message))
                        rpta = this.Error_message;
                }
                else
                {
                    if (dtFlight.Rows.Count < 1)
                        dtFlight = null;
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                dtFlight = null;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return rpta;
        }
        #endregion

        #region METHOD INSERT DETAIL FLIGHT
        public string InsertDetailFlight(Details_journeys detail)
        {
            //Inicializamos la respuesta que vamos a devolver
            string rpta = "OK";
            SqlConnection SqlCon = new();
            try
            {
                //Asignamos un evento SqlInfoMessage para obtener errores con severidad < 10 y > 11 desde SQL
                SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
                SqlCon.FireInfoMessageEventOnUserErrors = true;
                //Asignamos la cadena de conexión desde un método estático que lee el archivo de configuracion
                SqlCon.ConnectionString = Conexion.Cn();
                //Abrimos la conexión.
                SqlCon.Open();
                //Creamos un comando para ejecutar un procedimiento almacenado
                SqlCommand SqlCmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Details_journeys_i",
                    CommandType = CommandType.StoredProcedure
                };
                //Creamos cada parámetro y lo agregamos a la lista de parámetros del comando
                //El primer comando es el id del usuario que es parámetro de salida
                SqlParameter Id_flight = new()
                {
                    ParameterName = "@Id_flight",
                    SqlDbType = SqlDbType.Int,
                    Value = detail.Id_flight,
                };
                SqlCmd.Parameters.Add(Id_flight);

                #region PARÁMETROS

                SqlParameter Id_journey = new()
                {
                    ParameterName = "@Id_journey",
                    SqlDbType = SqlDbType.Int,
                    Value = detail.Id_journey,
                };
                SqlCmd.Parameters.Add(Id_journey);
               
                #endregion

                //Ejecutamos nuestro comando cuando agreguemos todos los parámetros requeridos
                rpta = SqlCmd.ExecuteNonQuery() > 0 ? "OK" : "ERROR";

                //Comprobamos la variable de respuesta Mensaje_error que guarda el mensaje específico
                //De cualquier error generado en SQL procedimiento almacenado
                if (!rpta.Equals("OK"))
                    if (!string.IsNullOrEmpty(this.Error_message))
                        rpta = this.Error_message;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return rpta;
        }
        #endregion
    }
}
