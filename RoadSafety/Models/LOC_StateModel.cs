﻿using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace RoadSafety.Models
{
    public class LOC_StateModel
    {
        public int? StateID { get; set; }

        [Required]
        [DisplayName("State Name")]
        public string StateName { get; set; }

        [Required]
        [DisplayName("State Code")]
        public string StateCode { get; set; }

        [Required]
        [DisplayName("Country Name")]
        public int CountryID { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
        public object CountryName { get; internal set; }

    }
    public class LOC_StateDropDownModel
    {
        public int? StateID { get; set; }
        public string? StateName { get; set; }
    }
}
