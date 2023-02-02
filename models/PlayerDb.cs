using chessAPI.models.player;
using Microsoft.EntityFrameworkCore;

namespace chessAPI.models
{
    public class PlayerDb : DbContext
    {

        public PlayerDb(DbContextOptions<PlayerDb> options) : base(options)
        {

        }
        public DbSet<Jugador> players => Set<Jugador>();

        
    }
}
