using CadastroDeCampeonato.Models;

namespace CadastroDeCampeonato.Repositories.Interfaces
{
    public interface ICrudGenericRepository <T> 
    {
        Task Add(T Objeto);
        Task Update(T Objeto);
        Task Delete(T Objeto);
        Task<T> GetEntityById(int Id);
        Task<List<T>> List();
    }
}
