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

using System.Data;
using Capa_Negocio;

namespace Proyecto_IIP
{
    /// <summary>
    /// Interaction logic for Salida.xaml
    /// </summary>
    public partial class Salida : UserControl
    {
        private Factura factura;
        public Salida()
        {
            InitializeComponent();
            factura = new Factura();
        }

        private void BtnSalidaVehiculo_Click(object sender, RoutedEventArgs e)
        {
            if (TxtPlaca.Text != "")
            {
                DataTable dt = new DataTable();
                dt = NVehiculo.SalidaVehiculo(TxtPlaca.Text);
                DataRow dr = dt.Rows[0];

                factura.TxtTiempo.Text = dr[0].ToString() + " horas con " + dr[1].ToString() + " minutos.";
                factura.TxtTotal.Text = dr[2].ToString();

                factura.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ingrese el numero de placa. ");
            }
            TxtPlaca.Clear();
        }
    }

}
