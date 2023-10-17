using EmpresaEnvíoData;
using EmpresaEnvÍoDto;

namespace EmpresaEnvíoService
{
    public class ViajeService
    {
        #region Constructor

        private ArchivoViaje archivoViaje;
        private CamionetaService camionetaService;
        private ClienteService clienteService;
        private ArchivoCompra archivoCompra;

        public ViajeService()
        {
            archivoViaje = new ArchivoViaje();
            camionetaService = new CamionetaService();
            clienteService = new ClienteService();
            archivoCompra = new ArchivoCompra();
        }

        #endregion Constructor

        // Metodo para generar viajes (POST)
        public ValidacionViaje ProgramarEnvío(DateTime fechaDesde, DateTime fechaHasta)
        {
            // Validar fechas de viaje
            Validacion validacion = ValidarViaje_Fechas(fechaDesde, fechaHasta);
            ValidacionViaje validacionViaje = new();
            validacionViaje.Errores = validacion.Errores;
            validacionViaje.Resultado = validacion.Resultado;
            if (!validacionViaje.Resultado)
            {
                return validacionViaje;
            }
            //Buscar camionetas disponibles en esas fechas
            List<Camioneta> camionetas = CamionetasDisponibles(fechaDesde, fechaHasta);
            if (camionetas.Count == 0)
            {
                validacionViaje.Resultado = false;
                validacionViaje.Errores.Add(new Error() { ErrorDetail = "No hay camionetas disponibles para el viaje" });
                return validacionViaje;
            }

            //Buscar compras estado open entre esas fechas
            List<CompraDto> listadoCompras = archivoCompra.GetCompraDBList()
                .Where(x => x.EstadoCompra == EstadosCompraDB.OPEN && x.FechaEntregaSolicitada > fechaDesde && x.FechaEntregaSolicitada < fechaHasta)
                .Select(x => new CompraDto()
                {
                    CodigoProducto = x.CodigoProducto,
                    DNICliente = x.DNICliente,
                    FechaEntregaSolicitada = x.FechaEntregaSolicitada,
                    CodigoCompra = x.CodigoCompra,
                    CantComprada = x.CantComprada,
                    EstadoCompra = EstadosCompraDto.OPEN,
                    LatitudGeografica = x.LatitudGeografica,
                    LongitudGeografica = x.LongitudGeografica,
                    MontoTotal = x.MontoTotal,
                    FechaCompra = x.FechaCompra
                }).ToList();

            //Verificar que haya compras a enviar
            if (listadoCompras.Count == 0)
            {
                validacionViaje.Resultado = false;
                validacionViaje.Errores.Add(new Error() { ErrorDetail = "No hay compras listas para enviar" });
                return validacionViaje;
            }

            //Buscar la mejor combinación posible
            ResultadoEnvio resultado = MejorCombinacion(listadoCompras, camionetas);

            //Verificar que haya una combinación posible
            if (resultado.ListadoCompras.Count == 0)
            {
                validacionViaje.Resultado = false;
                validacionViaje.Errores.Add(new Error() { ErrorDetail = "Las compras a enviar no pudieron ser combinadas correctamente" });
                return validacionViaje;
            }

            //Cambiar estado de compras a READY_TO_DISPATCH
            var listadoComprasDB = archivoCompra.GetCompraDBList();
            foreach (var compra in listadoComprasDB)
            {
                if (!resultado.ListadoCompras.Any(x => x.CodigoCompra == compra.CodigoCompra))
                {
                    compra.FechaEntregaSolicitada = compra.FechaEntregaSolicitada.AddDays(14);
                    continue;
                }
                listadoComprasDB.First(x => x.CodigoCompra == compra.CodigoCompra).EstadoCompra = EstadosCompraDB.READY_TO_DISPATCH;
            }

            //Aumentar fecha estimada de entrega en 2 semanas para las compras que no estén en resultado.ListadoCompras
            foreach (var compra in listadoCompras.Where(x => !resultado.ListadoCompras.Any(y => y.CodigoCompra == x.CodigoCompra)))
            {
                listadoComprasDB.First(x => x.CodigoCompra == compra.CodigoCompra).FechaEntregaSolicitada.AddDays(14);
            }
            archivoCompra.SaveCompraDB(listadoComprasDB);

            //Registro viaje
            List<ViajeDB> listadoViajesDB = archivoViaje.GetViajeDBList();
            ViajeDto viaje = new ViajeDto()
            {
                CodigoUnicoViaje = listadoViajesDB.Count + 1,
                FechaEntregasDesde = fechaDesde,
                FechaEntregasHasta = fechaHasta,
                ListadoCompras = resultado.ListadoCompras.Select(x => x.CodigoCompra).ToList(),
                Patente = resultado.Patente,
                PorcentajeOcupacionCarga = resultado.PorcentajeOcupacion
            };
            listadoViajesDB.Add(new ViajeDB()
            {
                CodigoUnicoViaje = viaje.CodigoUnicoViaje,
                FechaEntregasDesde = viaje.FechaEntregasDesde,
                FechaEntregasHasta = viaje.FechaEntregasHasta,
                Patente = viaje.Patente,
                PorcentajeOcupacionCarga = viaje.PorcentajeOcupacionCarga,
                ListadoCompras = viaje.ListadoCompras,
                FechaCreacion = DateTime.Now
            });

            //Guardar listado de viajes
            archivoViaje.SaveViajeDB(listadoViajesDB);

            //Devolver resultado correcto de viaje
            validacionViaje.Resultado = true;
            validacionViaje.Viaje = viaje;
            return validacionViaje;
        }

        #region Auxiliares

        //Metodo para validar viaje entre fechas
        private Validacion ValidarViaje_Fechas(DateTime fechad, DateTime fechah)
        {
            Validacion validacion = new Validacion();
            if (fechad < DateTime.Now)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "Fecha de inicio del viaje menor a la fecha actual" });
                return validacion;
            }
            if ((fechah - fechad).Days > 7)
            {
                validacion.Errores.Add(new Error() { ErrorDetail = "La fecha de entrega solo puede ser hasta 7 días mayor que la fecha de inicio del viaje" });
                return validacion;
            }
            validacion.Resultado = true;
            return validacion;
        }

        //Metodo para buscar camionetas disponibles para el viaje
        private List<Camioneta> CamionetasDisponibles(DateTime fechaDesde, DateTime fechaHasta)
        {
            List<ViajeDB> listaViajes = archivoViaje.GetViajeDBList();
            List<Camioneta> listaCamionetas = CamionetaService.ObtenerListadoCamionetas();
            foreach (Camioneta camioneta in listaCamionetas)
            {
                var listCamioneta = listaViajes.Where(x => x.Patente == camioneta.Patente);
                if (listCamioneta.Any(x => (x.FechaEntregasDesde < fechaHasta && x.FechaEntregasHasta > fechaDesde)
                || (x.FechaEntregasHasta > fechaDesde && x.FechaEntregasDesde < fechaHasta)))
                {
                    listaCamionetas.Remove(camioneta);
                }
            }
            return listaCamionetas;
        }

        //Metodo para validar que la distancia no sea mayor a la de la camioneta
        private bool ValidarDistanciaDeCamioneta(CompraDto compra, string patente)
        {
            var camioneta = CamionetaService.ObtenerListadoCamionetas().First(x => x.Patente == patente);
            if (CalcularDistanciaEntrePuntos(compra) > camioneta.DistanciaMaxKM)
            {
                return false;
            }
            return true;
        }

        private double CalcularDistanciaEntrePuntos(CompraDto compra)
        {
            double lat1 = GradosARadianes(-31.25033);
            double lon1 = GradosARadianes(-61.4867);
            double lat2 = GradosARadianes(compra.LatitudGeografica);
            double lon2 = GradosARadianes(compra.LongitudGeografica);

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

        #region Combinacion para viajes

        private ResultadoEnvio MejorCombinacion(List<CompraDto> compras, List<Camioneta> camionetas)
        {
            ResultadoEnvio mejorResultado = null;
            foreach (var camioneta in camionetas)
            {
                var resultado = CombinacionParaCamioneta(compras, camioneta, new List<CompraDto>(), 0);
                if (mejorResultado == null || resultado.PorcentajeOcupacion > mejorResultado.PorcentajeOcupacion)
                    mejorResultado = resultado;
            }
            return mejorResultado;
        }

        private ResultadoEnvio CombinacionParaCamioneta(List<CompraDto> comprasRestantes, Camioneta camioneta, List<CompraDto> comprasSeleccionadas, double tamañoActual)
        {
            if (!comprasRestantes.Any() || tamañoActual > camioneta.TamañoCargaCM3)
            {
                return new ResultadoEnvio
                {
                    Patente = camioneta.Patente,
                    PorcentajeOcupacion = tamañoActual / camioneta.TamañoCargaCM3 * 100,
                    ListadoCompras = comprasSeleccionadas
                };
            }
            var mejorResultado = CombinacionParaCamioneta(comprasRestantes.Skip(1).ToList(), camioneta, comprasSeleccionadas, tamañoActual);
            var compra = comprasRestantes[0];
            var producto = new ArchivoProducto().GetProductoDBList().First(x => x.CodProducto == compra.CodigoProducto);
            if (ValidarDistanciaDeCamioneta(compra, camioneta.Patente) && tamañoActual + producto.AnchoCaja * producto.ProfundidadCaja * producto.AltoCaja * (compra.CantComprada) <= camioneta.TamañoCargaCM3)
            {
                var comprasNueva = new List<CompraDto>(comprasSeleccionadas) { compra };
                var resultadoConCompra = CombinacionParaCamioneta(comprasRestantes.Skip(1).ToList(), camioneta, comprasNueva,
                    tamañoActual + producto.AnchoCaja * producto.ProfundidadCaja * producto.AltoCaja * (compra.CantComprada));
                if (resultadoConCompra.PorcentajeOcupacion > mejorResultado.PorcentajeOcupacion)
                    mejorResultado = resultadoConCompra;
            }
            return mejorResultado;
        }
        #endregion Combinacion para viajes
    }
}