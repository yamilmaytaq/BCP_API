using BCP_API_JM.Data;
using BCP_API_JM.Models;
using BCP_API_JM.Repository.IRepository;

namespace BCP_API_JM.Repository
{
    public class UsuariosRepository : Repository<BD_USUARIOS>, IUsuariosRepository
        {
            private readonly ApplicationDbContext _context;

            public UsuariosRepository(ApplicationDbContext context) : base(context)
            {
                _context = context;
            }

            public async Task<BD_USUARIOS> Update(BD_USUARIOS entity)
            {
                entity.FechaActualizacion = DateTime.Now;
                _context.BD_USUARIOS_JM.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
        }
    }

