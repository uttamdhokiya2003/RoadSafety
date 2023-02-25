using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoadSafety.Models
{
    public class LOC_StateModel
    {
        public int? StateID { get; set; }

        [Required]
        public string StateName { get; set; }

        [Required]
        public string StateCode { get; set; }

        [Required]
        [DisplayName("CountryName")]
        public int CountryID { get; set; }
        
        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public object CountryName { get; internal set; }
       
    }

    
}
