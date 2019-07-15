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

        public static DataTable MostrarTipo()
        {
            return new DVehiculo().MostrarTipo();
        }
        public static DataTable MostrarEstacionamiento()
        {
            return new DVehiculo().MostrarEstacionamiento();
        }

        public static string IngresoVehiculo(string Placa, int Tipo)
        {
            DVehiculo Obj = new DVehiculo();
            Obj.Placa = Placa;
            Obj.IdTipovehiculo = Tipo;
            return Obj.IngresarVehiculo(Obj);
        }
        public static DataTable SalidaVehiculo(string Placa)
        {
            DVehiculo Obj = new DVehiculo();
            Obj.Placa = Placa;
            return Obj.SalidaVehiculo(Obj);
        }

    }
}
