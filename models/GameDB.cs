using Microsoft.EntityFrameworkCore;

namespace chessAPI.models
{
    public class GameDB:DbContext
    {
        public GameDB(DbContextOptions<GameDB> options) : base(options)
        {

        }
        
        public DbSet<Juego> partido => Set<Juego>();
    }
}
