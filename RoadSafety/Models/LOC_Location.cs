namespace RoadSafety.Models
{
	public class LOC_Location
	{
		public int? LocationID { get; set; }

		public int? CountryID { get; set; }
		public int? CityID { get; set; }

		public int? StateID { get; set; }

		public string? Address { get; set; }

		public string? Longitude { get; set; }

		public string? Latitude { get; set; }

	}
}
