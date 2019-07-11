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
using Capa_Negocio;
namespace Proyecto_IIP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
        }

        //EVENTO PARA LLAMAR AL METODO Mostrar_Vehiculo
        private void LbTipo_Vehiculo_SelectionChanged(object sender,SelectionChangedEventArgs e)
        {
   
        }
    }
}
