namespace Estudio.Domain
{
    public class Brand
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = default!;
        public string? Description { get; private set; } = default!;

        public ICollection<Product> Products { get; private set; } = new List<Product>();

        protected Brand() { }

        public Brand(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }
    }
}
