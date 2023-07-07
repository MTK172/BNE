using CRUD.Models;
using CRUD.Repositorio.Contrato;
using System.Data;
using System.Data.SqlClient;

namespace CRUD.Repositorio.Implementacao
{
    public class DepartamentoRepository : IGenericRepository<Departamento>
    {
        private readonly string _CadeiaSQL = "";

        public DepartamentoRepository(IConfiguration configuration)
        {
            _CadeiaSQL = configuration.GetConnectionString("CadeiaSQL");
        }

        public  async Task<List<Departamento>> Lista()
        {
            List<Departamento> _lista = new List<Departamento>();

            using (var connection = new SqlConnection(_CadeiaSQL))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_ListaDepartamentos", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _lista.Add(new Departamento
                        {
                            idDepartamento = Convert.ToInt32(dr["idDepartamento"]),
                            nome = dr["nome"].ToString()
                        });
                    }
                }
            }

            return _lista;
        }

        public Task<bool> Editar(Departamento modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Guardar(Departamento modelo)
        {
            throw new NotImplementedException();
        }
    }
}
