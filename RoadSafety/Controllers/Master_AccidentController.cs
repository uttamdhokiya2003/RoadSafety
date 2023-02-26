using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using RoadSafety.Models;

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
        #region Delete
        public IActionResult Delete(int AccidentID)
        {
            DataTable dt = new DataTable();
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Master_Accident_DeleteByPK";
            cmd.Parameters.AddWithValue("@AccidentID", AccidentID);
            cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Index");
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
        public IActionResult Save(Master_Accident modelMaster_Accident)
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
                
            }
            #endregion

            #region UpdateByPk
            else
            {
                cmd.CommandText = "PR_Master_Accident_UpdateByPK";
                cmd.Parameters.Add("@AccidentID", SqlDbType.Int).Value = modelMaster_Accident.AccidentID;
            }
            #endregion

            cmd.Parameters.Add("@Vehicle1ID", SqlDbType.Int).Value = modelMaster_Accident.Vehicle1ID;
            cmd.Parameters.Add("@Vehicle2ID", SqlDbType.Int).Value = modelMaster_Accident.Vehicle2ID;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelMaster_Accident.CountryID;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelMaster_Accident.StateID;
            cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelMaster_Accident.CityID;
            cmd.Parameters.Add("@Casuality", SqlDbType.Int).Value = modelMaster_Accident.Casuality;
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
        #region Add
        public IActionResult Add(int? AccidentID)
        {
            #region Select for  Dropdown vehicle1
            string str1 = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn1 = new SqlConnection(str1);
            conn1.Open();
            SqlCommand cmd1 = conn1.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_VEH_Vehicle1_SelectForDropDown";

            DataTable dt1 = new DataTable();
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            dt1.Load(sdr1);

            List<VEH_Vehicle1DropDownModel> List = new List<VEH_Vehicle1DropDownModel>();
            foreach (DataRow dr1 in dt1.Rows)
            {
                VEH_Vehicle1DropDownModel vlst = new VEH_Vehicle1DropDownModel();
                vlst.Vehicle1ID = Convert.ToInt32(dr1["Vehicle1ID"]);
                vlst.Vehicle1Type = dr1["Vehicle1Type"].ToString();
                List.Add(vlst);
            }
            ViewBag.Vehicle1List = List;
            conn1.Close();
            #endregion

            #region Select for  Dropdown vehicle2
            string str5 = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn5 = new SqlConnection(str5);
            conn5.Open();
            SqlCommand cmd5 = conn5.CreateCommand();
            cmd5.CommandType = CommandType.StoredProcedure;
            cmd5.CommandText = "PR_VEH_Vehicle2_SelectForDropDown";

            DataTable dt5 = new DataTable();
            SqlDataReader sdr5 = cmd5.ExecuteReader();
            dt5.Load(sdr5);

            List<VEH_Vehicle2DropDownModel> List5 = new List<VEH_Vehicle2DropDownModel>();
            foreach (DataRow dr5 in dt5.Rows)
            {
                VEH_Vehicle2DropDownModel vlst5 = new VEH_Vehicle2DropDownModel();
                vlst5.Vehicle2ID = Convert.ToInt32(dr5["Vehicle2ID"]);
                vlst5.Vehicle2Type = dr5["Vehicle2Type"].ToString();
                List5.Add(vlst5);
            }
            ViewBag.Vehicle2List = List5;
            #endregion

            #region Country Drop Down

            SqlConnection conn2 = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));

            conn2.Open();

            SqlCommand Cmd1 = conn2.CreateCommand();
            Cmd1.CommandType = CommandType.StoredProcedure;
            Cmd1.CommandText = "PR_LOC_Country_SelectForDropDown";
            SqlDataReader objSDR1 = Cmd1.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Load(objSDR1);

            List<LOC_CountryDropDownModel> list = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr in dt2.Rows)
            {
                LOC_CountryDropDownModel vlst = new LOC_CountryDropDownModel();
                vlst.CountryID = Convert.ToInt32(@dr["CountryID"]);
                vlst.CountryName = dr["CountryName"].ToString();
                list.Add(vlst);
            }
            ViewBag.CountryList = list;
            conn2.Close();
            #endregion

            #region State Drop Down

       
            List<LOC_StateDropDownModel> state_list = new List<LOC_StateDropDownModel>();
          
            ViewBag.StateList = state_list;
            
            #endregion

            #region City Drop Down
            
            List<LOC_CityDropDownModel> list3 = new List<LOC_CityDropDownModel>();
           
            ViewBag.CityList = list3;
           
            #endregion

            

            if (AccidentID != null)
            {

                string str = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_Master_Accident_SelectByPK";
                cmd.Parameters.Add("@AccidentID", SqlDbType.Int).Value = AccidentID;

                DataTable dt = new DataTable();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                Master_Accident modelmaster_Accident = new Master_Accident();

                foreach (DataRow dr in dt.Rows)
                {
                    //DropDownByCountry(Convert.ToInt32(dr["CountryID"]));
                    DropDownByCountry(Convert.ToInt32(dr["CountryID"]));
                    DropDownByState(Convert.ToInt32(dr["StateID"]));
                    modelmaster_Accident.AccidentID = Convert.ToInt32(dr["AccidentID"]);
                    modelmaster_Accident.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelmaster_Accident.StateID = Convert.ToInt32(dr["StateID"]);
                    modelmaster_Accident.CityID = Convert.ToInt32(dr["CityID"]);

                    modelmaster_Accident.Vehicle1ID = Convert.ToInt32(dr["Vehicle1ID"]);
                    modelmaster_Accident.Vehicle2ID = Convert.ToInt32(dr["Vehicle2ID"]);
                    modelmaster_Accident.Casuality = Convert.ToInt32(dr["Casuality"]);
                    modelmaster_Accident.Death = Convert.ToInt32(dr["Death"]);

                    modelmaster_Accident.Date = Convert.ToDateTime(dr["Date"]);
                }

                return View("Master_AccidentAddEdit", modelmaster_Accident);
            }
            return View("Master_AccidentAddEdit");
        }
        #endregion
    }
}

