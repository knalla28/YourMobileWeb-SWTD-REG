using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

using YourMobileGuide.Models;

using Microsoft.AspNetCore.Hosting;

namespace YourMobileGuide.Services
{
    /// <summary>
    /// Manage the JSON serializing and deserializing of data
    /// </summary>
    public class JsonFileProductService
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            // Configuring webHostEnvironment from IWebHostEnvironment into local variable
            WebHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// REST accessor for WebHostEnvironment object
        /// </summary>
        public IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// REST Get Request
        /// Loads the path of the requested website
        /// </summary>
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        /// <summary>
        /// Retrieves name of requested JSON object
        /// Deserializes JSON object
        /// Returns JSON object Deserialized data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductModel> GetAllData()
        {
            // Find file through file reader and open text
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                // Deserialize JSON object and return object as a readable string
                return JsonSerializer.Deserialize<ProductModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        /// <summary>
        /// Add Rating
        /// 
        /// Take in the product ID and the rating
        /// If the rating does not exist, add it
        /// Save the update
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="rating"></param>
        public bool AddRating(string productId, int rating)
        {
            // If the ProductID is invalid, return
            if (string.IsNullOrEmpty(productId))
            {
                return false;
            }

            var products = GetAllData();

            // Look up the product, if it does not exist, return
            var data = products.FirstOrDefault(x => x.Id.Equals(productId));
            if (data == null)
            {
                return false;
            }

            // Check Rating for boundries, do not allow ratings below 0
            if (rating < 0)
            {
                return false;
            }

            // Check Rating for boundries, do not allow ratings above 5
            if (rating > 5)
            {
                return false;
            }

            // Check to see if the rating exist, if there are none, then create the array
            if (data.Ratings == null)
            {
                data.Ratings = new int[] { };
            }

            // Add the Rating to the Array
            var ratings = data.Ratings.ToList();
            ratings.Add(rating);
            data.Ratings = ratings.ToArray();

            // Save the data back to the data store
            SaveData(products);

            return true;
        }

        /// <summary>
        /// Find the data record
        /// Update the fields
        /// Save to the data store
        /// </summary>
        /// <param name="data"></param>
        public ProductModel UpdateData(ProductModel data)
        {
            // retrieve data list
            var products = GetAllData();

            //null if data isn't present
            var productData = products.FirstOrDefault(x => x.Id.Equals(data.Id));
            if (productData == null)
            {
                return null;
            }

            productData = data.DeepCopy();

            // Create a new product list without the changed object
            var newProductList = products.Where(x => x.Id != productData.Id);
            newProductList = newProductList.Append(productData);

            //sava added values within new data object
            SaveData(newProductList);

            //return new object
            return productData;
        }

        /// <summary>
        /// Save All products data to storage as JSON entry
        /// </summary>
        private void SaveData(IEnumerable<ProductModel> products)
        {

            //Create JSON entry as a JSON object
            using (var outputStream = File.Create(JsonFileName))
            {
                //Serialize new data of ProductModel as a JSON enumerable object
                JsonSerializer.Serialize<IEnumerable<ProductModel>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    products
                );
            }
        }

        /// <summary>
        /// Create a new product using default values
        /// After create the user can update to set values
        /// </summary>
        /// <returns></returns>
        public ProductModel CreateData(ProductModel data)
        {
            // retrieve data list
            var productList = GetAllData();

            //append new data record to the original dataset
            productList = productList.Append(data);

            //sava the added data
            SaveData(productList);

            return data;
        }

        /// <summary>
        /// Remove the item from the system
        /// </summary>
        /// <returns></returns>
        public ProductModel DeleteData(string id)
        {
            // Get the current set, and append the new record to it
            var dataSet = GetAllData();
            var data = dataSet.FirstOrDefault(m => m.Id.Equals(id));

            //retrieve data where ID value doesn't match the deleted ID value
            var newDataSet = GetAllData().Where(m => m.Id.Equals(id) == false);

            //save the new dataset
            SaveData(newDataSet);

            //return new dataset without deleted ID
            return data;
        }

    }
}
