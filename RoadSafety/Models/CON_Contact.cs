using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RoadSafety.Models
{
    public class CON_Contact
    {
        public int? ContactID { get; set; }

        [Required]
        public string PoliceNumber { get; set; }

        [Required]
        public string AmbulanceNumber { get; set; }

        [Required]
        
        public string FireNumber { get; set; }
        public int? CityID { get; set; }

        
    }
}
