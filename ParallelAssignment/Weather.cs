namespace ParallelAssignment
{
    public class Weather
    {
        public int Id { get; set; }
        public CurrentWeather Current_Weather { get; set; }
    }

    public class CurrentWeather
    {
        public float Temperature { get; set; }
        public float Windspeed { get; set; }
        public float Winddirection { get; set; }
        public DateTime Time { get; set; }
    }
}
