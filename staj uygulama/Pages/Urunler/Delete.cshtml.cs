using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace staj_uygulama.Pages.Urunler
{
    public class Index1Model : PageModel
    {
        public String id;
        public void OnGet()
        {
        }

        public void OnPost()
        {
            try{
                id = Request.Query["id"];

                string connectionString = "Data Source=DESKTOP-FG8SRQ2\\SQLEXPRESS;Initial Catalog=stajVeriTabaný;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "DELETE FROM TableUrunler WHERE Urun_ID=@id";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch(Exception ex){}

            Response.Redirect("/Urunler/Product");
        }
    }
}