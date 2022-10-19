using hjs_encomiendas_servidor.Common;
using hjs_encomiendas_servidor.Common.ValueObjects;
using hjs_encomiendas_servidor.Common.ValueObjects.Pedidos;
using hjs_encomiendas_servidor.Dominio.Interfaces;
using hjs_encomiendas_servidor.Modelo;
using hjs_encomiendas_servidor.Persistencia;
using Microsoft.EntityFrameworkCore;
using Google.OrTools.ConstraintSolver;
using System.Text.Json;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace hjs_encomiendas_servidor.Dominio
{
    public class dPedido : IDominio
    {
        private readonly ProjectContext context;

        public dPedido(ProjectContext _context)
        {
            context = _context;
        }

        public BaseMethodOut? agregarPedido(PedidoVO pedidoVO)
        {
            Pedido pedido = new Pedido(pedidoVO);

            context.Pedido.Add(pedido);

            context.SaveChanges();

            return new BaseMethodOut { OperationResult = OperationResult.Success };
        }

        public PedidosVO obtenerPedidos(GetDataInPedidoVO getData)
        {
            var qry = (from p in context.Pedido where p.activo == true select p);

            if (getData.idUsuarioPedido != 0)
            {
                qry = qry.Where(collection => collection.idCliente == getData.idUsuarioPedido);
            }

             if (getData.estado != 0) //FIXME ARRAY DE ESTADOS
            {
                qry = qry.Where(collection => collection.estado == getData.estado);
            }

            var count = qry.Count();
            var pedidos = qry.OrderBy(p => p.idPedido)
                .Skip(getData.PageIndex)
                .Take(getData.PageSize).Include(p => p.chofer).Include(p => p.cliente).Include(p => p.transporte).Include(p => p.tipoPedido)
                .ToList();

            PedidosVO usuariosVO = new PedidosVO { pedidos = pedidos, totalRows = count, OperationResult = OperationResult.Success };

            return usuariosVO;
        }

        public PedidosVO obtenerUltimosPedidos(int idChofer)
        {
            PedidosVO pedidosVO = new PedidosVO { OperationResult = OperationResult.Error };
            if (idChofer == 0) return pedidosVO;

            var ultimoPedidoChofer = context.Pedido.Where(p => p.idChofer == idChofer && p.activo == true && p.fechaCreacion.Day != DateTime.Now.Day).OrderByDescending(p => p.fechaCreacion)
                       .FirstOrDefault();
            if (ultimoPedidoChofer != null)
            {
                var qry = (from p in context.Pedido where p.activo == true select p);

                qry = qry.Where(p => p.idChofer == idChofer && p.fechaCreacion.Day == ultimoPedidoChofer.fechaCreacion.Day);

                var count = qry.Count();
                var pedidos = qry.OrderBy(p => p.fechaCreacion).Include(p => p.cliente).Include(p => p.transporte).Include(p => p.tipoPedido)
                    .ToList();

                pedidosVO = new PedidosVO { pedidos = pedidos, totalRows = count, OperationResult = OperationResult.Success };
            }

            return pedidosVO;
        }

        public PedidosVO obtenerPedidosDeHoy(int idChofer)
        {
            PedidosVO pedidosVO = new PedidosVO { OperationResult = OperationResult.Error };
            if (idChofer == 0) return pedidosVO;
            
                var qry = (from p in context.Pedido where p.activo == true select p);

                qry = qry.Where(p => p.idChofer == idChofer && p.fechaCreacion.Day == DateTime.Now.Day);
            
                var pedidos = qry.OrderBy(p => p.fechaCreacion).Include(p => p.cliente).Include(p => p.transporte).Include(p => p.tipoPedido)
                    .ToList();

                pedidosVO = new PedidosVO { pedidos = pedidos, totalRows = 0, OperationResult = OperationResult.Success };

            return pedidosVO;
        }

        public PedidosVO obtenerPedidosChofer(GetDataInPedidoVO getData)
        {
            var qry = (from p in context.Pedido where p.activo == true select p);

            if (getData.idUsuarioChofer != 0)
            {
                qry = qry.Where(collection => collection.idChofer == getData.idUsuarioChofer && collection.fechaCreacion.Day == getData.fecha.Day && collection.estado == ((int) Constantes.ESTADO_PEDIDO_PENDIENTE));
            }
            else
            {
                return new PedidosVO { OperationResult = OperationResult.Error };
            }

            var pedidos = qry.OrderBy(p => p.idPedido).Include(p => p.cliente).Include(p => p.tipoPedido)
                .ToList();

            PedidosVO usuariosVO = new PedidosVO { pedidos = pedidos, OperationResult = OperationResult.Success };

            return usuariosVO;
        }

        public PedidosVO obtenerPedidosDiaEstadoChofer(GetDataInPedidoVO getData)
        {
            var qry = (from p in context.Pedido where p.activo == true select p);

            if (getData.idUsuarioChofer != 0)
            {
                qry = qry.Where(collection => collection.idChofer == getData.idUsuarioChofer && collection.fechaCreacion.Day == getData.fecha.Day);
            }
            else
            {
                return new PedidosVO { OperationResult = OperationResult.Error };
            }

            if (getData.estado != 0)
            {
                qry = qry.Where(collection => collection.estado == getData.estado);
            }

            var pedidos = qry.OrderBy(p => p.idPedido).Include(p => p.cliente).Include(p => p.tipoPedido)
                .ToList();

            PedidosVO usuariosVO = new PedidosVO { pedidos = pedidos, OperationResult = OperationResult.Success };

            return usuariosVO;
        }

        public Pedido? obtenerPedido(int idPedido)
        {
            var pedido = context.Pedido.Where(p => p.idPedido == idPedido && p.activo == true).FirstOrDefault();

            return pedido;
        }

        public BaseMethodOut eliminarPedido(int idPedido)
        {
            BaseMethodOut result = new BaseMethodOut { OperationResult = OperationResult.Success };

            var pedido = obtenerPedido(idPedido);

            if (pedido != null)
            {
                pedido.activo = false;
                context.SaveChanges();

                return result;
            }

            result.OperationResult = OperationResult.Error;
            return result;
        }

        public BaseMethodOut modificarPedido(PedidoVO pedidoVO)
        {
            BaseMethodOut result = new BaseMethodOut { OperationResult = OperationResult.Success };

            var pedido = obtenerPedido(pedidoVO.idPedido);

            if (pedido != null)
            {
                pedido.update(pedidoVO);
                context.SaveChanges();

                return result;
            }

            result.OperationResult = OperationResult.Error;
            return result;
        }

        public BaseMethodOut actualizarEstadoPedido(int idPedido, int estado)
        {
            BaseMethodOut result = new BaseMethodOut { OperationResult = OperationResult.Success };

            var pedido = obtenerPedido(idPedido);

            if (pedido != null)
            {
                pedido.estado = estado;
                context.SaveChanges();

                return result;
            }

            result.OperationResult = OperationResult.Error;
            return result;
        }

        public PedidosVO obtenerOptimizacion(List<String> addresses)
        {
            HttpClient client = new HttpClient();
            JsonSerializerOptions jso = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };


            var json = JsonSerializer.Serialize(addresses, jso);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("url"),

                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var response = client.SendAsync(request).ConfigureAwait(true);

            var responseInfo = response.GetAwaiter().GetResult();

            if (!responseInfo.IsSuccessStatusCode)
            {
                return new PedidosVO { OperationResult = OperationResult.Error };
            }
            
            var responseString = responseInfo.Content.ReadAsStringAsync().Result;
            
            long[][] distanceMatrixjs = JsonSerializer.Deserialize<long[][]>(responseString);
            
            // Instantiate the data problem.
            DataModel data = new DataModel();
            data.DistanceMatrix = distanceMatrixjs;
            
            // Create Routing Index Manager
            RoutingIndexManager manager =
                new RoutingIndexManager(data.DistanceMatrix.GetLength(0), data.VehicleNumber, data.Depot);

            // Create Routing Model.
            RoutingModel routing = new RoutingModel(manager);

            int transitCallbackIndex = routing.RegisterTransitCallback((long fromIndex, long toIndex) =>
            {
                // Convert from routing variable Index to
                // distance matrix NodeIndex.
                var fromNode = manager.IndexToNode(fromIndex);
                var toNode = manager.IndexToNode(toIndex);
                return data.DistanceMatrix[fromNode][toNode];
            });

            // Define cost of each arc.
            routing.SetArcCostEvaluatorOfAllVehicles(transitCallbackIndex);

            // Setting first solution heuristic.
            RoutingSearchParameters searchParameters =
                operations_research_constraint_solver.DefaultRoutingSearchParameters();
            searchParameters.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.PathCheapestArc;

            // Solve the problem.
            Assignment solution = routing.SolveWithParameters(searchParameters);
            
            // Se formatea la solucion en list<int>
            List<int> orden = FormatSolution(routing, manager, solution);

            PedidosVO usuariosVO = new PedidosVO { ordenPedidos = orden, OperationResult = OperationResult.Success };

            return usuariosVO;
        }

        public List<int> obtenerCantidadPedidosPorMes()
        {
            var query = (from m in Enumerable.Range(1, 12)
                         join p in context.Pedido on m equals p.fechaCreacion.Month into monthGroup
                         select monthGroup.Count()
             ).ToList();

            return query;
        }



        class DataModel
        {
            public long[][] DistanceMatrix = new long[][]
            {
            };

            public int VehicleNumber = 1;
            public int Depot = 0;
        };
        static List<int> FormatSolution(in RoutingModel routing, in RoutingIndexManager manager, in Assignment solution)
        {
            List<int> sol = new List<int>();
            long routeDistance = 0;
            var index = routing.Start(0);
            while (routing.IsEnd(index) == false)
            {
                sol.Add(manager.IndexToNode((int)index));
                var previousIndex = index;
                index = solution.Value(routing.NextVar(index));
                routeDistance += routing.GetArcCostForVehicle(previousIndex, index, 0);
            }
            sol.Add(manager.IndexToNode((int)index));

            return sol;
        }
    }
}
