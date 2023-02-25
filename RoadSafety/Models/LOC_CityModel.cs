using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoadSafety.Models
{
    public class LOC_CityModel
    {
        public int? CityID { get; set; }

        [Required]
        public string? CityName { get; set; }

        [Required]
        public string CityCode { get; set; }

        [Required]
        [DisplayName("Country Name")]
        public int CountryID { get; set; }

        [Required]
        [DisplayName("State Name")]
        public int StateID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public object CountryName { get;  set; }
        public object StateName { get; set; }
    }
    public class LOC_CityDropDownModel
    {
        public int? CityID { get; set; }
        public string? CityName { get; set; }
    }
}
