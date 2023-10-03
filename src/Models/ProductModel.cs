using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using YourMobileGuide.Models;

namespace YourMobileGuide.Models
{
    /// <summary>
    /// Represents a product model.
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the maker of the product.
        /// </summary>
        public string Maker { get; set; }

        [JsonPropertyName("Image")]
        [RegularExpression(@"^(http:\/\/|https:\/\/).*\.(jpg|jpeg|png)$", ErrorMessage = "The Image must be a valid URL ending with .jpg, .jpeg, or .png.")]
        public string Image { get; set; }

        [RegularExpression(@"^(http:\/\/|https:\/\/).*$", ErrorMessage = "The Url must be a valid HTTP or HTTPS URL.")]
        public string Url { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 2, ErrorMessage = "The Title should have a length of more than {2} and less than {1}")]
        public string Title { get; set; }

        [StringLength(maximumLength: 500, MinimumLength = 10, ErrorMessage = "The Description should have a length of more than 10")]
        public string Description { get; set; }

        public int[] Ratings { get; set; }

        public ProductTypeEnum ProductType { get; set; }

        public string Quantity { get; set; }

        [Range(-1, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Price { get; set; }

        // Store the Comments entered by the users on this product
        public List<CommentModel> CommentList { get; set; } = new List<CommentModel>();

        /// <summary>
        /// Converts the product model to a JSON string.
        /// </summary>
        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);

        /// <summary>
        /// Creates a deep copy of the product model.
        /// </summary>
        /// <returns>A deep copy of the product model.</returns>
        public ProductModel DeepCopy()
        {
            // Create a new ProductModel to copy everything to
            ProductModel deepCopyModel = new ProductModel();
            deepCopyModel.Id = this.Id;
            deepCopyModel.Maker = this.Maker;
            deepCopyModel.Image = this.Image;
            deepCopyModel.Url = this.Url;
            deepCopyModel.Title = this.Title;
            deepCopyModel.Description = this.Description;
            deepCopyModel.ProductType = this.ProductType;
            deepCopyModel.Quantity = this.Quantity;
            deepCopyModel.Price = this.Price;
            deepCopyModel.CommentList = this.CommentList;

            if (this.Ratings == null)
            {
                return deepCopyModel;
            }

            deepCopyModel.Ratings = new int[this.Ratings.Length];
            this.Ratings.CopyTo(deepCopyModel.Ratings, 0);
            return deepCopyModel;
        }
    }
}
