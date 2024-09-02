using BCP_API_JM.Models;
using static System.Net.Mime.MediaTypeNames;

namespace BCP_API_JM.Repository.IRepository
{
    public interface IClientesRepository : IRepository<BD_CLIENTES>
    {
        Task<BD_CLIENTES> Update(BD_CLIENTES entity);

    }
}
