using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ICBFApp.Pages.Acudiente.IndexModel;
using System.Data.SqlClient;

namespace ICBFApp.Pages.Acudiente
{
    public class CrearAcudienteModel : PageModel
    {
        public AcudienteInfo acudienteInfo = new AcudienteInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            acudienteInfo.Nombre = Request.Form["Nombre"];
            acudienteInfo.Cedula = Request.Form["Cedula"];
            acudienteInfo.Telefono = Request.Form["Telefono"];
            acudienteInfo.Direccion = Request.Form["Direccion"];
           

            if (string.IsNullOrEmpty(acudienteInfo.Nombre) ||
                string.IsNullOrEmpty(acudienteInfo.Cedula) ||
                string.IsNullOrEmpty(acudienteInfo.Telefono) ||
                string.IsNullOrEmpty(acudienteInfo.Direccion)) 
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
                    String sqlInsert = "INSERT INTO Acudiente " +
                                       "(Nombre, Cedula, Telefono, Direccion) VALUES " +
                                       "(@Nombre, @Cedula, @Telefono, @Direccion)";

                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", acudienteInfo.Nombre);
                        command.Parameters.AddWithValue("@Cedula", acudienteInfo.Cedula);
                        command.Parameters.AddWithValue("@Telefono", acudienteInfo.Telefono);
                        command.Parameters.AddWithValue("@Direccion", acudienteInfo.Direccion);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            acudienteInfo = new AcudienteInfo();
            successMessage = "Acudiente agregado con éxito";
            Response.Redirect("/Acudiente/Index");
        }
    }
}
