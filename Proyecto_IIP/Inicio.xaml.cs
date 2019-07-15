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

using Capa_Negocio;
using System.Data;
namespace Proyecto_IIP
{
    /// <summary>
    /// Interaction logic for Inicio.xaml
    /// </summary>
    public partial class Inicio : UserControl
    {
        string rpta = "";
        public Inicio()
        {
            InitializeComponent();
            cbTipoVehiculo.DisplayMemberPath = "Nombre";
            cbTipoVehiculo.SelectedValuePath = "Id_Tipo";
            cbTipoVehiculo.ItemsSource = NVehiculo.MostrarTipo().DefaultView;
        }

        private void BtnIngresarVehiculo_Click(object sender, RoutedEventArgs e)
        {
   
            if (TxtPlaca.Text != "" && cbTipoVehiculo.SelectedItem != null)
            {
                DataTable dt = NVehiculo.VerificarVehiculo(TxtPlaca.Text);
                if (dt.Rows.Count < 1)
                {
                    rpta = NVehiculo.IngresoVehiculo(TxtPlaca.Text, Convert.ToInt32(cbTipoVehiculo.SelectedValue));
                    MessageBox.Show(rpta);
                }
                else
                {
                    MessageBox.Show("El vehiculo ya esta en el estacionamiento");
                }
            }
            else
            {
                MessageBox.Show("Seleccione o escriba un valor.");
            }
            TxtPlaca.Clear();
        }
    }
}
