using System.ComponentModel.DataAnnotations;

namespace chessAPI.models
{
    public class Juego
    {

        [Key]
        public int Id { get; set; }
        public int idhome { get; set; }
        public string emailhome { get; set; }
        public int punteohome { get; set; }
        public int punteoaway { get; set; }
        public int idaway { get; set; }
        public string emailaway { get; set; }
    }
}
