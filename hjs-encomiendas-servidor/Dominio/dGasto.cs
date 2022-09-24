using hjs_encomiendas_servidor.Common;
using hjs_encomiendas_servidor.Common.ValueObjects;
using hjs_encomiendas_servidor.Common.ValueObjects.Gastos;
using hjs_encomiendas_servidor.Dominio.Interfaces;
using hjs_encomiendas_servidor.Modelo;
using hjs_encomiendas_servidor.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace hjs_encomiendas_servidor.Dominio
{
    public class dGasto : IDominio
    {
        private readonly ProjectContext context;

        public dGasto(ProjectContext _context)
        {
            context = _context;
        }

        public BaseMethodOut? agregarGasto(GastoVO gastoVO)
        {
            Gasto gasto = new Gasto(gastoVO);

            context.Gasto.Add(gasto);

            context.SaveChanges();

            return new BaseMethodOut { OperationResult = OperationResult.Success };
        }
        
        public GastosVO obtenerGastos(GetDataInVO getData)
        {
            var qry = (from g in context.Gasto where g.activo == true select g);
            
            var count = qry.Count();
            var gastos = qry.OrderBy(p => p.idGasto)
                .Skip(getData.PageIndex)
                .Take(getData.PageSize).Include(p => p.usuario).Include(p => p.transporte)
                .ToList();

            GastosVO usuariosVO = new GastosVO { gastos = gastos, totalRows = count, OperationResult = OperationResult.Success };

            return usuariosVO;
        }

        public Gasto? obtenerGasto(int idGasto)
        {
            var gasto = context.Gasto.Where(p => p.idGasto == idGasto && p.activo == true).FirstOrDefault();

            return gasto;
        }

        public BaseMethodOut eliminarGasto(int idGasto)
        {
            BaseMethodOut result = new BaseMethodOut { OperationResult = OperationResult.Success };

            var gasto = obtenerGasto(idGasto);

            if (gasto != null)
            {
                gasto.activo = false;
                context.SaveChanges();

                return result;
            }

            result.OperationResult = OperationResult.Error;
            return result;
        }

        public BaseMethodOut modificarGasto(GastoVO gastoVO)
        {
            BaseMethodOut result = new BaseMethodOut { OperationResult = OperationResult.Success };

            var gasto = obtenerGasto(gastoVO.idGasto);

            if (gasto != null)
            {
                gasto.update(gastoVO);
                context.SaveChanges();

                return result;
            }

            result.OperationResult = OperationResult.Error;
            return result;
        }
    }
}
