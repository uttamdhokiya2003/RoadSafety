using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoadSafety.Models
{
    public class LOC_CountryModel
    {
        public int? CountryID { get; set; }

        [Required]
        public string? CountryName { get; set; }     
        [Required]
        public string CountryCode { get; set; }
        public DateTime Creation
            Date { get; set; }
        public DateTime ModificationDate { get; set; }

        public IFormFile File { get; set; }
        public string PhotoPath { get; set; }

    }

     public class LOC_CountryDropDownModel
     {
        public int? CountryID { get; set; }
        public string? CountryName { get; set; }
     }
}
