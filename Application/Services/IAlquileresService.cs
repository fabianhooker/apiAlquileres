using Domain.Entities;

namespace Application.Services
{
    public interface IAlquileresService
    {
        Task<IEnumerable<Alquileres>> GetAll();
        Task<Alquileres?> GetById(int id);
        Task<bool> Create(Alquileres alquiler);
        Task<bool> Update(Alquileres alquiler);
        Task<bool> Delete(int id);
    }
}
