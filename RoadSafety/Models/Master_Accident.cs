using System.ComponentModel.DataAnnotations;

namespace RoadSafety.Models
{
    public class Master_Accident
    {
        public int? AccidentID { get; set; }

        
        public int Vehicle1ID { get; set; }

      
        public int Vehicle2ID { get; set; }


        public int LocationID { get; set; }
        public DateTime Date { get; set; }
        public int Casuality { get; set; }
        public int Death { get; set; }
    }
}
