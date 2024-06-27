using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static ICBFApp.Pages.Niño.IndexModel;

namespace ICBFApp.Pages.Niño
{
    public class IndexModel : PageModel
    {
        public List<NiñoInfo> listniño = new List<NiñoInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-O20N4T4\\SQLEXPRESS;Initial Catalog=ICBF1;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlSelect =
                   "SELECT n.Id_niño, n.nom_niño, n.RH, n.CiudadNacimiento, n.Telefono, n.Direccion, n.EPS, a.Nombre AS NombreAcudiente, j.Nombre AS NombreJardin, m.Nombres AS NombreMadre FROM Niño n JOIN Acudiente a ON n.FK_Acudiente = a.Id_Acudiente JOIN Jardines j ON n.FK_Jardin = j.Id_jardin JOIN MadreComunitaria m ON n.FK_Madre = m.Id_Madre;";

                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    
                                    {
                                        NiñoInfo niñoInfo = new NiñoInfo();
                                        niñoInfo.Id_niño = reader.GetInt32(0).ToString();
                                        niñoInfo.nom_niño = reader.GetString(1);
  
                                        niñoInfo.RH = reader.GetString(3);
                                        niñoInfo.CiudadNacimiento = reader.GetString(4);
                                        niñoInfo.Telefono = reader.GetString(5);
                                        niñoInfo.Direccion = reader.GetString(6);
                                        niñoInfo.EPS = reader.GetString(7);
                                        niñoInfo.FK_Acudiente = reader.GetString(8);
                                        niñoInfo.FK_Jardin = reader.GetString(9);
                                        niñoInfo.FK_Madre = reader.GetString(10);
                                            listniño.Add(niñoInfo);
                                    };

                                    
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

        public class NiñoInfo
        {
            public string Id_niño { get; set; }
            public string nom_niño { get; set; }
       
            public string RH { get; set; }
            public string CiudadNacimiento { get; set; }
            public string Telefono { get; set; }
            public string Direccion { get; set; }
            public string EPS { get; set; }
            public string FK_Acudiente { get; set; }
            public string FK_Jardin { get; set; }
            public string FK_Madre { get; set; }
        }
    }
}
