namespace BibliotecaAPI.Models
{
    public class UsuarioModel
    {
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string tipo_usuario { get; set; }
        public string clave { get; set; }

        //public DateTime? fecha_adicion { get; set; }
        //public string? adicionado_por { get; set; }
        //public DateTime? fecha_modificacion { get; set; }
        //public string? modificado_por { get; set; }

    }
}