using Dapper;
using Manejo_de_gastos.Models;
using Microsoft.Data.SqlClient;

namespace Manejo_de_gastos.Servicios
{
    public interface IRepositorioTipoCuenta
    {
        Task Crear(TipoCuentaDTO tipoCuentaDTO);
        Task<bool> Existe(string nombre, int usuarioId);
    }


    public class RepositorioTipoCuenta:IRepositorioTipoCuenta
    {
        private readonly string connectionString;
        public RepositorioTipoCuenta(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(TipoCuentaDTO tipoCuentaDTO) 
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO TipoCuentas (Nombre, Orden, UsuarioId) 
                                                               VALUES (@Nombre, @Orden, @UsuarioId);
                                                               SELECT SCOPE_IDENTITY();", tipoCuentaDTO);

            tipoCuentaDTO.Id = id;

        }

        public async Task<bool> Existe(string nombre, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);

            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"select 1
                                                                  from TipoCuentas
                                                                  where Nombre = @Nombre AND UsuarioId= @UsuarioId",
                                                                          new { nombre, usuarioId });


            return existe == 1;
        }



    }
}
