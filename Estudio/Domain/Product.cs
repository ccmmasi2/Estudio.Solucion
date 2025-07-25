namespace Estudio.Domain
{
    public class Product
    {
        public int Id { get; private set; }

        public int BrandId { get; private set; }
        public Brand Brand { get; private set; } = default!;

        public int FragranceTypeId { get; private set; }
        public FragranceType FragranceType { get; private set; } = default!;

        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; }
        public bool IsOutOfStock { get; private set; } = false;
        public string Gender { get; private set; } = default!;
        public decimal? DiscountPercentage { get; private set; }
        public bool IsNew { get; private set; } = false;
        public string? ImageUrl { get; private set; }
        public int PresentationMM { get; private set; }

        protected Product() { }  

        public Product(string name, decimal price,
            bool isOutOfStock, string gender, decimal? discountPercentage, bool isNew, 
            string? imageUrl, int presentationMM, int brandId, int fragranceTypeId)
        {
            Name = name;
            Price = price;
            IsOutOfStock = isOutOfStock;
            Gender = gender;
            DiscountPercentage = discountPercentage;
            IsNew = isNew;
            ImageUrl = imageUrl;
            PresentationMM = presentationMM;

            BrandId = brandId;
            FragranceTypeId = fragranceTypeId;
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
