using System.ComponentModel.DataAnnotations;

namespace chessAPI.models
{
    public class Equipo
    {
        [Key]
        public int Id { get; set; }
        public int idjugador1 { get; set; }
        public string email1 { get; set; }
        public int idjugador2 { get; set; }
        public string email2 { get; set; }
        public int punteoequipo1 { get; set; }
        public int punteoequipo2 { get; set; }
        public int idjugador3 { get; set; }
        public string email3 { get; set; }
        public int idjugador4 { get; set; }
        public string email4 { get; set; }
    }
}
