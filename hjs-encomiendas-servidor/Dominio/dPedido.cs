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

        public PedidosVO obtenerPedidos(GetDataInPedidoVO getData, int[] estados)
        {
            var qry = (from p in context.Pedido where p.activo == true select p);

            if (getData.idUsuarioPedido != 0)
            {
                qry = qry.Where(collection => collection.idCliente == getData.idUsuarioPedido || collection.idChofer == getData.idUsuarioPedido);
            }
            
            if (getData.fechaDesde != null && getData.fechaHasta != null)
            {
                qry = qry.Where(collection => collection.fechaRetiro >= getData.fechaDesde && collection.fechaRetiro <= getData.fechaHasta);
            }
            
            if (getData.idUnidad != 0)
            {
                qry = qry.Where(collection => collection.idTransporte == getData.idUnidad);
            }

            if (estados != null && estados.Length > 0)
            {
                qry = qry.Where(collection => estados.Contains(collection.estado));
            }

            var count = qry.Count();
            var pedidos = qry.OrderBy(p => p.fechaRetiro)
                .Skip(getData.PageIndex)
                .Take(getData.PageSize).Include(p => p.chofer).Include(p => p.cliente).Include(p => p.transporte).Include(p => p.tipoPedido)
                .ToList();
            
            int distanciaRecorrida = 0;
            if (getData.idUnidad != 0 && getData.fechaDesde != null && getData.fechaHasta != null)
            {
                int? query = 0;
                
                if (estados != null && estados.Length > 0)
                {
                    query = (from p in context.Pedido
                                  where p.activo == true && p != null && p.idTransporte == getData.idUnidad
                             && p.fechaRetiro >= getData.fechaDesde && p.fechaRetiro <= getData.fechaHasta
                             && estados.Contains(p.estado) select p.distanciaRecorrida).Sum();
                } else
                {
                       query = (from p in context.Pedido where p.activo == true && p != null && p.idTransporte == getData.idUnidad
                             && p.fechaRetiro >= getData.fechaDesde && p.fechaRetiro <= getData.fechaHasta select p.distanciaRecorrida).Sum();
                }
                
                if (query != null)
                {
                    distanciaRecorrida = (int)query;
                }
            }

            PedidosVO usuariosVO = new PedidosVO { pedidos = pedidos, totalRows = count, OperationResult = OperationResult.Success, distanciaRecorrida = distanciaRecorrida };

            return usuariosVO;
        }

        public PedidosVO obtenerPedidosReservados(GetDataInPedidoVO getData)
        {
            var qry = (from p in context.Pedido where p.activo == false && p.estado == ((int)Constantes.ESTADO_PEDIDO_PENDIENTE) && p.reservado == true select p);
            
            var count = qry.Count();
            var pedidos = qry.OrderBy(p => p.fechaRetiro)
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

            var ultimoPedidoChofer = context.Pedido.Where(p => p.idChofer == idChofer && p.activo == true && p.fechaRetiro.Day != DateTime.Now.Day).OrderByDescending(p => p.fechaRetiro)
                       .FirstOrDefault();
            if (ultimoPedidoChofer != null)
            {
                var qry = (from p in context.Pedido where p.activo == true select p);

                qry = qry.Where(p => p.idChofer == idChofer && p.fechaRetiro.Day == ultimoPedidoChofer.fechaRetiro.Day);

                var count = qry.Count();
                var pedidos = qry.OrderBy(p => p.fechaRetiro).Include(p => p.cliente).Include(p => p.transporte).Include(p => p.tipoPedido)
                    .ToList();

                pedidosVO = new PedidosVO { pedidos = pedidos, totalRows = count, OperationResult = OperationResult.Success };
            }

            return pedidosVO;
        }

        public PedidosVO obtenerPedidosRetiradosChofer(int idChofer)
        {
            PedidosVO pedidosVO = new PedidosVO { OperationResult = OperationResult.Error };
            if (idChofer == 0) return pedidosVO;
            
           
                var qry = (from p in context.Pedido where p.activo == true select p);

                qry = qry.Where(p => p.idChofer == idChofer && p.estado == ((int)Constantes.ESTADO_PEDIDO_RETIRADO));

                var count = qry.Count();
                var pedidos = qry.OrderByDescending(p => p.fechaRetiro).Include(p => p.cliente).Include(p => p.transporte).Include(p => p.tipoPedido)
                    .ToList();

                pedidosVO = new PedidosVO { pedidos = pedidos, totalRows = count, OperationResult = OperationResult.Success };

            return pedidosVO;
        }

        public PedidosVO obtenerPedidosDeHoy(int idChofer)
        {
            PedidosVO pedidosVO = new PedidosVO { OperationResult = OperationResult.Error };
            if (idChofer == 0) return pedidosVO;
            
                var qry = (from p in context.Pedido where p.activo == true select p);

                qry = qry.Where(p => p.idChofer == idChofer && p.fechaRetiro.Day == DateTime.Now.Day);
            
                var pedidos = qry.OrderBy(p => p.fechaRetiro).Include(p => p.cliente).Include(p => p.transporte).Include(p => p.tipoPedido)
                    .ToList();

                pedidosVO = new PedidosVO { pedidos = pedidos, totalRows = 0, OperationResult = OperationResult.Success };

            return pedidosVO;
        }

        public PedidosVO obtenerPedidosChofer(GetDataInPedidoVO getData)
        {
            var qry = (from p in context.Pedido where p.activo == true select p);

            if (getData.idUsuarioChofer != 0)
            {
                qry = qry.Where(collection => collection.idChofer == getData.idUsuarioChofer && collection.estado == ((int) Constantes.ESTADO_PEDIDO_PENDIENTE));
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
                qry = qry.Where(collection => collection.idChofer == getData.idUsuarioChofer && collection.fechaRetiro.Day == getData.fecha.Day);
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
            var pedido = context.Pedido.Where(p => p.idPedido == idPedido).FirstOrDefault();

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

        public BaseMethodOut actualizarEstadoPedido(int idPedido, int estado, int metros)
        {
            BaseMethodOut result = new BaseMethodOut { OperationResult = OperationResult.Success };

            var pedido = obtenerPedido(idPedido);

            if (pedido != null)
            {
                pedido.estado = estado;

                if (metros > 0)
                {
                    pedido.distanciaRecorrida = metros;
                }
                
                if (estado != ((int)Constantes.ESTADO_PEDIDO_ENTREGADO))
                {
                    pedido.fechaRetiro = DateTime.Now;
                }
                
                if (estado == ((int)Constantes.ESTADO_PEDIDO_RETIRADO))
                {
                    pedido.fechaRetirado = DateTime.Now;
                } else if(estado == ((int)Constantes.ESTADO_PEDIDO_ENTREGADO))
                {
                    pedido.fechaEntrega = DateTime.Now;
                }
                    
                context.SaveChanges();

                return result;
            }

            result.OperationResult = OperationResult.Error;
            return result;
        }

        public List<int> obtenerCantidadPedidosPorMes(int anio)
        {
            if(anio == 0)
            {
                anio = DateTime.Now.Year;
            }
            
            var query = (from m in Enumerable.Range(1, 12)
                         join p in context.Pedido on m equals p.fechaRetiro.Month into monthGroup
                         select monthGroup.Count(p => p.activo == true && p.fechaRetiro.Year == anio)
             ).ToList();

            return query;
        }


    }
}
