using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ICBFApp.Pages.AvanceAcademico
{
    public class IndexModel : PageModel
    {
        public List<AcademicoInfo> listAvance { get; private set; } = new List<AcademicoInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-O20N4T4\\SQLEXPRESS;Initial Catalog=ICBF1;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlSelect = @"
                        SELECT a.Id_Academico, n.Nom_niño AS Nombre_Niño, a.Nivel, a.Notas, a.Descripcion 
                        FROM AvanceAcademico a 
                        JOIN Niño n ON a.FK_Niño = n.Id_Niño;";

                    using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    AcademicoInfo academicoInfo = new AcademicoInfo
                                    {
                                        Id_Academico = reader.GetInt32(0).ToString(),
                                        FK_Niño = reader.GetString(1),
                                        Nivel = reader.GetString(2),
                                        Notas = reader.GetString(3),
                                        Descripcion = reader.GetString(4)
                                    };

                                    listAvance.Add(academicoInfo);
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

        public class AcademicoInfo
        {
            public string Id_Academico { get; set; }
            public string FK_Niño { get; set; }
            public string Nivel { get; set; }
            public string Notas { get; set; }
            public string Descripcion { get; set; }
        }
    }
}
