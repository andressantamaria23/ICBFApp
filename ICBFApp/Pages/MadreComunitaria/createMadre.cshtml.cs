using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ICBFApp.Pages.MadreComunitaria.IndexModel;
using System.Data.SqlClient;


namespace ICBFApp.Pages.MadreComunitaria
{
    public class createMadreModel : PageModel
    {
        public MadreComunitariaInfo madreComunitariaInfo = new MadreComunitariaInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            madreComunitariaInfo.Nombres = Request.Form["Nombres"];
            madreComunitariaInfo.Telefono = Request.Form["Telefono"];
            madreComunitariaInfo.Direccion = Request.Form["Direccion"];
			if (DateTime.TryParse(Request.Form["FechaNacimiento"], out DateTime FechaNacimiento))
			{
				madreComunitariaInfo.FechaNacimiento = FechaNacimiento;
			}
			else
			{
				errorMessage = "Formato de fecha no válido";
				return;
			}


			if (string.IsNullOrEmpty(madreComunitariaInfo.Nombres) ||
                string.IsNullOrEmpty(madreComunitariaInfo.Telefono) ||
                string.IsNullOrEmpty(madreComunitariaInfo.Direccion))
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
                    String sqlInsert = "INSERT INTO MadreComunitaria " +
                                       "(Nombres, Telefono, Direccion,FechaNacimiento) VALUES " +
                                       "(@Nombres,@Telefono, @Direccion, @FechaNacimiento)";

                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@Nombres", madreComunitariaInfo.Nombres);
                        command.Parameters.AddWithValue("@Telefono", madreComunitariaInfo.Telefono);
                        command.Parameters.AddWithValue("@Direccion", madreComunitariaInfo.Direccion);
                        command.Parameters.AddWithValue("@FechaNacimiento", madreComunitariaInfo.Direccion);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            madreComunitariaInfo = new MadreComunitariaInfo();
            successMessage = "Acudiente agregado con éxito";
            Response.Redirect("/Acudiente/Index");
        }
    }
}
