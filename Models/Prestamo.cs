namespace BibliotecaAPI.Models
{
    public class Prestamo
    {
        public int id_prestamo { get; set; }
        public int id_usuario { get; set; }
        public int id_libro { get; set; }
        public DateTime fecha_prestamo { get; set; }
        public DateTime fecha_devolucion_esperada { get; set; }
        public DateTime? fecha_devolucion_real { get; set; }
        public string estado { get; set; }
    }
}