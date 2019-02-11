using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using WSEmision.Models.DAL.DTO.Coaseguro;
using WSEmision.Models.DAL.Entities;
using WSEmision.Models.DAL.ViewModels.Coaseguro;

namespace WSEmision.Models.DAL.DAO.Coaseguro
{
    /// <summary>
    /// Funciones de acceso a base de datos para el módulo de Coaseguro.
    /// </summary>
    public class CoaseguroDao
    {
        private static string entorno = ConfigurationManager.AppSettings["EntornoBD"];

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

                    // *** Posibles efectos secundarios
                    // Translate<T>() por alguna razón no obtiene el campo 'Vigencia' del DbDataReader.
                    // Por lo tanto, se obtiene el valor manualmente y se asigna al result set.
                    rs.DatosGenerales.FechaVigencia = reader["Vigencia"] as System.DateTime?;

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

        /// <summary>
        /// Regresa una lista de los ramos comerciales (1 - 102) para
        /// ser incluida en una etiqueta HTML &lt;select&gt;.
        /// </summary>
        /// <returns>Una colección de objectos <see cref="SelectListItem"/></returns>
        public static IEnumerable<SelectListItem> ObtenerRamosComerciales()
        {
            using (var db = new EmisionContext(entorno)) {
                return db.tramo
                    .Where(ramo => ramo.cod_ramo <= 102M)
                    .AsEnumerable()
                    .Select(ramo => new SelectListItem {
                        Text = $"{ramo.txt_desc} ({ramo.cod_ramo})",
                        Value = ramo.cod_ramo.ToString()
                    })
                    .ToList();
            }
        }

        /// <summary>
        /// Regresa una lista con todas las sucursales de GMX para ser incluida
        /// en una etiqueta HTML &lt;select&gt;.
        /// </summary>
        /// <returns>Una colección de objetos <see cref="SelectListItem"/></returns>
        public static IEnumerable<SelectListItem> ObtenerSucursales()
        {
            using (var db = new EmisionContext(entorno)) {
                return db.tsuc
                    .AsEnumerable()
                    .Select(tsuc => new SelectListItem {
                        Text = $"{tsuc.txt_nom_suc} ({tsuc.cod_suc})",
                        Value = tsuc.cod_suc.ToString()
                    })
                    .ToList();
            }
        }

        /// <summary>
        /// Indica si existe el registro en la tabla [pv_header] con la
        /// llave primaria indicada por el Vista Modelo.
        /// </summary>
        /// <param name="model">El Vista Modelo con las llaves primarias del registro a buscar.</param>
        /// <returns>True si el registro existe con esas llaves primarias. De lo contrario, regresa False.</returns>
        public static bool ExistePolizaCoaseguro(ConsultarPolizaViewModel model)
        {
            using (var db = new EmisionContext(entorno)) {
                return db.pv_header
                    .Any(header =>
                        header.cod_suc == model.CodSucursal
                        && header.cod_ramo == model.CodRamo
                        && header.nro_pol == model.NroPoliza
                        && header.aaaa_endoso == model.Sufijo
                        && header.nro_endoso == model.Endoso);
            }
        }

        /// <summary>
        /// Regresa el [id_pv] de la póliza con los parámetros indicados.
        /// </summary>
        /// <param name="model">El Vista Modelo con las llaves primarias del registro a buscar.</param>
        /// <returns>El [id_pv] de la póliza indicada.</returns>
        public static int ObtenerIdPv(ConsultarPolizaViewModel model)
        {
            using (var db = new EmisionContext(entorno)) {
                return db
                    .pv_header
                    .FirstOrDefault(header =>
                        header.cod_suc == model.CodSucursal
                        && header.cod_ramo == model.CodRamo
                        && header.nro_pol == model.NroPoliza
                        && header.aaaa_endoso == model.Sufijo
                        && header.nro_endoso == model.Endoso)
                    .id_pv;
            }
        }
    }
}