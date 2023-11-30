using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace staj_uygulama.Pages.Urunler
{
    public class IndexModel : PageModel
    {
        public List<UrunlerInfo> listUrunler = new List<UrunlerInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-FG8SRQ2\\SQLEXPRESS;Initial Catalog=stajVeriTabaný;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "SELECT * FROM TableUrunler";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UrunlerInfo urunlerInfo = new UrunlerInfo();
                                urunlerInfo.id = "" + reader.GetInt32(0);
                                urunlerInfo.name = reader.GetString(1);
                                urunlerInfo.code = reader.GetString(2);
                                
                                if(reader.GetString(3) == "")
                                {
                                    urunlerInfo.picture = "Null";
                                }
                                else
                                {
                                    urunlerInfo.picture = reader.GetString(3);
                                }

                                urunlerInfo.amount = "" + reader.GetInt32(4);
                                urunlerInfo.unit = reader.GetString(5);

                                listUrunler.Add(urunlerInfo);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }

    public class UrunlerInfo
    {
        public String id;
        public String name;
        public String code;
        public String picture;
        public String amount;
        public String unit;
    }
}
