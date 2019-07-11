using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using Capa_Datos;

namespace Capa_Negocio
{
    public class NVehiculo
    {
        public static DataTable MostrarVehiculo()
        {
            return new DVehiculo().MostrarVehiculo();
        }
    }
}
