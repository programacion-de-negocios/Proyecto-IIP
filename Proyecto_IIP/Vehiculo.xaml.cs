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
using System.Windows.Shapes;

using Capa_Negocio;//Namespace para acceder a la capa negocio
using System.Data;//Namespace para utilizar el datatable

namespace Proyecto_IIP
{
    /// <summary>
    /// Interaction logic for Vehiculo.xaml
    /// </summary>
    public partial class Vehiculo : Window
    {
        public Vehiculo()
        {
            InitializeComponent();
            DataTable dt = NVehiculo.MostrarVehiculo();

            IList<Vehiculos> V = new List<Vehiculos>();
            foreach (DataRow dr in dt.Rows)
            {
                V.Add(new Vehiculos
                {
                    Placa = dr[0].ToString(),
                    Tipo = dr[1].ToString()
                });
            }
            lbvehiculo.ItemsSource = V;
        }
    }
    public class Vehiculos
    {
        public string Placa { get; set; }
        public string Tipo { get; set; }
    }
}
