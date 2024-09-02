using BCP_API_JM.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace BCP_API_JM.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<BD_CLIENTES> BD_CLIENTES_JM { get; set; }
        public DbSet<BD_USUARIOS> BD_USUARIOS_JM { get; set; }
    }
}
