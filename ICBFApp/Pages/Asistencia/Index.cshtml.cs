using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ICBFApp.Pages.Asistencia
{
    public class IndexModel : PageModel
    {
		public List<AsistenciaInfo> listAsistencia = new List<AsistenciaInfo>();
        
        public void OnGet()
        {
			{
				try
				{
					string connectionString = "Data Source=DESKTOP-O20N4T4\\SQLEXPRESS;Initial Catalog=ICBF1;Integrated Security=True";

					using (SqlConnection connection = new SqlConnection(connectionString))
					{
						connection.Open();
						string sqlSelect = "SELECT * FROM Asistencia";

						using (SqlCommand command = new SqlCommand(sqlSelect, connection))
						{
							using (SqlDataReader reader = command.ExecuteReader())
							{
								if (reader.HasRows)
								{
									while (reader.Read())
									{
										AsistenciaInfo asistenciaInfo = new AsistenciaInfo
										{
											Id = reader.GetInt32(0).ToString(),
											FK_niño = reader.GetString(1),
											Fecha = reader.GetDateTime(2),
											Estado = reader.GetString(3),
											

										};

										listAsistencia.Add(asistenciaInfo);
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

		public class AsistenciaInfo
		{
			public string Id { get; set; }
			public string FK_niño { get; set; }
			public DateTime Fecha { get; set; }
			public string Estado { get; set; }
		
		}

	}
}


     
        