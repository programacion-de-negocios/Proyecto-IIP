﻿using System;
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
    /// Interaction logic for Vehiculo.xaml
    /// </summary>
    public partial class Vehiculo : UserControl
    {
        public Vehiculo()
        {
            InitializeComponent();
            DataTable dt = NVehiculo.MostrarVehiculo();
            IList<VehiculoLista> Lista = new List<VehiculoLista>();
            foreach (DataRow dr in dt.Rows)
            {
                Lista.Add(new VehiculoLista
                {
                    Placa = dr[0].ToString(),
                    Tipo_Vehiculo = dr[1].ToString(),
                });
            }
            LbVehiculo.ItemsSource = Lista;
        }
    }
    public class VehiculoLista
    {
        public string Placa { get; set; }
        public string Tipo_Vehiculo { get; set; }
    }
}
