using BCP_API_JM.Data;
using BCP.Shared.Models;
using BCP_API_JM.Repository.IRepository;
using static System.Net.Mime.MediaTypeNames;

namespace BCP_API_JM.Repository
{
    public class ClientesRepository : Repository<BD_CLIENTES>, IClientesRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BD_CLIENTES> Update(BD_CLIENTES entity)
        {
            entity.FechaActualizacion = DateTime.Now;
            _context.BD_CLIENTES_JM.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
