using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace ICBFApp.Pages.Jardines
{
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id_jardin { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (Id_jardin <= 0)
            {
                return NotFound();
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-O20N4T4\\SQLEXPRESS;Initial Catalog=ICBF1;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlDelete = "DELETE FROM Jardines WHERE Id_jardin = @Id_jardin";
                    using (SqlCommand command = new SqlCommand(sqlDelete, connection))
                    {
                        command.Parameters.AddWithValue("@Id_jardin", Id_jardin);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el jardín: " + ex.Message);
                return RedirectToPage("/Jardines/Index");
            }

            return RedirectToPage("/Jardines/Index");
        }
    }
}
