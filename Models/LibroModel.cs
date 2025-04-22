namespace BibliotecaAPI.Models
{
    public class LibroModel
    {

        public int id_libro { get; set; }
        public string titulo { get; set; }
        public string autor { get; set; }
        public string editorial { get; set; }
        public string isbn { get; set; }
        public int anio { get; set; }
        public string categoria { get; set; }
        public int stock { get; set; }
    }

}
