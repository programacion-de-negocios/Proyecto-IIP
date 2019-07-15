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
    /// Interaction logic for Reporte.xaml
    /// </summary>
    public partial class Reporte : UserControl
    {
        public Reporte()
        {
            InitializeComponent();
            DataTable dt = NVehiculo.MostrarReporte();
            IList<ReporteLista> Lista = new List<ReporteLista>();
            foreach (DataRow dr in dt.Rows)
            {
                Lista.Add(new ReporteLista
                {
                    Placa = dr[0].ToString(),
                    Hora_Entrada = dr[1].ToString(),
                    Hora_Salida = dr[2].ToString(),
                    Tiempo = dr[3].ToString(),
                    Total = dr[4].ToString(),
                    Fecha = dr[5].ToString()
                });
            }
            LbReporteGeneral.ItemsSource = Lista;
        }
    }
    public class ReporteLista
    {
        public string Placa { get; set; }
        public string Hora_Entrada { get; set; }
        public string Hora_Salida { get; set; }
        public string Tiempo { get; set; }
        public string Total { get; set; }
        public string Fecha { get; set; }

    }
}
