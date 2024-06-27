using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ICBFApp.Pages.MadreComunitaria
{
    public class IndexModel : PageModel
    {
        public List<MadreComunitariaInfo> listMadre = new List<MadreComunitariaInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-O20N4T4\\SQLEXPRESS;Initial Catalog=ICBF1;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlSelect = "SELECT * FROM MadreComunitaria";

                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MadreComunitariaInfo madreComunitariaInfo = new MadreComunitariaInfo
                                {
                                    Id_Madre = reader.GetInt32(0).ToString(),
                                    Nombres = reader.GetString(1),
                                    FechaNacimiento = reader.GetDateTime(2),
                                    Telefono = reader.GetInt32(3).ToString(),
                                    Direccion = reader.GetString(4)
                                };

                                listMadre.Add(madreComunitariaInfo);
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

        public class MadreComunitariaInfo
        {
            public string Id_Madre { get; set; }
            public string Nombres { get; set; }
            public DateTime FechaNacimiento { get; set; }
            public string Telefono { get; set; }
            public string Direccion { get; set; }
        }
    }
}
