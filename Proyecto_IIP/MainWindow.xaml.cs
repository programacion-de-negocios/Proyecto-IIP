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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
           
        }

        private void ListViewItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            VtnContenedor.Children.Clear();
            VtnContenedor.Children.Add(new Inicio());
        }

        private void LviParqueo_MouseUp(object sender, MouseButtonEventArgs e)
        {
            VtnContenedor.Children.Clear();
            VtnContenedor.Children.Add(new Estacionamiento());
        }

        private void LviVehiculo_MouseUp(object sender, MouseButtonEventArgs e)
        {
            VtnContenedor.Children.Clear();
            VtnContenedor.Children.Add(new Vehiculo());
        }

        private void LviSalida_MouseUp(object sender, MouseButtonEventArgs e)
        {
            VtnContenedor.Children.Clear();
            VtnContenedor.Children.Add(new Salida());
        }

        //EVENTO PARA LLAMAR AL METODO Mostrar_Vehiculo


    }

}
