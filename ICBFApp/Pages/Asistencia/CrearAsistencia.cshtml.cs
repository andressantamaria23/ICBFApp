using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ICBFApp.Pages.Asistencia.IndexModel;
using System.Data.SqlClient;

namespace ICBFApp.Pages.Asistencia
{
    public class CrearAsistenciaModel : PageModel
    {
        public AsistenciaInfo asistenciaInfo = new AsistenciaInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            asistenciaInfo.FK_niño = Request.Form["FK_niño"];
            if (DateTime.TryParse(Request.Form["Fecha"], out DateTime fecha))
            {
                asistenciaInfo.Fecha = fecha;
            }
            else
            {
                errorMessage = "Formato de fecha no válido";
                return;
            }
            asistenciaInfo.Estado = Request.Form["Estado"];
            

            if (string.IsNullOrEmpty(asistenciaInfo.FK_niño) ||
                string.IsNullOrEmpty(asistenciaInfo.Estado))
                
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
                    String sqlInsert = "INSERT INTO Asistencia " +
                                       "(FK_niño, Fecha, Estado) VALUES " +
                                       "(@FK_niño, @Fecha, @Estado)";

                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@FK_niño", asistenciaInfo.FK_niño);
                        command.Parameters.AddWithValue("@Fecha", asistenciaInfo.Fecha);
                        command.Parameters.AddWithValue("@Estado", asistenciaInfo.Estado);
                       
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            asistenciaInfo = new AsistenciaInfo();
            successMessage = "Asistencia agregado con éxito";
            Response.Redirect("/Asistencia/Index");
        }
    }
}
