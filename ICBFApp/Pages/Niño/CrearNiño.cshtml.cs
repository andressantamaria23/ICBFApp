using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ICBFApp.Pages.Niño.IndexModel;
using System.Data.SqlClient;

namespace ICBFApp.Pages.Niño
{
    public class CrearNiñoModel : PageModel
    {
        public NiñoInfo niñoInfo = new NiñoInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            niñoInfo.nom_niño = Request.Form["nom_niño"];
            
            niñoInfo.RH = Request.Form["RH"];
            niñoInfo.CiudadNacimiento = Request.Form["CiudadNacimiento"];
            niñoInfo.Telefono = Request.Form["Telefono"];
            niñoInfo.Direccion = Request.Form["Direccion"];
            niñoInfo.EPS = Request.Form["EPS"];
            niñoInfo.FK_Acudiente = Request.Form["FK_Acudiente"];
            niñoInfo.FK_Jardin = Request.Form["FK_Jardin"];
            niñoInfo.FK_Madre = Request.Form["FK_Madre"];

            if (string.IsNullOrEmpty(niñoInfo.nom_niño) || 
                string.IsNullOrEmpty(niñoInfo.RH) || string.IsNullOrEmpty(niñoInfo.CiudadNacimiento) ||
                string.IsNullOrEmpty(niñoInfo.Telefono) || string.IsNullOrEmpty(niñoInfo.Direccion) ||
                string.IsNullOrEmpty(niñoInfo.EPS) || string.IsNullOrEmpty(niñoInfo.FK_Acudiente) ||
                string.IsNullOrEmpty(niñoInfo.FK_Jardin) || string.IsNullOrEmpty(niñoInfo.FK_Madre))
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

                    String sqlInsert = "INSERT INTO Niño " +
                        "(Nom_niño, FechaNacimiento, RH, CiudadNacimiento, Telefono, Direccion, EPS, FK_Acudiente, FK_Jardin, FK_Madre) VALUES" +
                        "(@Nom_niño, @FechaNacimiento, @RH, @CiudadNacimiento, @Telefono, @Direccion, @EPS, @FK_Acudiente, @FK_Jardin, @FK_Madre)";

                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@Nom_niño", niñoInfo.nom_niño);
                        
                        command.Parameters.AddWithValue("@RH", niñoInfo.RH);
                        command.Parameters.AddWithValue("@CiudadNacimiento", niñoInfo.CiudadNacimiento);
                        command.Parameters.AddWithValue("@Telefono", niñoInfo.Telefono);
                        command.Parameters.AddWithValue("@Direccion", niñoInfo.Direccion);
                        command.Parameters.AddWithValue("@EPS", niñoInfo.EPS);
                        command.Parameters.AddWithValue("@FK_Acudiente", niñoInfo.FK_Acudiente);
                        command.Parameters.AddWithValue("@FK_Jardin", niñoInfo.FK_Jardin);
                        command.Parameters.AddWithValue("@FK_Madre", niñoInfo.FK_Madre);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            niñoInfo.nom_niño = "";
            
            niñoInfo.RH = "";
            niñoInfo.CiudadNacimiento = "";
            niñoInfo.Telefono = "";
            niñoInfo.Direccion = "";
            niñoInfo.EPS = "";
            niñoInfo.FK_Acudiente = "";
            niñoInfo.FK_Jardin = "";
            niñoInfo.FK_Madre = "";

            successMessage = "Niño agregado con éxito";
            Response.Redirect("/Niño/Index");
        }
    }
}
