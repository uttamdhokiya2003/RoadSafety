using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace RoadSafety.Controllers
{
    public class Master_AccidentController : Controller
    {
        #region Configuration
        private IConfiguration Configuration;
        public Master_AccidentController(IConfiguration _configuration)
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
            cmd.CommandText = "PR_Master_Accident_SelectAll";
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);

            return View("Master_AccidentList", dt);

            conn.Close();
        }


        #region DropDownByCountry
        public IActionResult DropDownByCountry(int CountryID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn1 = new SqlConnection(str);
            DataTable dt1 = new DataTable();
            conn1.Open();
            SqlCommand cmd1 = conn1.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_LOC_State_SelectDropDownByCountryID";
            cmd1.Parameters.AddWithValue("@CountryID", CountryID);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            dt1.Load(sdr1);


            List<LOC_StateDropDownModel> state_list = new List<LOC_StateDropDownModel>();
            foreach (DataRow dr1 in dt1.Rows)
            {
                LOC_StateDropDownModel vlst1 = new LOC_StateDropDownModel();
                vlst1.StateID = Convert.ToInt32(dr1["StateID"]);
                vlst1.StateName = dr1["StateName"].ToString();
                state_list.Add(vlst1);
            }
            ViewBag.StateList = state_list;
            var vModel = state_list;
            return Json(vModel);
            conn1.Close();
        }
        #endregion

        #region DropDownByState
        public IActionResult DropDownByState(int StateID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn2 = new SqlConnection(str);
            DataTable dt2 = new DataTable();
            conn2.Open();
            SqlCommand cmd1 = conn2.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_LOC_City_SelectDropDownByStateID";
            cmd1.Parameters.AddWithValue("@StateID", StateID);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            dt2.Load(sdr1);


            List<LOC_CityDropDownModel> list3 = new List<LOC_CityDropDownModel>();
            foreach (DataRow dr2 in dt2.Rows)
            {
                LOC_CityDropDownModel vlst1 = new LOC_CityDropDownModel();
                vlst1.CityID = Convert.ToInt32(dr2["CityID"]);
                vlst1.CityName = dr2["CityName"].ToString();
                list3.Add(vlst1);
            }
            ViewBag.CityList = list3;
            var vModel = list3;
            conn2.Close();
            return Json(vModel);
            #endregion
        }
        #region Save
        [HttpPost]
        public IActionResult Save(Master_AccidentModel modelMaster_Accident)
        {
           

            //if (ModelState.IsValid)
            //{
            #region Insert

            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            if (modelMaster_Accident.AccidentID == null)
            {
                cmd.CommandText = "PR_Master_Accident_Insert";
                cmd.Parameters.Add("@Date", SqlDbType.Date).Value = modelMaster_Accident.Date;
            }
            #endregion

            #region UpdateByPk
            else
            {
                cmd.CommandText = "PR_Master_Accident_UpdateByPK";
                cmd.Parameters.Add("@AccidentID", SqlDbType.Int).Value = modelMaster_Accident.AccidentID;
            }
            #endregion

            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelMaster_Accident.CountryID;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelMaster_Accident.StateID;
            cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelMaster_Accident.CityID;
            cmd.Parameters.Add("@Vehicle1ID", SqlDbType.Int).Value = modelMaster_Accident.Vehicle1ID;
            cmd.Parameters.Add("@Vehicle2ID", SqlDbType.Int).Value = modelMaster_Accident.Vehicle2ID;
            cmd.Parameters.Add("@Casulity", SqlDbType.Int).Value = modelMaster_Accident.Casulity;
            cmd.Parameters.Add("@Death", SqlDbType.Int).Value = modelMaster_Accident.Death;
            cmd.Parameters.Add("@Date", SqlDbType.Date).Value = modelMaster_Accident.Date;


            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelMaster_Accident.AccidentID == null)
                    TempData["ContactInsertMsg"] = "Record Inserted Sucessfully!!";
                else
                    TempData["ContactInsertMsg"] = "Record Updated Sucessfully!!";
            }
            conn.Close();

            return RedirectToAction("Index");
        }
        #endregion

    }

    internal class LOC_StateDropDownModel
    {
        internal int StateID;

        public string? StateName { get; internal set; }
    }

    internal class LOC_CityDropDownModel
    {
        internal int CityID;
        internal string? CityName;
    }
}
