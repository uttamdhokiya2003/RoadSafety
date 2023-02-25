namespace RoadSafety.Models
{
    public class LOC_Weather
    {
        public int? WeatherID { get; set; }


        public int CityID { get; set; }


        public float Temperature { get; set; }


        public string WindSpeed { get; set; }
        public string Humidity { get; set; }
        public DateTime Time { get; set; }

    }
}
