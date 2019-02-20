using System.Data;

using WSEmision.Models.DAL.DTO.CaratulaDanos;

namespace WSEmision.Models.DAL.DAO.CaratulaDanos
{
    /// <summary>
    /// Funciones de acceso a base de datos para el módulo de Carátula de Daños.
    /// </summary>
    public class CaratulaDanosDao : EmisionDao
    {
        public CaratulaDanosResultSet ObtenerCaratulaDanos(int idPv)
        {
            var rs = new CaratulaDanosResultSet();
            var cmd = db.Database.Connection.CreateCommand();
            var paramIdPv = cmd.CreateParameter();

            cmd.CommandText = "EXEC sp_pp_caratula_danos @IdPv";
            paramIdPv.ParameterName = "@IdPv";
            paramIdPv.Value = idPv;
            cmd.Parameters.Add(paramIdPv);

            try {
                db.Database.Connection.Open();
                var reader = cmd.ExecuteReader(); // Obtiene el primer RS ([IMP41], [IVA41])
                var auxTable = new DataTable();

                reader.NextResult(); // Se obtiene el siguiente RS ([PREC], [PIVA], [P41])
                reader.NextResult(); // Se obtiene el siguiente RS ([FRACC], [IMPORTEFRACC], [PRIMA], [GASTOS], [IMPORTEIVA], [TOTAL], [SUMA_ASEG], [TOTALPRIMA], [ETIQUETA])
                rs.Importes = ObtenerImportes(reader);

                reader.NextResult(); // Se obtiene el siguiente RS ([PAQUETE])

                // Se obtiene el siguiente RS ([LUGAR])
                reader.NextResult();
                auxTable.Load(reader);
                rs.Oficina = auxTable.Rows[0]["LUGAR"] as string;

                // Se obtiene el siguiente RS ([DESC_POR_RAMO], [cod_tipo_poliza], [cod_tipo_poliza], [cod_desc])
                auxTable.Reset();
                auxTable.Load(reader);
                rs.DescPorRamo = auxTable.Rows[0]["DESC_POR_RAMO"] as string;

                // Se obtiene información adicional acerca de la póliza y el footer del documento.
                AgregarInfoAdicionalYFooter(reader, ref rs);

                // Se agrega la información de la vigencia de la póliza.
                AgregarVigencia(reader, ref rs);

                // Se agrega la información del asegurado.
                AgregarAsegurado(reader, ref rs);

                // Se obtiene al agente.
                reader.NextResult();
                auxTable.Reset();
                auxTable.Load(reader);
                rs.Agentes = auxTable.Rows[0]["AGENTES"] as string;

                // Se obtiene la fecha de emisión de la póliza.
                reader.NextResult();
                reader.NextResult();
                auxTable.Reset();
                auxTable.Load(reader);
                rs.InfoPoliza.Dia = auxTable.Rows[0]["DIA"] as string;
                rs.InfoPoliza.Mes = auxTable.Rows[0]["MES"] as string;
                rs.InfoPoliza.Ano = (int)auxTable.Rows[0]["ANO"];
            } catch {
                // TODO: Posible log.
                throw;
            } finally {
                db.Database.Connection.Close();
            }

            return rs;
        }

        /// <summary>
        /// Obtiene la información de los importes de la carátula.
        /// </summary>
        /// <param name="reader">El lector obtenido al ejecutar el procedimiento almacenado.</param>
        /// <returns>Una nueva instancia de <see cref="ImportesRS"/>.</returns>
        private ImportesRS ObtenerImportes(IDataReader reader)
        {
            var rs = new ImportesRS();
            var table = new DataTable();
            table.Load(reader);

            var row = table.Rows[0];
            rs.PrimaNeta = row["PRIMA"] as string;
            rs.Recargo = row["IMPORTEFRAC"] as string;
            rs.Derecho = row["GASTOS"] as string;
            rs.IVA = row["IMPORTEIVA"] as string;
            rs.Total = row["TOTAL"] as string;

            return rs;
        }

        /// <summary>
        /// Agrega información adicional de la póliza y el footer a la carátula de daños.
        /// </summary>
        /// <param name="reader">El lector obtenido al ejecutar el procedimiento almacenado.</param>
        /// <param name="caratula">La carátula de daños.</param>
        private void AgregarInfoAdicionalYFooter(IDataReader reader, ref CaratulaDanosResultSet caratula)
        {
            var table = new DataTable();
            table.Load(reader);

            var row = table.Rows[0];
            caratula.Footer = row["FOOTER_ID_PV_BARRAS"] as string;

            caratula.InfoPoliza = new InfoPolizaRS {
                Vigencia = (double)row["VIGENCIA"],
                FormaPago = row["PAGO"] as string,
                Moneda = row["MONEDA"] as string
            };
        }

        /// <summary>
        /// Agrega información sobre las fechas de inicio y vigencia de la póliza.
        /// </summary>
        /// <param name="reader">El lector obtenido al ejecutar el procedimiento almacenado.</param>
        /// <param name="caratula">La carátula de daños.</param>
        private void AgregarVigencia(IDataReader reader, ref CaratulaDanosResultSet caratula)
        {
            var auxTable = new DataTable();

            // Se agregan las fechas y hora de inicio de vigencia de la póliza.
            auxTable.Reset();
            auxTable.Load(reader);
            caratula.InfoPoliza.Dia1 = auxTable.Rows[0]["DIA1"] as string;
            caratula.InfoPoliza.Mes1 = auxTable.Rows[0]["MES1"] as string;
            caratula.InfoPoliza.Ano1 = (int)auxTable.Rows[0]["ANO1"];

            auxTable.Reset();
            auxTable.Load(reader);
            caratula.InfoPoliza.HoraDesde = auxTable.Rows[0]["HORADESDE"] as string;

            // Se agregan las fechas y hora de fin de vigencia de la póliza.
            auxTable.Reset();
            auxTable.Load(reader);
            caratula.InfoPoliza.Dia2 = auxTable.Rows[0]["DIA2"] as string;
            caratula.InfoPoliza.Mes2 = auxTable.Rows[0]["MES2"] as string;
            caratula.InfoPoliza.Ano2 = (int)auxTable.Rows[0]["ANO2"];

            auxTable.Reset();
            auxTable.Load(reader);
            caratula.InfoPoliza.HoraHasta = auxTable.Rows[0]["HORAHASTA"] as string;
        }

        /// <summary>
        /// Agrega la información del asegurado a la carátula.
        /// </summary>
        /// <param name="reader">El lector obtenido al ejecutar el procedimiento almacenado.</param>
        /// <param name="caratula">La carátula de daños.</param>
        private void AgregarAsegurado(IDataReader reader, ref CaratulaDanosResultSet caratula)
        {
            var table = new DataTable();
            table.Load(reader);
            caratula.Asegurado = new AseguradoRS();

            caratula.Asegurado.Contratante = table.Rows[0]["NOMBRE"] as string;
            caratula.Asegurado.RFC = table.Rows[0]["RFC"] as string;

            table.Reset();
            table.Load(reader);
            caratula.Asegurado.FechaNacimiento = table.Rows[0]["FEC_NACIMIENTO"] as string;

            // Se saltan los resultados no relevantes (5 consultas del SP)
            reader.NextResult();
            reader.NextResult();
            reader.NextResult();
            reader.NextResult();
            reader.NextResult();

            table.Reset();
            table.Load(reader);
            caratula.Asegurado.Domicilio = new DomicilioRS {
                Calle = table.Rows[0]["CALLE"] as string,
                Poblacion = table.Rows[0]["POBLACION"] as string,
                Colonia = table.Rows[0]["COLONIA"] as string,
                CP = table.Rows[0]["CP"] as string,
                Ciudad = table.Rows[0]["CIUDAD"] as string,
                Estado = table.Rows[0]["ESTADO"] as string,
                Numero = table.Rows[0]["NUMERO"] as string,
                Interior = table.Rows[0]["INTERIOR"] as string
            };
        }
    }
}