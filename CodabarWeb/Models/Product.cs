namespace CodabarWeb.Models
{
    public class Product : IIdentifiable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Remained { get; set; }
        public string Code { get; set; }
        public Unit Unit { get; set; }
    }
}
