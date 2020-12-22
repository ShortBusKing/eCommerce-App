using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Data
{
    public class EcommerceContextSeed
    {
        public static async Task SeedAsync(EcommerceContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                JsonSerializer js = new JsonSerializer();
                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    JsonReader jr = new JsonTextReader(new StringReader(brandsData));
                   List<ProductBrand> brands = js.Deserialize<List<ProductBrand>>(jr);

                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.ProductTypes.Any())
                {
                    var typeData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    JsonReader jr = new JsonTextReader(new StringReader(typeData));
                    var types = js.Deserialize<List<ProductType>>(jr);

                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    JsonReader jr = new JsonTextReader(new StringReader(productsData));
                    var products = js.Deserialize<List<Product>>(jr);

                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.DeliveryMethods.Any())
                {
                    var deliveryData = File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");
                    JsonReader jr = new JsonTextReader(new StringReader(deliveryData));
                    var DeliveryMethods = js.Deserialize<List<DeliveryMethod>>(jr);

                    foreach (var item in DeliveryMethods)
                    {
                        context.DeliveryMethods.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<EcommerceContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}