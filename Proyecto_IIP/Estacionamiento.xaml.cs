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

using Capa_Negocio; //Namespace para usar la capanegocio
using System.Data; //Namespace para usar el datatable
namespace Proyecto_IIP
{
    /// <summary>
    /// Interaction logic for Estacionamiento.xaml
    /// </summary>
    public partial class Estacionamiento : UserControl
    {
        public Estacionamiento()
        {
            InitializeComponent();
            DataTable dt = NVehiculo.MostrarEstacionamiento();
            IList<EstacionamientoLista> Lista = new List<EstacionamientoLista>();
            foreach (DataRow dr in dt.Rows)
            {
                Lista.Add(new EstacionamientoLista
                {
                    Placa = dr[0].ToString(),
                    Tipo_Vehiculo = dr[1].ToString(),
                    Hora_Ingreso = dr[2].ToString()
                });
            }
            LbEstacionamiento.ItemsSource = Lista;

        }
    }
    public class EstacionamientoLista
    {
        public string Placa { get; set; }
        public string Tipo_Vehiculo { get; set; }
        public string Hora_Ingreso { get; set; }
    }
}
