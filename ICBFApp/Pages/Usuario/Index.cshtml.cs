using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ICBFApp.Pages.Usuario
{
    public class IndexModel : PageModel
    {
		public List<UsuarioInfo> listUsuario = new List<UsuarioInfo>();
		public void OnGet()
		{
			{
				try
				{
					string connectionString = "Data Source=DESKTOP-O20N4T4\\SQLEXPRESS;Initial Catalog=ICBF1;Integrated Security=True";

					using (SqlConnection connection = new SqlConnection(connectionString))
					{
						connection.Open();
						string sqlSelect = "SELECT * FROM Usuario";

						using (SqlCommand command = new SqlCommand(sqlSelect, connection))
						{
							using (SqlDataReader reader = command.ExecuteReader())
							{
								if (reader.HasRows)
								{
									while (reader.Read())
									{
										UsuarioInfo usuarioInfo = new UsuarioInfo
										{
											Id_usuario = reader.GetInt32(0).ToString(),
											nom_usuario = reader.GetString(1),
											apell_usuario = reader.GetString(2),
											email = reader.GetString(3),
											Contrasena = reader.GetString(4),
											Rol = reader.GetString(5),

										};

										listUsuario.Add(usuarioInfo);
									}
								}
								else
								{
									Console.WriteLine("No hay filas en el resultado");
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Exception: " + ex.ToString());
				}
			}
		}

		public class UsuarioInfo
		{
			public string Id_usuario { get; set; }
			public string nom_usuario { get; set; }
			public string apell_usuario { get; set; }
			public string email { get; set; }
			public string Contrasena { get; set; }
			public string Rol { get; set; }
		}
        
        }
    }


     