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
                qry = qry.Where(collection => collection.fechaCreacion >= getData.fechaDesde && collection.fechaCreacion <= getData.fechaHasta);
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
            var pedidos = qry.OrderBy(p => p.fechaCreacion)
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

        public PedidosVO obtenerPedidosRetiradosChofer(int idChofer)
        {
            PedidosVO pedidosVO = new PedidosVO { OperationResult = OperationResult.Error };
            if (idChofer == 0) return pedidosVO;
            
           
                var qry = (from p in context.Pedido where p.activo == true select p);

                qry = qry.Where(p => p.idChofer == idChofer && p.estado == ((int)Constantes.ESTADO_PEDIDO_RETIRADO));

                var count = qry.Count();
                var pedidos = qry.OrderByDescending(p => p.fechaCreacion).Include(p => p.cliente).Include(p => p.transporte).Include(p => p.tipoPedido)
                    .ToList();

                pedidosVO = new PedidosVO { pedidos = pedidos, totalRows = count, OperationResult = OperationResult.Success };

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
                if(estado == ((int)Constantes.ESTADO_PEDIDO_RETIRADO))
                {
                    pedido.fechaRetiro = DateTime.Now;
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

        public List<int> obtenerCantidadPedidosPorMes()
        {
            var query = (from m in Enumerable.Range(1, 12)
                         join p in context.Pedido on m equals p.fechaCreacion.Month into monthGroup
                         select monthGroup.Count()
             ).ToList();

            return query;
        }
        
       
    }
}
