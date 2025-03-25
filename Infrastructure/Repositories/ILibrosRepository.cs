using Domain.Entities;


namespace Infrastructure.Repositories
{
    public interface ILibrosRepository
    {
        Task<IEnumerable<Libros>> GetAll();
        Task<Libros?> GetById(int id);
        Task<bool> Create(Libros libro);
        Task<bool> Update(Libros libro);
        Task<bool> Delete(int id);
    }
}
