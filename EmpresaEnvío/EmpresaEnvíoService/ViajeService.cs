using EmpresaEnvíoData;
using EmpresaEnvÍoDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaEnvíoService
{
    public class ViajeService
    {
        ArchivoViaje archivoViaje;
        ArchivoCamioneta archivoCamioneta;
        ArchivoCliente archivoCliente;
        ArchivoCompra archivoCompra;
        public ViajeService()
        {
            archivoViaje = new ArchivoViaje();
            archivoCamioneta = new ArchivoCamioneta();
            archivoCliente = new ArchivoCliente();
            archivoCompra = new ArchivoCompra();
        }
        //Metodo obtener cliente
        public ClienteDB ObtenerCliente(int dniCliente)
        {
            List<ClienteDB> listaClientes = archivoCliente.GetClienteDBList();
            var cliente = listaClientes.FirstOrDefault(c => c.DNI == dniCliente);
            return cliente;
        }
        //Metodo obtener camioneta
        public CamionetaDB ObtenerCamioneta(string patente)
        {
            List<CamionetaDB> listaCamionetas = archivoCamioneta.GetCamionetaDBList();
            var camioneta = listaCamionetas.FirstOrDefault(s => s.Patente == patente);
            return camioneta;
        }
        //Metodo para validar los datos de la camioneta
        private bool ValidarDatos_Camioneta(string patente)
        {
            List<CamionetaDB> listaCamioneta = archivoCamioneta.GetCamionetaDBList();
            var camioneta = listaCamioneta.FirstOrDefault(c => c.Patente == patente);
            if (camioneta.Nombre== default || camioneta.Patente == default || camioneta == null)
            {
                return false;
            }
            return true;
        }
        //Metodo para validar viaje entre fechas
        private bool ValidarViaje_EntreFechas(int codigoViaje, DateTime fechaDesde, DateTime fechaHasta)
        {
            List<ViajeDB> listaViajes = archivoViaje.GetViajeDBList();
            var viaje = listaViajes.FirstOrDefault(v => v.CodigoUnicoViaje == codigoViaje);
            if (viaje.FechaEntregasDesde >= fechaDesde && viaje.FechaEntregasDesde <= fechaHasta 
                || viaje.FechaEntregasHasta >= fechaDesde && viaje.FechaEntregasHasta <= fechaDesde 
                || fechaDesde >= viaje.FechaEntregasDesde && fechaDesde <= viaje.FechaEntregasHasta 
                || fechaHasta >= viaje.FechaEntregasDesde && fechaHasta <= viaje.FechaEntregasHasta)
            {
                return false; 
            }
            return true; 
        }
        //Metodo para validar que la distancia no sea mayor a la de la camioneta
        private bool ValidarDistanciaDeCamioneta(int dniCliente, string patente)
        {
            var cliente = ObtenerCliente(dniCliente);
            var camioneta = ObtenerCamioneta(patente);
            if ( CalcularDistanciaEntrePuntos(dniCliente) > camioneta.DistanciaMaxKM)
            {
                return false;
            }
            return true;
        }
        //Metodo para calcular la distancia entre los puntos 
        public double CalcularDistanciaEntrePuntos(int dniCliente)
        {
            var cliente = ObtenerCliente(dniCliente);
            const double R = 6371; // Radio de la tierra en km
            double dLat = GradosARadianes(cliente.LatitudGeografica - -31.25033);
            double dLon = GradosARadianes(cliente.LongitudGeografica - -61.4867);
            double a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(GradosARadianes(cliente.LatitudGeografica)) * Math.Cos(GradosARadianes(-31.25033)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c;
            return distance;

        }
        //Metodo para pasar de grados a radianes
        public double GradosARadianes(double grados)
        {
            return grados * (Math.PI * 180);
        }
    }
}
