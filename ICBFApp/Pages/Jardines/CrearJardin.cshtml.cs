using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;
using System.Data.SqlClient;
using static ICBFApp.Pages.Jardines.IndexModel;

namespace ICBFApp.Pages.Jardines
{
    public class CrearJardinModel : PageModel
    {
        public JardinInfo jardinInfo = new JardinInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            
            string GetFormValue(string key)
            {
                if (Request.Form.TryGetValue(key, out StringValues value))
                {
                    return value.ToString();
                }
                return string.Empty;
            }

           
            jardinInfo.Nombre = GetFormValue("Nombre");
            jardinInfo.Direccion = GetFormValue("direccion");
            jardinInfo.Estado = GetFormValue("estado");
            jardinInfo.Fk_usuario = GetFormValue("Fk_usuario");

          
            if (string.IsNullOrEmpty(jardinInfo.Nombre) || string.IsNullOrEmpty(jardinInfo.Direccion) ||
                string.IsNullOrEmpty(jardinInfo.Estado) || string.IsNullOrEmpty(jardinInfo.Fk_usuario))
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

                    
                    String sqlInsert = "INSERT INTO Jardines " +
                        "(Nombre, Direccion, Estado, Fk_usuario) VALUES" +
                        "(@Nombre, @Direccion, @Estado, @Fk_usuario)";

                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", jardinInfo.Nombre);
                        command.Parameters.AddWithValue("@Direccion", jardinInfo.Direccion);
                        command.Parameters.AddWithValue("@Estado", jardinInfo.Estado);
                        command.Parameters.AddWithValue("@Fk_usuario", jardinInfo.Fk_usuario);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            
            jardinInfo.Nombre = ""; jardinInfo.Direccion = ""; jardinInfo.Estado = "";
            successMessage = "Jardin agregado con Exito";
            Response.Redirect("/Jardines/Index");
        }
    }
}
