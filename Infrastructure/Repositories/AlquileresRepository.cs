using Dapper;
using Domain.Entities;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class AlquileresRepository : IAlquileresRepository
    {
        private readonly string _connectionString;

        public AlquileresRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Alquileres>> GetAll()
        {
            using var db = Connection;
            return await db.QueryAsync<Alquileres>("sp_ObtenerAlquileres", commandType: CommandType.StoredProcedure);
        }

        public async Task<Alquileres?> GetById(int id)
        {
            using var db = Connection;
            return await db.QueryFirstOrDefaultAsync<Alquileres>(
                "sp_ObtenerAlquilerPorId",
                new { ID_Alquiler = id },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> Create(Alquileres alquiler)
        {
            using var db = Connection;
            var result = await db.ExecuteAsync("sp_InsertarAlquiler",
                new
                {
                    alquiler.ID_Usuario,
                    alquiler.ID_Libro,
                    alquiler.Fecha_Alquiler,
                    alquiler.Fecha_Devolucion,                   
                    alquiler.Penalidad
                },
                commandType: CommandType.StoredProcedure);

            return result > 0;
        }

        public async Task<bool> Update(Alquileres alquiler)
        {
            using var db = Connection;
            var parameters = new DynamicParameters();
            parameters.Add("@ID_Alquiler", alquiler.ID_Alquiler);
            parameters.Add("@ID_Usuario", alquiler.ID_Usuario);
            parameters.Add("@ID_Libro", alquiler.ID_Libro);
            parameters.Add("@Fecha_Alquiler", alquiler.Fecha_Alquiler);
            parameters.Add("@Fecha_Devolucion", alquiler.Fecha_Devolucion);
            parameters.Add("@Estado", alquiler.Estado);
            parameters.Add("@Penalidad", alquiler.Penalidad);
            parameters.Add("@Resultado", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await db.ExecuteAsync("sp_ActualizarAlquiler", parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<bool>("@Resultado");
        }

        public async Task<bool> Delete(int id)
        {
            using var db = Connection;
            var parameters = new DynamicParameters();
            parameters.Add("@ID_Alquiler", id);
            parameters.Add("@Resultado", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await db.ExecuteAsync("sp_EliminarAlquiler", parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<bool>("@Resultado");
        }
    }
}
