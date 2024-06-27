using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ICBFApp.Pages.Usuario.IndexModel;
using System.Data.SqlClient;

namespace ICBFApp.Pages.Usuario
{
    public class CrearUsuarioModel : PageModel
    {
        public UsuarioInfo usuarioInfo = new UsuarioInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            usuarioInfo.nom_usuario = Request.Form["nom_usuario"];
            usuarioInfo.apell_usuario = Request.Form["apell_usuario"];
            usuarioInfo.email = Request.Form["email"];
            usuarioInfo.Contrasena = Request.Form["Contrasena"];
            usuarioInfo.Rol = Request.Form["Rol"];

            if (string.IsNullOrEmpty(usuarioInfo.nom_usuario) ||
                string.IsNullOrEmpty(usuarioInfo.apell_usuario) ||
                string.IsNullOrEmpty(usuarioInfo.email) ||
                string.IsNullOrEmpty(usuarioInfo.Contrasena) ||
                string.IsNullOrEmpty(usuarioInfo.Rol))
            {
                errorMessage = "Debe completar todos los campos";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-O20N4T4\\SQLEXPRESS;Initial Catalog=ICBF1;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlInsert = "INSERT INTO Usuario " +
                                       "(nom_usuario, apell_usuario, email, Contrasena, Rol) VALUES " +
                                       "(@nom_usuario, @apell_usuario, @email, @Contrasena, @Rol)";

                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@nom_usuario", usuarioInfo.nom_usuario);
                        command.Parameters.AddWithValue("@apell_usuario", usuarioInfo.apell_usuario);
                        command.Parameters.AddWithValue("@email", usuarioInfo.email);
                        command.Parameters.AddWithValue("@Contrasena", usuarioInfo.Contrasena);
                        command.Parameters.AddWithValue("@Rol", usuarioInfo.Rol);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            usuarioInfo = new UsuarioInfo();
            successMessage = "Usuario agregado con éxito";
            Response.Redirect("/Usuario/Index");
        }

    }
}


