using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.IO;

namespace staj_uygulama.Pages.Urunler
{
    public class CreateModel : PageModel
    {
        public UrunlerInfo urunlerInfo = new UrunlerInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public String y�nlendirmeMessage = "";
        public int y�nlendirmeid;

        public List<string> codes = new List<string>();
        public List<string> names = new List<string>();

        public void OnGet()
        {
        }

        public void OnPost()
        {
            urunlerInfo.name = Request.Form["name"];
            urunlerInfo.code = Request.Form["code"];
            urunlerInfo.picture = Request.Form["picture"];
            urunlerInfo.amount = Request.Form["amount"];
            urunlerInfo.unit = Request.Form["unit"];

            if(urunlerInfo.name == "" || urunlerInfo.code == "" || urunlerInfo.amount == "" || urunlerInfo.unit == "")
            {
                errorMessage = "Girdi�iniz de�erleri kontrol ediniz!";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-FG8SRQ2\\SQLEXPRESS;Initial Catalog=stajVeriTaban�;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "SELECT * FROM TableUrunler";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if(urunlerInfo.name == reader.GetString(1))
                                {
                                    y�nlendirmeMessage = "Bu �r�n daha �nce eklenmi�. �r�n� g�ncellemek istiyorsan�z a�a��daki butona t�klay�n�z.";
                                    y�nlendirmeid = reader.GetInt32(0);
                                    return;
                                }  //a�a��daki sorguyu buraya ta��y�p ad� ayn� olan�n id de�erini at edit sayfas�na
                                codes.Add(reader.GetString(2));
                            }
                        }
                    }

                    while (true)
                    {
                        if (codes.Contains(urunlerInfo.code))
                        {
                            int uzunluk = urunlerInfo.code.Length;
                            String code = urunlerInfo.code + urunlerInfo.name.Substring((uzunluk - 2), 2);
                            urunlerInfo.code = code;
                        }
                        else
                        {
                            break;
                        }
                    }

                    sql = "INSERT INTO TableUrunler (Urun_Adi, Urun_Kodu, Urun_Resim, Urun_Miktar�, Urun_Birim) VALUES (@name, @code, @picture, @amount, @unit);";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", urunlerInfo.name);
                        command.Parameters.AddWithValue("@code", urunlerInfo.code);
                        command.Parameters.AddWithValue("@picture", urunlerInfo.picture);
                        command.Parameters.AddWithValue("@amount", urunlerInfo.amount);
                        command.Parameters.AddWithValue("@unit", urunlerInfo.unit);

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

            urunlerInfo.name = ""; urunlerInfo.code = ""; urunlerInfo.picture = ""; urunlerInfo.amount = ""; urunlerInfo.unit = "";
            successMessage = "Yeni �r�n eklendi.";

            Response.Redirect("/Urunler/Product");
        }
    }
}
