namespace Estudio.Domain
{
    public class Product
    {
        public int Id { get; private set; }

        public int BrandId { get; private set; }
        public Brand brand { get; private set; } = default!;

        public string Name { get; private set; } = default!;
        public string FragranceType { get; private set; } = default!;
        public decimal Price { get; private set; }
        public bool IsOutOfStock { get; private set; } = false;
        public string Gender { get; private set; } = default!;
        public decimal? DiscountPercentage { get; private set; }
        public bool IsNew { get; private set; } = false;
        public string? ImageUrl { get; private set; } 

        protected Product() { }  

        public Product(int brandId, string name, string fragranceType, decimal price,
            bool isOutOfStock, string gender, decimal? discountPercentage, bool isNew, string? imageUrl)
        {
            BrandId = brandId;
            Name = name;
            FragranceType = fragranceType;
            Price = price;
            IsOutOfStock = isOutOfStock;
            Gender = gender;
            DiscountPercentage = discountPercentage;
            IsNew = isNew;
            ImageUrl = imageUrl;
        }

        public decimal GetDiscountedPrice()
        {
            if (DiscountPercentage is null || DiscountPercentage <= 0)
                return Price;

            return Math.Round(Price * (1 - DiscountPercentage.Value / 100), 2);
        }

        public void MarkAsOutOfStock() => IsOutOfStock = true;

        public void UpdatePrice(decimal newPrice) => Price = newPrice;
    }
}
