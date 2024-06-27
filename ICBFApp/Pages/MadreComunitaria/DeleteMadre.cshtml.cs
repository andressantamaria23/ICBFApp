using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ICBFApp.Pages.MadreComunitaria
{
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id_Madre { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (Id_Madre <= 0)
            {
                return NotFound();
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-O20N4T4\\SQLEXPRESS;Initial Catalog=ICBF1;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlDelete = "DELETE FROM MadreComunitaria WHERE Id_Madre = @Id_Madre";
                    using (SqlCommand command = new SqlCommand(sqlDelete, connection))
                    {
                        command.Parameters.AddWithValue("@Id_Madre", Id_Madre);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el Usuario: " + ex.Message);
                return RedirectToPage("/MadreComunitaria/Index");
            }

            return RedirectToPage("/MadreComunitaria/Index");
        }
    }
}
