using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ICBFApp.Pages.Acudiente
{
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id_acudiente { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (Id_acudiente <= 0)
            {
                return NotFound();
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-O20N4T4\\SQLEXPRESS;Initial Catalog=ICBF1;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlDelete = "DELETE FROM Acudiente WHERE Id_acudiente = @Id_acudiente";
                    using (SqlCommand command = new SqlCommand(sqlDelete, connection))
                    {
                        command.Parameters.AddWithValue("@Id_acudiente", Id_acudiente);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el acudiente: " + ex.Message);
                return RedirectToPage("/Acudiente/Index");
            }

            return RedirectToPage("/Acudiente/Index");
        }
    }
}
