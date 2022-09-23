using hjs_encomiendas_servidor.Common;
using hjs_encomiendas_servidor.Common.ValueObjects;
using hjs_encomiendas_servidor.Common.ValueObjects.Pedidos;
using hjs_encomiendas_servidor.Dominio.Interfaces;
using hjs_encomiendas_servidor.Modelo;
using hjs_encomiendas_servidor.Persistencia;

namespace hjs_encomiendas_servidor.Dominio
{
    public class dTipoPedido : IDominio
    {
        private readonly ProjectContext context;

        public dTipoPedido(ProjectContext _context)
        {
            context = _context;
        }
        
        public BaseMethodOut? agregarTipoPedido(TipoPedidoVO tipoPedidoVO)
        {
            TipoPedido tipoPedido = new TipoPedido(tipoPedidoVO);

            context.TipoPedido.Add(tipoPedido);

            context.SaveChanges();

            return new BaseMethodOut { OperationResult = OperationResult.Success };
        }

        public TiposPedidoVO obtenerTiposPedido(GetDataInVO getData)
        {
            var qry = (from t in context.TipoPedido where t.activo == true select t);
            var count = qry.Count();
            var tiposDePedidos = qry.OrderBy(t => t.idTipoPedido)
                .Skip(getData.PageIndex)
                .Take(getData.PageSize)
                .ToList();

            TiposPedidoVO tipoPedidoVO = new TiposPedidoVO { tiposPedidos = tiposDePedidos, totalRows = count, OperationResult = OperationResult.Success };


            return tipoPedidoVO;
        }

        public TipoPedido? obtenerTipoPedido(int idTipoPedido)
        {
            var tipoPedido = context.TipoPedido.Where(t => t.idTipoPedido == idTipoPedido && t.activo == true).FirstOrDefault();

            return tipoPedido;
        }

        public BaseMethodOut modificarPedido(TipoPedidoVO tipoPedidoVO)
        {
            BaseMethodOut result = new BaseMethodOut { OperationResult = OperationResult.Success };

            var tipoPedido = obtenerTipoPedido(tipoPedidoVO.idTipoPedido);

            if (tipoPedido != null)
            {
                tipoPedido.update(tipoPedidoVO);
                context.SaveChanges();

                return result;
            }

            result.OperationResult = OperationResult.Error;
            return result;
        }

        public BaseMethodOut eliminarTipoPedido(int idTipoPedido)
        {
            BaseMethodOut result = new BaseMethodOut { OperationResult = OperationResult.Success };

            var tipoPedido = obtenerTipoPedido(idTipoPedido);

            if (tipoPedido != null)
            {
                tipoPedido.activo = false;
                context.SaveChanges();

                return result;
            }

            result.OperationResult = OperationResult.Error;
            return result;
        }
    }
}
