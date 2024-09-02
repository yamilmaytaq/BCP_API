using BCP_API_JM.Models;

namespace BCP_API_JM.Repository.IRepository
{
    public interface IUsuariosRepository : IRepository<BD_USUARIOS>
    {
        Task<BD_USUARIOS> Update(BD_USUARIOS entity);
    }
}
