namespace BibliotecaAPI.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Tipo_usuario { get; set; }
        public string Clave { get; set; }

        //public DateTime? fecha_adicion { get; set; }
        //public string? adicionado_por { get; set; }
        //public DateTime? fecha_modificacion { get; set; }
        //public string? modificado_por { get; set; }

    }
}