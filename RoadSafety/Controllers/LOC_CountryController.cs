using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace RoadSafety.Controllers
{
    public class LOC_CountryController : Controller
    {
        #region Configuration
        private IConfiguration Configuration;
        public LOC_CountryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion
        public IActionResult Index()
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);

            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_Country_SelectAll";
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);

            return View("LOC_CountryList", dt);

            conn.Close();
        }
    }
}
