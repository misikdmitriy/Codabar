namespace CodabarWeb.Models
{
    public class Unit : IIdentifiable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsFloat { get; set; }
    }
}
