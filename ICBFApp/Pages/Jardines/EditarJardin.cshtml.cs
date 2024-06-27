using Microsoft.AspNetCore.Mvc.RazorPages;
using static ICBFApp.Pages.Jardines.IndexModel;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace ICBFApp.Pages.Jardines
{
    public class EditModel : PageModel
    {
        private readonly string _connectionString;

        public EditModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ICBF1"); // Ajuste para usar el nombre de cadena de conexión correcto
        }

        public JardinInfo jardinInfo = new JardinInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            string id = Request.Query["Id_jardin"];

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Jardines WHERE Id_Jardin = @Id_jardin";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id_jardin", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                jardinInfo.Id_jardin = reader.GetInt32(0).ToString();
                                jardinInfo.Nombre = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                                jardinInfo.Direccion = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                                jardinInfo.Estado = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                                jardinInfo.Fk_usuario = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                            }
                            else
                            {
                                errorMessage = "No se encontró el jardín con el ID especificado.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }

        public IActionResult OnPost()
        {
            jardinInfo.Id_jardin = Request.Form["id"];
            jardinInfo.Nombre = Request.Form["Nombre"];
            jardinInfo.Direccion = Request.Form["direccionJardin"];
            jardinInfo.Estado = Request.Form["estado"];

            if (string.IsNullOrWhiteSpace(jardinInfo.Id_jardin) ||
                string.IsNullOrWhiteSpace(jardinInfo.Nombre) ||
                string.IsNullOrWhiteSpace(jardinInfo.Direccion) ||
                string.IsNullOrWhiteSpace(jardinInfo.Estado))
            {
                errorMessage = "Debe completar todos los campos";
                OnGet();
                return Page();
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlUpdate = "UPDATE Jardines SET Nombre = @Nombre, Direccion = @Direccion, Estado = @Estado WHERE Id_jardin = @Id_jardin";
                    using (SqlCommand command = new SqlCommand(sqlUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@Id_jardin", jardinInfo.Id_jardin);
                        command.Parameters.AddWithValue("@Nombre", jardinInfo.Nombre);
                        command.Parameters.AddWithValue("@Direccion", jardinInfo.Direccion);
                        command.Parameters.AddWithValue("@Estado", jardinInfo.Estado);

                        command.ExecuteNonQuery();
                    }
                }
                TempData["SuccessMessage"] = "Jardin editado exitosamente";
                return RedirectToPage("/Jardin/Index");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return Page();
            }
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
