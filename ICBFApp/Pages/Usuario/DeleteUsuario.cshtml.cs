using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ICBFApp.Pages.Usuario
{
    public class DeleteModel : PageModel
    {
            [BindProperty(SupportsGet = true)]
            public int Id_usuario { get; set; }

            public IActionResult OnGet()
            {
                return Page();
            }

            public IActionResult OnPost()
            {
                if (Id_usuario <= 0)
                {
                    return NotFound();
                }

                try
                {
                    string connectionString = "Data Source=DESKTOP-O20N4T4\\SQLEXPRESS;Initial Catalog=ICBF1;Integrated Security=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sqlDelete = "DELETE FROM Usuario WHERE Id_usuario = @Id_usuario";
                        using (SqlCommand command = new SqlCommand(sqlDelete, connection))
                        {
                            command.Parameters.AddWithValue("@Id_usuario", Id_usuario);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al eliminar el Usuario: " + ex.Message);
                    return RedirectToPage("/Usuario/Index");
                }

                return RedirectToPage("/Usuario/Index");
            }
        }
    }
 
