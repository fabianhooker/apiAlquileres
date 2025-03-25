using Domain.Entities;
using Infrastructure.Repositories;
using System.ComponentModel.DataAnnotations;
namespace Application.Services
{
    public class LibrosService: ILibrosService
    {
        private readonly ILibrosRepository _librosRepository;


        public LibrosService(ILibrosRepository libroRepository)
        {
            _librosRepository = libroRepository;

        }

        public async Task<IEnumerable<Libros>> GetAll()
        {
            return await _librosRepository.GetAll();
        }

        public async Task<Libros?> GetById(int id)
        {
            return await _librosRepository.GetById(id);
        }

        public async Task<bool> Create(Libros libro)
        {
        

            return await _librosRepository.Create(libro);
        }

        public async Task<bool> Update(Libros libro)
        {   

            return await _librosRepository.Update(libro);
        }

        public async Task<bool> Delete(int id)
        {
            return await _librosRepository.Delete(id);
        }
    }
}
