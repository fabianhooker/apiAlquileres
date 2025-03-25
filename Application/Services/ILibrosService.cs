using Domain.Entities;

namespace Application.Services
{
    public interface ILibrosService
    {
        Task<IEnumerable<Libros>> GetAll();
        Task<Libros?> GetById(int id);
        Task<bool> Create(Libros libro);
        Task<bool> Update(Libros libro);
        Task<bool> Delete(int id);
    }
}
