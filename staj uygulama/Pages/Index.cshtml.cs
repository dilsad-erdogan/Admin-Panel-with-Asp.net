using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace staj_uygulama.Pages
{
    public class IndexModel : PageModel
    {
        public List<UrunlerInfo> listUrunler = new List<UrunlerInfo>();

        //UnitList liste = new UnitList();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-FG8SRQ2\\SQLEXPRESS;Initial Catalog=stajVeriTabanı;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    //liste.unitList.Add("0");

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
                                //urunlerInfo.picture = "/Images/default.png";
                                
                                if((reader.GetString(3) != "") && (reader.GetString(3) != "Null"))
                                {
                                    urunlerInfo.picture = "/Images/" + reader.GetString(3);
                                }
                                else
                                {
                                    urunlerInfo.picture = "/Images/default.png"; 
                                }

                                urunlerInfo.amount = "" + reader.GetInt32(4);
                                urunlerInfo.unit = reader.GetString(5);

                                //liste.unitList.Add(urunlerInfo.unit);//unit listeleri

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

    //public class UnitList
    //{
    //    public List<string> unitList = new List<string>();
    //}

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