using hjs_encomiendas_servidor.Common;
using hjs_encomiendas_servidor.Common.ValueObjects;
using hjs_encomiendas_servidor.Common.ValueObjects.Pedidos;
using hjs_encomiendas_servidor.Dominio.Interfaces;
using hjs_encomiendas_servidor.Modelo;
using hjs_encomiendas_servidor.Persistencia;

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

        public PedidosVO obtenerPedidos(GetDataInVO getData)
        {
            var qry = (from p in context.Pedido where p.activo == true select p);
            var count = qry.Count();
            var pedidos = qry.OrderBy(p => p.idPedido)
                .Skip(getData.PageIndex)
                .Take(getData.PageSize)
                .ToList();

            PedidosVO usuariosVO = new PedidosVO { pedidos = pedidos, totalRows = count, OperationResult = OperationResult.Success };

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
    }
}
