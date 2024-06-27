using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ICBFApp.Pages.AvanceAcademico.IndexModel;
using System.Data.SqlClient;

namespace ICBFApp.Pages.AvanceAcademico
{
    public class CrearAvanceModel : PageModel
    {
        public AcademicoInfo academicoInfo = new AcademicoInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            academicoInfo.Nivel = Request.Form["Nivel"];
            academicoInfo.Notas = Request.Form["Notas"];
            academicoInfo.Descripcion = Request.Form["Descripicion"];
            academicoInfo.FK_Niño = Request.Form["FK_Niño"];


            if (string.IsNullOrEmpty(academicoInfo.Nivel) ||
                string.IsNullOrEmpty(academicoInfo.Notas) ||
                string.IsNullOrEmpty(academicoInfo.Descripcion) ||
                string.IsNullOrEmpty(academicoInfo.FK_Niño))
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
                    String sqlInsert = "INSERT INTO AvanceAcademico " +
                                       "(Nivel, Notas, Descripcion, FK_Niño) VALUES " +
                                       "(@Nivel, @Notas, @Descripcion, @FK_Niño)";

                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@Nivel", academicoInfo.Nivel);
                        command.Parameters.AddWithValue("@Notas", academicoInfo.Notas);
                        command.Parameters.AddWithValue("@Descripcion", academicoInfo.Descripcion);
                        command.Parameters.AddWithValue("@FK_Niño", academicoInfo.FK_Niño);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            academicoInfo = new AcademicoInfo();
            successMessage = "Usuario agregado con éxito";
            Response.Redirect("/Usuario/Index");
        }
    }
}
