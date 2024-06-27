using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace ICBFApp.Pages.Acudiente
{
    public class IndexModel : PageModel
    {
		public List<AcudienteInfo> listAcudiente = new List<AcudienteInfo>();
		public void OnGet()
        {
			{
				try
				{
					string connectionString = "Data Source=DESKTOP-O20N4T4\\SQLEXPRESS;Initial Catalog=ICBF1;Integrated Security=True";

					using (SqlConnection connection = new SqlConnection(connectionString))
					{
						connection.Open();
						string sqlSelect = "SELECT * FROM Acudiente";

						using (SqlCommand command = new SqlCommand(sqlSelect, connection))
						{
							using (SqlDataReader reader = command.ExecuteReader())
							{
								if (reader.HasRows)
								{
									while (reader.Read())
									{
										AcudienteInfo acudienteInfo = new AcudienteInfo
										{
											Id_acudiente = reader.GetInt32(0).ToString(),
											Nombre = reader.GetString(1),
											Cedula = reader.GetInt32(2).ToString(),
											Telefono = reader.GetInt32(3).ToString(),
											Direccion = reader.GetString(4),
											

										};

										listAcudiente.Add(acudienteInfo);
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

		public class AcudienteInfo
		{
			public string Id_acudiente { get; set; }
			public string Nombre { get; set; }
			public string Cedula { get; set; }
			public string Telefono { get; set; }
			public string Direccion { get; set; }
		}

	}

}
    
