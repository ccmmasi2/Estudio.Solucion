namespace Estudio.Domain
{
    public class FragranceType
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = default!;
        public string? Description { get; private set; } = default!;

        public ICollection<Product> Products { get; private set; } = new List<Product>();

        protected FragranceType() {}

        public FragranceType(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }
    }
}
