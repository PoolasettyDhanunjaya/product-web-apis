// File: Models/Product.cs

namespace web_apis.Models
{
    /// <summary>
    /// Represents a product document stored in the MongoDB collection.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Unique identifier for the product (stored as string to match MongoDB ObjectId or GUID).
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string productName { get; set; }

        /// <summary>
        /// Price of the product.
        /// </summary>
        public decimal price { get; set; }

        /// <summary>
        /// Description of the product.
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Category to which the product belongs.
        /// </summary>
        public string category { get; set; }
    }

}
