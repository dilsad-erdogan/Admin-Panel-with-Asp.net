using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace staj_uygulama.Pages.Urunler
{
    public class EditModel : PageModel
    {
        public UrunlerInfo urunlerInfoGet = new UrunlerInfo();
        public UrunlerInfo urunlerInfoPost = new UrunlerInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"]; //bu sayfaya index sayfasýndan yolladýðýmýz id deðerini aldýk

            try
            {
                String connectionString = "Data Source=DESKTOP-FG8SRQ2\\SQLEXPRESS;Initial Catalog=stajVeriTabaný;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM TableUrunler WHERE Urun_ID = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                urunlerInfoGet.id = "" + reader.GetInt32(0);
                                urunlerInfoGet.name = reader.GetString(1);
                                urunlerInfoGet.code = reader.GetString(2);
                                //urunlerInfoGet.picture = "/Images/" + reader.GetString(3);
                                if((reader.GetString(3) != "") && (reader.GetString(3) != "Null"))
                                {
                                    urunlerInfoGet.picture = "/Images/" + reader.GetString(3);
                                }
                                else
                                {
                                    urunlerInfoGet.picture = "/Images/default.png"; 
                                }
                                urunlerInfoGet.amount = "" + reader.GetInt32(4);
                                urunlerInfoGet.unit = reader.GetString(5);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost()
        {
            urunlerInfoPost.id = Request.Query["id"];
            urunlerInfoPost.name = Request.Form["name"];
            urunlerInfoPost.code = Request.Form["code"];

            if (Request.Form["picture"] == "")
            {
                urunlerInfoPost.picture = urunlerInfoGet.picture;
            }
            else
            {
                urunlerInfoPost.picture = Request.Form["picture"];
            }

            if (Request.Form["rb1"] == "rb1")
            {
                urunlerInfoPost.amount = urunlerInfoGet.amount + Request.Form["amount"];
            }
            else if (Request.Form["rb2"] == "secili2")
            {
                urunlerInfoPost.amount = Request.Form["amount"];
            }
            else
            {
                errorMessage = "Stok durumunu seçmediniz.";
                return;
            }

            if(Request.Form["unit"] == "0")
            {
                errorMessage = "Birim seçmeyi unutmayýnýz!";
                return;
            }
            urunlerInfoPost.unit = Request.Form["unit"];

            if(urunlerInfoPost.name.Length == 0 || urunlerInfoPost.code.Length == 0 || urunlerInfoPost.amount.Length == 0 || urunlerInfoPost.unit.Length == 0)
            {
                errorMessage = "Girdiðiniz deðerleri kontrol ediniz.";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-FG8SRQ2\\SQLEXPRESS;Initial Catalog=stajVeriTabaný;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE TableUrunler SET Urun_Adi=@name, Urun_Kodu=@code, Urun_Resim=@picture, Urun_Miktarý=@amount, Urun_Birim=@unit WHERE Urun_ID=@id";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", urunlerInfoPost.name);
                        command.Parameters.AddWithValue("@code", urunlerInfoPost.code);
                        command.Parameters.AddWithValue("@picture", urunlerInfoPost.picture);
                        command.Parameters.AddWithValue("@amount", urunlerInfoPost.amount);
                        command.Parameters.AddWithValue("@unit", urunlerInfoPost.unit);
                        command.Parameters.AddWithValue("@id", urunlerInfoPost.id);

                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Urunler/Product");
        }
    }
}
