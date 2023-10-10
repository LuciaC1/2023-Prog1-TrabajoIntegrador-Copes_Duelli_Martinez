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


        public Validacion ProgramarEnvío()
        {
            //Buscar compras estado open
            List<CompraDB> listadoCompras = archivoCompra.GetCompraDBList().Where(x => x.EstadoCompra == EstadosCompraDB.OPEN)
                .OrderBy(x => x.FechaEntregaSolicitada).ToList();
            //Asignar camión si
            //1. La fecha de viaje no coincide
            
            //Registro viaje
            //Asignar a camiones si
            //1. El camión tiene lugar
            //2. La distancia a cliente es menor a distancia recorrible
            //Si no se asigna:
            //Agregar 2 semanas a fecha estimada
        }



        #region Auxiliares


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
            var cliente = clienteService.ObtenerListadoClientes().First(x => x.DNI == dniCliente);
            var camioneta = camionetaService.ObtenerListadoCamionetas().First(x => x.Patente == patente);
            if (CalcularDistanciaEntrePuntos(cliente) > camioneta.DistanciaMaxKM)
            {
                return false;
            }
            return true;
        }
        private double CalcularDistanciaEntrePuntos(ClienteDto cliente)
        {
            double lat1 = GradosARadianes(-31.25033);
            double lon1 = GradosARadianes(-61.4867);
            double lat2 = GradosARadianes(cliente.LatitudGeografica);
            double lon2 = GradosARadianes(cliente.LongitudGeografica);

            double radioTierra = 6371;

            // Fórmula de Haversine
            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;
            double a = Math.Pow(Math.Sin(dlat / 2), 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(dlon / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distancia = radioTierra * c;

            return distancia;

        }

        //Metodo para pasar de grados a radianes
        private double GradosARadianes(double grados)
        {
            return grados * (Math.PI / 180);
        }

        #endregion Auxiliares
    }
}
