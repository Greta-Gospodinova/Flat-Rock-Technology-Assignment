namespace WebScraper
{
    public class Product
    {
        public Product(string name, string price, string rating)
        {
            this.ProductName = name;
            this.Price = price;
            this.Rating = rating;
        }

        public string ProductName { get; set; }

        public string Price { get; set; }

        public string Rating { get; set; }
    }
}
