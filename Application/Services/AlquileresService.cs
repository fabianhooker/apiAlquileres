using Domain.Entities;
using Infrastructure.Repositories;


namespace Application.Services
{
    public class AlquileresService : IAlquileresService
    {
        private readonly IAlquileresRepository _alquilerRepository;


        public AlquileresService(IAlquileresRepository alquilerRepository)
        {
            _alquilerRepository = alquilerRepository;
   
        }

        public async Task<IEnumerable<Alquileres>> GetAll()
        {
            return await _alquilerRepository.GetAll();
        }

        public async Task<Alquileres?> GetById(int id)
        {
            return await _alquilerRepository.GetById(id);
        }

        public async Task<bool> Create(Alquileres alquiler)
        {        
          
            return await _alquilerRepository.Create(alquiler);
        }

        public async Task<bool> Update(Alquileres alquiler)
        {
          
            return await _alquilerRepository.Update(alquiler);
        }

        public async Task<bool> Delete(int id)
        {
            return await _alquilerRepository.Delete(id);
        }
    }
}
