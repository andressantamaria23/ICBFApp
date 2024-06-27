using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ICBFApp.Pages.Asistencia
{
    public class DeleteAsistenciaModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (Id <= 0)
            {
                return NotFound();
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-O20N4T4\\SQLEXPRESS;Initial Catalog=ICBF1;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlDelete = "DELETE FROM Asistencia WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(sqlDelete, connection))
                    {
                        command.Parameters.AddWithValue("@Id", Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el Asistencia: " + ex.Message);
                return RedirectToPage("/Asistencia/Index");
            }

            return RedirectToPage("/Asistencia/Index");
        }
    }
}

