using Dapper;
using Domain.Entities;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class LibrosRepository : ILibrosRepository
    {
        private readonly string _connectionString;

        public LibrosRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Libros>> GetAll()
        {
            using var db = Connection;
            return await db.QueryAsync<Libros>("sp_ObtenerLibros", commandType: CommandType.StoredProcedure);
        }

        public async Task<Libros?> GetById(int id)
        {
            using var db = Connection;
            return await db.QueryFirstOrDefaultAsync<Libros>(
                "sp_ObtenerLibroPorId",
                new { ID_Libro = id },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> Create(Libros libro)
        {
            using var db = Connection;
            var result = await db.ExecuteAsync("sp_InsertarLibro",
                new
                {
                    libro.Titulo,
                    libro.Autor,
                    libro.Categoria,
                    libro.Editorial,
                    libro.Anio_Publicacion,
                    libro.ISBN,
                    libro.Precio_Venta,
                    libro.Estado
                },
                commandType: CommandType.StoredProcedure);
            return result > 0;
        }

        public async Task<bool> Update(Libros libro)
        {
            using var db = Connection;
            var parameters = new DynamicParameters();
            parameters.Add("@ID_Libro", libro.ID_Libro);
            parameters.Add("@Titulo", libro.Titulo);
            parameters.Add("@Autor", libro.Autor);
            parameters.Add("@Categoria", libro.Categoria);
            parameters.Add("@Editorial", libro.Editorial);
            parameters.Add("@Anio_Publicacion", libro.Anio_Publicacion);
            parameters.Add("@ISBN", libro.ISBN);
            parameters.Add("@Precio_Venta", libro.Precio_Venta);
            parameters.Add("@Estado", libro.Estado);
            parameters.Add("@Resultado", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await db.ExecuteAsync("sp_ActualizarLibro", parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<bool>("@Resultado");
        }

        public async Task<bool> Delete(int id)
        {
            using var db = Connection;
            var parameters = new DynamicParameters();
            parameters.Add("@ID_Libro", id);
            parameters.Add("@Resultado", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await db.ExecuteAsync("sp_EliminarLibro", parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<bool>("@Resultado");
        }
    }
}
