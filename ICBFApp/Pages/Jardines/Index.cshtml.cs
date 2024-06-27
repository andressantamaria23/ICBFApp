using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ICBFApp.Pages.Jardines
{
	public class IndexModel : PageModel
	{
		public List<JardinInfo> listJardin = new List<JardinInfo>();

		public void OnGet()
		{
			try
			{
				string connectionString = "Data Source=DESKTOP-O20N4T4\\SQLEXPRESS;Initial Catalog=ICBF1;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					string sqlSelect = "SELECT j.Id_jardin, j.Nombre, j.Direccion, j.Estado, u.nom_usuario AS nom_usuario FROM JARDINES j JOIN Usuario u ON j.Fk_usuario = u.Id_usuario;\r\n";

					using (SqlCommand command = new SqlCommand(sqlSelect, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.HasRows)
							{
								while (reader.Read())
								{
									JardinInfo jardinInfo = new JardinInfo
									{
										Id_jardin = reader.GetInt32(0).ToString(),
										Nombre = reader.GetString(1),
										Direccion = reader.GetString(2),
										Estado = reader.GetString(3),
										Fk_usuario = reader.GetString(4),
								
									};

									listJardin.Add(jardinInfo);
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

		public class JardinInfo
		{
			public string Id_jardin { get; set; }
			public string Nombre { get; set; }
			public string Direccion { get; set; }
			public string Estado { get; set; }
			public string Fk_usuario { get; set; }
		}
	}
}
