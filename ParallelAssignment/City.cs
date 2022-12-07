namespace ParallelAssignment
{
    public class City
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public City(string name, string url)
        {
            this.Name = name;
            this.Url = url;
        }
    }
}
