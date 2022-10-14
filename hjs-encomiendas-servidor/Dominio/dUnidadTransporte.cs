using hjs_encomiendas_servidor.Common;
using hjs_encomiendas_servidor.Common.ValueObjects;
using hjs_encomiendas_servidor.Common.ValueObjects.UnidadesTransporte;
using hjs_encomiendas_servidor.Common.ValueObjects.Usuarios;
using hjs_encomiendas_servidor.Dominio.Interfaces;
using hjs_encomiendas_servidor.Modelo;
using hjs_encomiendas_servidor.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace hjs_encomiendas_servidor.Dominio
{
    public class dUnidadTransporte : IDominio
    {
        private readonly ProjectContext context;

        public dUnidadTransporte(ProjectContext _context)
        {
            context = _context;
        }

        public BaseMethodOut? agregarUnidadTransporte(UnidadTransporteVO unidadTransporteVO)
        {
            UnidadTransporte unidadTransporte = new UnidadTransporte(unidadTransporteVO);

            context.UnidadTransporte.Add(unidadTransporte);

            context.SaveChanges();

            return new BaseMethodOut { OperationResult = OperationResult.Success };
        }

        public UnidadesTransporteVO obtenerUnidadesTransportes(GetDataInVO getData)
        {
            var qry = (from u in context.UnidadTransporte where u.activo == true select u);
            var count = qry.Count();
            var transportes = qry.OrderBy(u => u.idUnidadTransporte)
                .Skip(getData.PageIndex)
                .Take(getData.PageSize)
                .Include(u => u.chofer)
                .ToList();

            UnidadesTransporteVO usuariosVO = new UnidadesTransporteVO { unidadesTransporte = transportes, totalRows = count, OperationResult = OperationResult.Success };


            return usuariosVO;
        }

        public UnidadTransporte? obtenerUnidadTransporte(int idUnidadTransporte)
        {
            var unidadTransporte = context.UnidadTransporte.Where(u => u.idUnidadTransporte == idUnidadTransporte && u.activo == true).FirstOrDefault();

            return unidadTransporte;
        }

        public UnidadTransporte? obtenerUnidadTransporteDeChofer(int idChofer)
        {
            var unidadTransporte = context.UnidadTransporte.Where(u => u.idChofer == idChofer && u.activo == true).FirstOrDefault();

            return unidadTransporte;
        }

        public BaseMethodOut modificarUnidad(UnidadTransporteVO unidadTransporteVO)
        {
            BaseMethodOut result = new BaseMethodOut { OperationResult = OperationResult.Success };

            var unidad = obtenerUnidadTransporte(unidadTransporteVO.idUnidadTransporte);

            if (unidad != null)
            {
                unidad.update(unidadTransporteVO);
                context.SaveChanges();

                return result;
            }

            result.OperationResult = OperationResult.Error;
            return result;
        }

        public BaseMethodOut eliminarUnidadTransporte(int idUnidadTransporte)
        {
            BaseMethodOut result = new BaseMethodOut { OperationResult = OperationResult.Success };

            var unidadTransporte = obtenerUnidadTransporte(idUnidadTransporte);

            if (unidadTransporte != null)
            {
                unidadTransporte.activo = false;
                context.SaveChanges();

                return result;
            }

            result.OperationResult = OperationResult.Error;
            return result;
        }
    }
}
