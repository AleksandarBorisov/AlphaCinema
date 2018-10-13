namespace AlphaCinemaData.ViewModels
{
    public class ProjectionDetailsViewModel
    {
        public string CityName { get; set; }
        public string MovieName { get; set; }
        public string Hour { get; set; }

        public override string ToString()
        {
            return $"Name of City: {CityName}, Name of Movie: {MovieName}, Start Hour: {Hour}";
        }
    }
}
