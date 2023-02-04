using Microsoft.EntityFrameworkCore;

namespace chessAPI.models
{
    public class TeamDb:DbContext
    {
        public TeamDb(DbContextOptions<TeamDb> options) : base(options)
        {

        }
        public DbSet<Equipo> Equipos => Set<Equipo>();
    }
}
