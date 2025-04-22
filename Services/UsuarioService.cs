using System.Data.SqlClient;
using System.Data;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Services
{
    public class UsuarioService
    {
        private readonly string _connectionString;

        public UsuarioService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MiConexion");
        }

        public async Task<UsuarioModel> ValidarUsuarioAsync(string correo, string clave)
        {
            UsuarioModel usuario = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("ValidarUsuario", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Clave", clave);

                await con.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        usuario = new UsuarioModel
                        {
                            id_usuario = (int)reader["Id"],
                            nombre = reader["Nombre"].ToString(),
                            apellido = reader["Apellido"].ToString(),
                            correo = reader["Correo"].ToString(),
                            telefono = reader["Telefono"].ToString(),
                            tipo_usuario = reader["TipoUsuario"].ToString(),
                            clave = reader["Clave"].ToString()
                        };
                    }
                }
            }

            return usuario;
        }

        public async Task<UsuarioModel> ObtenerUsuarioPorIdAsync(int id)
        {
            UsuarioModel usuario = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("ObtenerUsuarios", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                await con.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        if ((int)reader["Id"] == id)
                        {
                            usuario = new UsuarioModel
                            {
                                id_usuario = (int)reader["Id"],
                                nombre = reader["Nombre"].ToString(),
                                apellido = reader["Apellido"].ToString(),
                                correo = reader["Correo"].ToString(),
                                telefono = reader["Telefono"].ToString(),
                                tipo_usuario = reader["TipoUsuario"].ToString(),
                                clave = reader["Clave"].ToString()
                            };
                            break;
                        }
                    }
                }
            }

            return usuario;
        }

        public async Task<List<UsuarioModel>> ObtenerUsuariosAsync()
        {
            var usuarios = new List<UsuarioModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("ObtenerUsuarios", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                await con.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        usuarios.Add(new UsuarioModel
                        {
                            id_usuario = (int)reader["Id"],
                            nombre = reader["Nombre"].ToString(),
                            apellido = reader["Apellido"].ToString(),
                            correo = reader["Correo"].ToString(),
                            telefono = reader["Telefono"].ToString(),
                            tipo_usuario = reader["TipoUsuario"].ToString(),
                            clave = reader["Clave"].ToString()
                        });
                    }
                }
            }

            return usuarios;
        }

        public async Task CrearUsuarioAsync(UsuarioModel usuario)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("InsertarUsuario", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", usuario.nombre);
                cmd.Parameters.AddWithValue("@Apellido", usuario.apellido);
                cmd.Parameters.AddWithValue("@Correo", usuario.correo);
                cmd.Parameters.AddWithValue("@Telefono", usuario.telefono);
                cmd.Parameters.AddWithValue("@TipoUsuario", usuario.tipo_usuario);
                cmd.Parameters.AddWithValue("@Clave", usuario.clave);

                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<bool> ActualizarUsuarioAsync(int id, UsuarioModel usuario)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("ActualizarUsuario", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Nombre", usuario.nombre);
                cmd.Parameters.AddWithValue("@Apellido", usuario.apellido);
                cmd.Parameters.AddWithValue("@Correo", usuario.correo);
                cmd.Parameters.AddWithValue("@Telefono", usuario.telefono);
                cmd.Parameters.AddWithValue("@TipoUsuario", usuario.tipo_usuario);
                cmd.Parameters.AddWithValue("@Clave", usuario.clave);

                await con.OpenAsync();
                int rows = await cmd.ExecuteNonQueryAsync();
                return rows > 0;
            }
        }

        public async Task<bool> EliminarUsuarioAsync(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("EliminarUsuario", con))
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
