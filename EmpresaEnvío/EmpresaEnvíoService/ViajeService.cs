using EmpresaEnvíoData;
using EmpresaEnvÍoDto;

namespace EmpresaEnvíoService
{
    public class ViajeService
    {
        ArchivoViaje archivoViaje;
        CamionetaService camionetaService;
        ClienteService clienteService;
        ArchivoCompra archivoCompra;
        public ViajeService()
        {
            archivoViaje = new ArchivoViaje();
            camionetaService = new CamionetaService();
            clienteService = new ClienteService();
            archivoCompra = new ArchivoCompra();
        }
        //Metodo para validar viaje entre fechas
        private Validacion ValidarViaje_Fechas(int codigoViaje, DateTime fechaDesde, DateTime fechaHasta)
        {
            Validacion validacion = new Validacion();
            if (fechaDesde < DateTime.Now)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "Fecha de inicio menor a la fecha actual" });
                return validacion;
            }
            if ((fechaHasta - fechaDesde).Days <= 7)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "La fecha de entrega solo puede ser hasta 7 días mayor que la fecha de inicio del viaje" });
                return validacion;
            }
            List<ViajeDB> listaViajes = archivoViaje.GetViajeDBList();
            if (listaViajes.Any(x => (x.FechaEntregasDesde < fechaHasta)
                || (x.FechaEntregasHasta > fechaDesde)))
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "Fechas concidentes con otros viajes" });
                return validacion;
            }
            validacion.Resultado = true;
            return validacion;
        }
        //Metodo para validar que la distancia no sea mayor a la de la camioneta
        private bool ValidarDistanciaDeCamioneta(int dniCliente, string patente)
        {
            var cliente = (dniCliente);
            var camioneta = camionetaService.ObtenerListadoCamionetas().First(x => x.Patente == patente);
            if (CalcularDistanciaEntrePuntos(dniCliente) > camioneta.DistanciaMaxKM)
            {
                return false;
            }
            return true;
        }
        //Metodo para calcular la distancia entre los puntos 
        public double CalcularDistanciaEntrePuntos(int dniCliente)
        {
            var cliente = clienteService.ObtenerListadoClientes().First(x => x.DNI == dniCliente);
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
