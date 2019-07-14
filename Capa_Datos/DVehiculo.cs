using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data; //Namespace para utilizar tableadapter y datatable
using System.Data.SqlClient; //Namespace para utilizar cliente sql

namespace Capa_Datos
{
    public class DVehiculo
    {
        private int _IdVehiculo;
        private int _IdTipovehiculo;
        private string _Placa;
        private string _TextoBuscar;

        //Creamos nuestra conexion SQL
        SqlConnection SqlCon = new SqlConnection(Conexion.Cn); 
        public DVehiculo()
        {
            
        }

        public int IdVehiculo
        {
            get { return _IdVehiculo; }
            set { _IdVehiculo = value; }
        }
        public int IdTipovehiculo
        {
            get { return _IdTipovehiculo; }
            set { _IdTipovehiculo = value; }
        }
        public string Placa
        {
            get { return _Placa; }
            set { _Placa = value; }
        }
        public string TextoBuscar
        {
            get { return _TextoBuscar; }
            set { _TextoBuscar = value; }
        }

        public DataTable MostrarVehiculo()
        {
            DataTable TablaVehiculo = new DataTable();
            try
            {
                SqlCommand SqlCmd = new SqlCommand("Vehiculo.SP_MostrarVehiculo", SqlCon);
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlData = new SqlDataAdapter(SqlCmd);
                using (sqlData)
                {
                    sqlData.Fill(TablaVehiculo);
                }
            }
            catch (Exception)
            {
                TablaVehiculo = null;
            }
            return TablaVehiculo;
        }
        public DataTable MostrarTipo()
        {
            DataTable TablaTipoVehiculo = new DataTable();
            try
            {
                SqlCommand SqlCmd = new SqlCommand("Vehiculo.SP_MostrarTipo", SqlCon);
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlData = new SqlDataAdapter(SqlCmd);
                using (sqlData)
                {
                    sqlData.Fill(TablaTipoVehiculo);
                }
            }
            catch (Exception)
            {
                TablaTipoVehiculo = null;
            }
            return TablaTipoVehiculo;
        }

        public string IngresarVehiculo(DVehiculo vehiculo)
        {
            string rpta="";
            SqlCommand SqlCmd = new SqlCommand("Vehiculo.SP_IngresoVehiculo",SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                SqlCon.Open();
                SqlCmd.Parameters.Add(new SqlParameter("Placa", SqlDbType.NVarChar));
                SqlCmd.Parameters["Placa"].Value = vehiculo.Placa;
                SqlCmd.Parameters.Add(new SqlParameter("Tipo", SqlDbType.Int));
                SqlCmd.Parameters["Tipo"].Value = vehiculo.IdTipovehiculo;
                SqlCmd.ExecuteNonQuery();
                rpta = "Ingreso Realizado Correctamente";
            }
            catch(Exception e)
            {
                rpta = e.ToString();
            }
            finally
            {
                SqlCon.Close();
            }
            return rpta;
        }
    }
}
