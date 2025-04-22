using BibliotecaAPI.Models;
using System.Data.SqlClient;
using System.Data;

namespace BibliotecaAPI.Services
{
    public class LibroService
    {
        private readonly string _connectionString;

        public LibroService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MiConexion");
        }

        public async Task<List<LibroModel>> ObtenerLibrosAsync()
        {
            var libros = new List<LibroModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("ObtenerLibros", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                await con.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        libros.Add(new LibroModel
                        {
                            id_libro = (int)reader["Id"],
                            titulo = reader["Titulo"].ToString(),
                            autor = reader["Autor"].ToString(),
                            editorial = reader["Editorial"].ToString(),
                            isbn = reader["ISBN"].ToString(),
                            anio = (int)reader["Anio"],
                            categoria = reader["Categoria"].ToString(),
                            stock = (int)reader["Existencias"]
                        });
                    }
                }
            }

            return libros;
        }

        public async Task<LibroModel> ObtenerLibroPorIdAsync(int id)
        {
            LibroModel libro = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("ObtenerLibros", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                await con.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        if ((int)reader["Id"] == id)
                        {
                            libro = new LibroModel
                            {
                                id_libro = (int)reader["Id"],
                                titulo = reader["Titulo"].ToString(),
                                autor = reader["Autor"].ToString(),
                                editorial = reader["Editorial"].ToString(),
                                isbn = reader["ISBN"].ToString(),
                                anio = (int)reader["Anio"],
                                categoria = reader["Categoria"].ToString(),
                                stock = (int)reader["Existencias"]
                            };
                            break;
                        }
                    }
                }
            }

            return libro;
        }

        public async Task CrearLibroAsync(LibroModel libro)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("InsertarLibro", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Titulo", libro.titulo);
                cmd.Parameters.AddWithValue("@Autor", libro.autor);
                cmd.Parameters.AddWithValue("@Editorial", libro.editorial);
                cmd.Parameters.AddWithValue("@ISBN", libro.isbn);
                cmd.Parameters.AddWithValue("@Anio", libro.anio);
                cmd.Parameters.AddWithValue("@Categoria", libro.categoria);
                cmd.Parameters.AddWithValue("@Existencias", libro.stock);

                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<bool> ActualizarLibroAsync(int id, LibroModel libro)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("ActualizarLibro", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Titulo", libro.titulo);
                cmd.Parameters.AddWithValue("@Autor", libro.autor);
                cmd.Parameters.AddWithValue("@Editorial", libro.editorial);
                cmd.Parameters.AddWithValue("@ISBN", libro.isbn);
                cmd.Parameters.AddWithValue("@Anio", libro.anio);
                cmd.Parameters.AddWithValue("@Categoria", libro.categoria);
                cmd.Parameters.AddWithValue("@Existencias", libro.stock);

                await con.OpenAsync();
                int rows = await cmd.ExecuteNonQueryAsync();
                return rows > 0;
            }
        }

        public async Task<bool> EliminarLibroAsync(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("EliminarLibro", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                await con.OpenAsync();
                int rows = await cmd.ExecuteNonQueryAsync();
                return rows > 0;
            }
        }
    }
}