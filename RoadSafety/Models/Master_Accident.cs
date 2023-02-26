using System.ComponentModel.DataAnnotations;

namespace RoadSafety.Models
{
    public class Master_Accident
    {
        public int?     AccidentID      { get; set; }

        
        public int      Vehicle1ID      { get; set; }

      
        public int      Vehicle2ID      { get; set; }


        public int      CountryID      { get; set; }
        public int StateID { get; set; }

        public int CityID { get; set; }

        public DateTime Date            { get; set; }
        public int      Casuality       { get; set; }
        public int      Death       { get; set; }
    }
}
