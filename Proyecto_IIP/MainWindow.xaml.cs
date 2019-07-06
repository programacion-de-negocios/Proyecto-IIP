using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//USANDO LOS NAMESPACES NECESARIOS
using System.Data;
using System.Data.SqlClient;

namespace Proyecto_IIP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection sqlConnection;
        public MainWindow()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(Conexion.Cn);
            Mostrar_TipoVehiculo();
        }

        private void Mostrar_TipoVehiculo()
        {
            try
            {
                //DEFINIMOS NUESTRO QUERY
                string query = "SELECT * FROM Vehiculo.Tipo_Vehiculo";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
                //LLENAMOS LOS DATOS DEL DATA ADAPTER POR MEDIO DE UN DATATABLE
                using (sqlDataAdapter)
                {
                    DataTable Tabla_TipoVehiculo = new DataTable();
                    sqlDataAdapter.Fill(Tabla_TipoVehiculo);
                    LbTipo_Vehiculo.DisplayMemberPath = "Nombre";
                    LbTipo_Vehiculo.SelectedValuePath = "Id_Tipo";
                    LbTipo_Vehiculo.ItemsSource = Tabla_TipoVehiculo.DefaultView;
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        private void Mostrar_Vehiculo()
        {
            try
            {
                //DEFINIMOS EL QUERY
                string query = @"SELECT * FROM Vehiculo.Vehiculo a INNER JOIN Vehiculo.Tipo_Vehiculo b
                                ON a.Id_Tipo = b.Id_Tipo WHERE b.Id_Tipo=@idtipo";
                SqlCommand SqlCmd = new SqlCommand(query, sqlConnection);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SqlCmd);
                using (sqlDataAdapter)
                {
                    SqlCmd.Parameters.AddWithValue("@idtipo", LbTipo_Vehiculo.SelectedValue);
                    //DEFINIMOS EL DATABLE
                    DataTable TablaVehiculo = new DataTable();
                    sqlDataAdapter.Fill(TablaVehiculo);
                    LbVehiculo.DisplayMemberPath = "Placa";
                    LbVehiculo.SelectedValuePath = "Id_Vehiculo";
                    LbVehiculo.ItemsSource = TablaVehiculo.DefaultView;
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }
        }
        //EVENTO PARA LLAMAR AL METODO Mostrar_Vehiculo
        private void LbTipo_Vehiculo_SelectionChanged(object sender,SelectionChangedEventArgs e)
        {
            Mostrar_Vehiculo();
        }
    }
}
