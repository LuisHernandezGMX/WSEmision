using System.Linq;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using WSEmision.Models.DAL.Entities;
using WSEmision.Models.DAL.DTO.Coaseguro;

namespace WSEmision.Models.DAL.DAO.Coaseguro
{
    /// <summary>
    /// Funciones de acceso a base de datos para el módulo de Coaseguro.
    /// </summary>
    public class CoaseguroDao
    {
        /// <summary>
        /// Ejecuta el procedimiento sp_CedulaParticipacionCoaseguro() y regresa
        /// toda la información de éste.
        /// </summary>
        /// <param name="idPv">El Id de la póliza a buscar.</param>
        /// <returns>Una nueva instancia de <see cref="CedulaParticipacionCoaseguroResultSet"/>
        /// con los datos requeridos.</returns>
        public static CedulaParticipacionCoaseguroResultSet ObtenerCedulaParticipacion(int idPv)
        {
            var rs = new CedulaParticipacionCoaseguroResultSet();
            var entorno = ConfigurationManager.AppSettings["EntornoBD"];

            using (var db = new EmisionContext(entorno)) {
                var cmd = db.Database.Connection.CreateCommand();
                var paramIdPv = cmd.CreateParameter();

                cmd.CommandText = "EXEC sp_CedulaParticipacionCoaseguro @IdPv";
                paramIdPv.ParameterName = "@IdPv";
                paramIdPv.Value = idPv;
                cmd.Parameters.Add(paramIdPv);

                try {
                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    var context = (db as IObjectContextAdapter).ObjectContext;

                    rs.DatosGenerales = context
                        .Translate<DatosGeneralesCedulaRS>(reader)
                        .FirstOrDefault() ?? new DatosGeneralesCedulaRS();

                    reader.NextResult();
                    rs.Coaseguradoras = context
                        .Translate<CoaseguradorasCedulaRS>(reader)
                        .ToList();
                } catch {
                    // TODO: Posible Log.
                    throw;
                } finally {
                    db.Database.Connection.Close();
                }
            }

            return rs;
        }

        /// <summary>
        /// Ejecuta el procedimiento sp_AnexoCondicionesParticularesCoaseguro() y
        /// regresa toda la información de éste.
        /// </summary>
        /// <param name="idPv">El Id de la póliza a buscar.</param>
        /// <returns>>Una nueva instancia de <see cref=""/>
        /// con los datos requeridos.</returns>
        public static AnexoCondicionesParticularesCoaseguroResultSet ObtenerAnexoCondicionesParticulares(int idPv)
        {
            var rs = new AnexoCondicionesParticularesCoaseguroResultSet();
            var entorno = ConfigurationManager.AppSettings["EntornoBD"];

            using (var db = new EmisionContext(entorno)) {
                var cmd = db.Database.Connection.CreateCommand();
                var paramIdPv = cmd.CreateParameter();

                cmd.CommandText = "EXEC sp_AnexoCondicionesParticularesCoaseguro @IdPv";
                paramIdPv.ParameterName = "@IdPv";
                paramIdPv.Value = idPv;
                cmd.Parameters.Add(paramIdPv);

                try {
                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    var context = (db as IObjectContextAdapter).ObjectContext;
                    
                    rs.DatosGenerales = context
                        .Translate<DatosGeneralesAnexoRS>(reader)
                        .FirstOrDefault() ?? new DatosGeneralesAnexoRS();

                    reader.NextResult();
                    rs.GMX = context
                        .Translate<GMXAnexoRS>(reader)
                        .FirstOrDefault() ?? new GMXAnexoRS();

                    reader.NextResult();
                    rs.Coaseguradoras = context
                        .Translate<CoaseguradorasAnexoRS>(reader)
                        .ToList();

                    reader.NextResult();
                    rs.DatosEspecificos = context
                        .Translate<DatosEspecificosAnexoRS>(reader)
                        .FirstOrDefault() ?? new DatosEspecificosAnexoRS();
                } catch {
                    // TODO: Posible log
                    throw;
                } finally {
                    db.Database.Connection.Close();
                }
            }

            return rs;
        }
    }
}