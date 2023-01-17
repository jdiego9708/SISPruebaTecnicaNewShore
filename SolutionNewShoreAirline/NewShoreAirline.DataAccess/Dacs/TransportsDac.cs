using System.Data.SqlClient;
using System.Data;
using NewShoreAirline.DataAccess.Interfaces;
using NewShoreAirline.Entities.Models;

namespace NewShoreAirline.DataAccess.Dacs
{
    public class TransportsDac : ITransportsDac
    {
        #region CONSTRUCTOR AND DEPENDENCY INJECTION
        private readonly IConexionDac Conexion;
        public TransportsDac(IConexionDac Conexion)
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

        #region METHOD INSERT TRANSPORT
        public string InsertTransport(Transports transport)
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
                    CommandText = "sp_Transports_i",
                    CommandType = CommandType.StoredProcedure
                };
                //Creamos cada parámetro y lo agregamos a la lista de parámetros del comando
                //El primer comando es el id del usuario que es parámetro de salida
                SqlParameter Id_transport = new()
                {
                    ParameterName = "Id_transport",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output,
                };
                SqlCmd.Parameters.Add(Id_transport);

                #region PARÁMETROS

                //Los parámetros varchar se les asigna una propiedad extra y es el Size
                SqlParameter FlightCarrier = new()
                {
                    ParameterName = "FlightCarrier",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = transport.FlightCarrier,
                };
                SqlCmd.Parameters.Add(FlightCarrier);

                SqlParameter FlightNumber = new()
                {
                    ParameterName = "FlightNumber",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = transport.FlightNumber,
                };
                SqlCmd.Parameters.Add(FlightNumber);

                #endregion

                //Ejecutamos nuestro comando cuando agreguemos todos los parámetros requeridos
                rpta = SqlCmd.ExecuteNonQuery() > 0 ? "OK" : "ERROR";

                //Comprobamos la variable de respuesta Mensaje_error que guarda el mensaje específico
                //De cualquier error generado en SQL procedimiento almacenado
                if (!rpta.Equals("OK"))
                    if (!string.IsNullOrEmpty(this.Error_message))
                        rpta = this.Error_message;
                //Obtenemos el id y lo asignamos a la propiedad existente para usarlo después
                transport.Id_transport = Convert.ToInt32(SqlCmd.Parameters["Id_transport"].Value);
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

        #region MÉTODO UPDATE TRANSPORT
        public string UpdateTransport(Transports transport)
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
                    CommandText = "sp_Transports_u",
                    CommandType = CommandType.StoredProcedure
                };
                //Creamos cada parámetro y lo agregamos a la lista de parámetros del comando
                //El primer comando es el id del usuario que es parámetro de salida
                SqlParameter Id_transport = new()
                {
                    ParameterName = "Id_transport",
                    SqlDbType = SqlDbType.Int,
                    Value = transport.Id_transport,
                };
                SqlCmd.Parameters.Add(Id_transport);

                #region PARÁMETROS

                //Los parámetros varchar se les asigna una propiedad extra y es el Size
                SqlParameter FlightCarrier = new()
                {
                    ParameterName = "FlightCarrier",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = transport.FlightCarrier,
                };
                SqlCmd.Parameters.Add(FlightCarrier);

                SqlParameter FlightNumber = new()
                {
                    ParameterName = "FlightNumber",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = transport.FlightNumber,
                };
                SqlCmd.Parameters.Add(FlightNumber);

                #endregion

                //Ejecutamos nuestro comando cuando agreguemos todos los parámetros requeridos
                rpta = SqlCmd.ExecuteNonQuery() > 0 ? "OK" : "ERROR";

                //Comprobamos la variable de respuesta Mensaje_error que guarda el mensaje específico
                //De cualquier error generado en SQL procedimiento almacenado
                if (!rpta.Equals("OK"))
                    if (!string.IsNullOrEmpty(this.Error_message))
                        rpta = this.Error_message;
                //Obtenemos el id y lo asignamos a la propiedad existente para usarlo después
                transport.Id_transport = Convert.ToInt32(SqlCmd.Parameters["Id_transport"].Value);
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

        #region SEARCH TRANSPORT
        public string SearchTransport(string type_search, string value_search,
            out DataTable dtTransport)
        {
            //Inicializamos la respuesta que vamos a devolver
            dtTransport = new();
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
                    CommandText = "sp_Transports_g",
                    CommandType = CommandType.StoredProcedure
                };
                //Creamos cada parámetro y lo agregamos a la lista de parámetros del comando
                //El primer comando es el id del usuario que es parámetro de salida
                SqlParameter Type_search = new()
                {
                    ParameterName = "Type_search",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = type_search
                };
                SqlCmd.Parameters.Add(Type_search);
                //Los parámetros varchar se les asigna una propiedad extra y es el Size
                SqlParameter Value_search = new()
                {
                    ParameterName = "Value_search",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = value_search,
                };
                SqlCmd.Parameters.Add(Value_search);

                //Ejecutamos nuestro comando cuando agreguemos todos los parámetros requeridos
                SqlDataAdapter SqlData = new(SqlCmd);
                SqlData.Fill(dtTransport);

                //Comprobamos la variable de respuesta Mensaje_error que guarda el mensaje específico
                //De cualquier error generado en SQL procedimiento almacenado
                if (dtTransport == null)
                {
                    if (!string.IsNullOrEmpty(this.Error_message))
                        rpta = this.Error_message;
                }
                else
                {
                    if (dtTransport.Rows.Count < 1)
                        dtTransport = null;
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                dtTransport = null;
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
