using Newtonsoft.Json;

namespace ParallelAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
             * This API does not allow you to get a specific city by name or in bulk, you have to give in the 
             * latitude and longitude of a specific city each time you make a request. This is the reason why 
             * I had to manually add the endpoints of the cities to a list first.
            */


            // Make and fill list of city objects 
            var cities = new List<City>();
            cities.Add(new City("Antwerpen","https://api.open-meteo.com/v1/forecast?latitude=51.22&longitude=4.40&hourly=temperature_2m&current_weather=true" ));
            cities.Add(new City("Brasschaat","https://api.open-meteo.com/v1/forecast?latitude=51.33&longitude=4.50&hourly=temperature_2m&current_weather=true"));
            cities.Add(new City("Oostende", "https://api.open-meteo.com/v1/forecast?latitude=51.22&longitude=2.93&hourly=temperature_2m&current_weather=true"));
            cities.Add(new City("Gent", "https://api.open-meteo.com/v1/forecast?latitude=51.05&longitude=3.72&hourly=temperature_2m&current_weather=true"));
            cities.Add(new City("Luik", "https://api.open-meteo.com/v1/forecast?latitude=50.63&longitude=5.57&hourly=temperature_2m&current_weather=true"));
            cities.Add(new City("Brussel", "https://api.open-meteo.com/v1/forecast?latitude=50.85&longitude=4.35&hourly=temperature_2m&current_weather=true"));
            cities.Add(new City("Brugge", "https://api.open-meteo.com/v1/forecast?latitude=51.21&longitude=3.22&hourly=temperature_2m&current_weather=true"));
            cities.Add(new City("London", "https://api.open-meteo.com/v1/forecast?latitude=51.51&longitude=-0.13&hourly=temperature_2m&current_weather=true"));
            cities.Add(new City("Berlin", "https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m&current_weather=true"));
            cities.Add(new City("Amsterdam", "https://api.open-meteo.com/v1/forecast?latitude=52.37&longitude=4.89&hourly=temperature_2m&current_weather=true"));
            cities.Add(new City("Parijs", "https://api.open-meteo.com/v1/forecast?latitude=48.85&longitude=2.35&hourly=temperature_2m&current_weather=true"));
            cities.Add(new City("Rome", "https://api.open-meteo.com/v1/forecast?latitude=41.89&longitude=12.51&hourly=temperature_2m&current_weather=true"));
            cities.Add(new City("Madrid", "https://api.open-meteo.com/v1/forecast?latitude=40.42&longitude=-3.70&hourly=temperature_2m&current_weather=true"));
            cities.Add(new City("Moskou", "https://api.open-meteo.com/v1/forecast?latitude=55.75&longitude=37.62&hourly=temperature_2m&current_weather=true"));
            cities.Add(new City("Tokio", "https://api.open-meteo.com/v1/forecast?latitude=35.69&longitude=139.69&hourly=temperature_2m&current_weather=true"));
            cities.Add(new City("Athene", "https://api.open-meteo.com/v1/forecast?latitude=37.98&longitude=23.73&hourly=temperature_2m&current_weather=true"));
            cities.Add(new City("Dubai", "https://api.open-meteo.com/v1/forecast?latitude=25.08&longitude=55.31&hourly=temperature_2m&current_weather=true"));

            // Get and show data in a parallel manner
            GetWeatherParallel(cities);

            // Get and show data in a sequential manner (made to compare the difference in speed)
            // GetWeatherSequential(cities);

            Console.ReadLine();
        }


        // Gets data in a parallel manner
        public static void GetWeatherParallel(List<City> cities)
        {
            Parallel.ForEach(cities, city =>
            {
                try
                {
                    var client = new HttpClient();
                    var content = client.GetStringAsync(city.Url).GetAwaiter().GetResult();
                    Weather weather = JsonConvert.DeserializeObject<Weather>(content);
                    PrintWeatherInfo(city, weather);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine($"This endpoint ({city.Url}) may not exist anymore");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }

        // Gets data in a sequential manner (made to compare the difference in speed)
        public static void GetWeatherSequential(List<City> cities)
        {
            foreach (var city in cities)
            {
                try
                {
                    var client = new HttpClient();
                    var content = client.GetStringAsync(city.Url).GetAwaiter().GetResult();
                    Weather weather = JsonConvert.DeserializeObject<Weather>(content);
                    PrintWeatherInfo(city, weather);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine($"This endpoint ({city.Url}) may not exist anymore");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        // Print the weather information
        public static void PrintWeatherInfo(City city, Weather weatherInfo)
        {
            Console.WriteLine($"Weer in {city.Name}: \nTemperatuur: {weatherInfo.Current_Weather.Temperature} °C\n" +
                $"Windsnelheid: {weatherInfo.Current_Weather.Windspeed} km/h\nDatum en Tijd: {weatherInfo.Current_Weather.Time}\n------------------");
        }
    }
}
