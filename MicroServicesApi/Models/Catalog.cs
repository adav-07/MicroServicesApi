namespace CatalogService.Models
{
    public class Catalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Catalog(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
