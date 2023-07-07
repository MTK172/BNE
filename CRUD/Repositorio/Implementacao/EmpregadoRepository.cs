using CRUD.Models;
using CRUD.Repositorio.Contrato;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace CRUD.Repositorio.Implementacao
{
    public class EmpregadoRepository : IGenericRepository<Empregado>
    {
        private readonly string _CadeiaSQL = "";

        public EmpregadoRepository(IConfiguration configuration)
        {
            _CadeiaSQL = configuration.GetConnectionString("CadeiaSQL");
        }
        public async Task<List<Empregado>> Lista()
        {
            List<Empregado> _lista = new List<Empregado>();

            using (var connection = new SqlConnection(_CadeiaSQL))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_ListaEmpregados", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _lista.Add(new Empregado
                        {
                            idEmpregado = Convert.ToInt32(dr["idEmpregado"]),
                            nomeCompleto = dr["nomeCompleto"].ToString(),
                            refDepartamento = new Departamento()
                            {
                                idDepartamento = Convert.ToInt32(dr["idDepartamento"]),
                                nome = dr["nome"].ToString()
                            },
                            saldo = Convert.ToInt32(dr["saldo"]),
                            fechaContrato = dr["fechaContrato"].ToString(),
                        });
                    }
                }
            }

            return _lista;
        }
        public async Task<bool> Guardar(Empregado modelo)
        {
            using (var connection = new SqlConnection(_CadeiaSQL))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_GuardarEmpregado", connection);
                cmd.Parameters.AddWithValue("nomeCompleto", modelo.nomeCompleto);
                cmd.Parameters.AddWithValue("idDepartamento", modelo.refDepartamento.idDepartamento);
                cmd.Parameters.AddWithValue("saldo", modelo.saldo);
                cmd.Parameters.AddWithValue("fechaContrato", modelo.fechaContrato);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afetadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afetadas > 0)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public async Task<bool> Editar(Empregado modelo)
        {
            using (var connection = new SqlConnection(_CadeiaSQL))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_EditarEmpregado", connection);
                cmd.Parameters.AddWithValue("idEmpregado", modelo.idEmpregado);
                cmd.Parameters.AddWithValue("nomeCompleto", modelo.nomeCompleto);
                cmd.Parameters.AddWithValue("idDepartamento", modelo.refDepartamento.idDepartamento);
                cmd.Parameters.AddWithValue("saldo", modelo.saldo);
                cmd.Parameters.AddWithValue("fechaContrato", modelo.fechaContrato);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afetadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afetadas > 0)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            using (var connection = new SqlConnection(_CadeiaSQL))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_EliminarEmpregado", connection);
                cmd.Parameters.AddWithValue("idEmpregado", id);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afetadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afetadas > 0)
                {
                    return true;
                }
                else
                    return false;
            }
        }


    }
}
