namespace Analystor.Nishomi.Core
{
    using Analystor.Nishomi.Domain;
    using Analystor.Nishomi.Persistence;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// ProductService
    /// </summary>
    public class ProductService : ServiceBase, IProduct
    {
        private object ProductImagge;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="contextProvider">The context provider.</param>
        /// <param name="logger">The logger.</param>
        public ProductService(NishomiDbContextProvider contextProvider/*, ILogger<ServiceBase> logger*/) : base(contextProvider/*, logger*/)
        {
        }

        /// <summary>
        /// Gets the product details.
        /// </summary>
        /// <param name="ProductId">The product identifier.</param>
        /// <returns></returns>
        public ProductDTO GetProductDetails(Guid ProductId)
        {
            var product = this.CurrentDbContext.Products
                                        .Include(it => it.Category)
                                        .Include(it => it.ProductImages)
                                        .FirstOrDefault(it => it.Id == ProductId);
            var item = new ProductDTO()
            {
                ProductId = product.Id,
                ProductCode=product.ProductCode,
                Name = product.Name,
                CategoryName = product.Category.Name,
                Caption = product.Category.Caption,
                Type = product.Type,
                Color = product.Color,
                Cost = product.Cost,
                Description=product.Description,
                Images = product.ProductImages.Select(it => new ImagesListDTO()
                {
                    ImageUrl = it.Url
                }).ToList()
            };

            return item;
        }

        /// <summary>
        /// Featureds the products.
        /// </summary>
        /// <returns></returns>
        public List<ProductDTO> FeaturedProducts()
        {
            return this.CurrentDbContext.Products
                                        .Include(it => it.Category)
                                        .Include(it => it.ProductImages)
                                        .Where(it => it.IsFeatured)
                                        .Select(it => new ProductDTO()
                                        {
                                            ProductId = it.Id,
                                            ProductCode=it.ProductCode,
                                            Name = it.Name,
                                            CategoryName = it.Category.Name,
                                            Caption = it.Category.Caption,
                                            Type = it.Type,
                                            Color = it.Color,
                                            Cost = it.Cost,
                                            Description=it.Description,
                                            Images = it.ProductImages.Select(pt => new ImagesListDTO()
                                            {
                                                ImageUrl = pt.Url
                                            }).ToList()
                                        }).ToList();
        }

        /// <summary>
        /// Saves the product image.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public bool SaveProductImage(Guid productId, string url)
        {
            ProductImages entry = new ProductImages()
            {
                ProductId = productId,
                Url = url
            };

            this.CurrentDbContext.ProductImages.Add(entry);
            this.CurrentDbContext.SaveChanges();

            return true;
        }

        /// <summary>
        /// Creates the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        public bool Create(ProductDTO product)
        {
            Product entry = new Product()
            {
                CategoryId = product.CategoryId,
                ProductCode=product.ProductCode,
                Name = product.Name,
                Color = product.Color,
                Type = product.Type,
                Cost = product.Cost,
                Description = product.Description
            };

            this.CurrentDbContext.Products.Add(entry);
            this.CurrentDbContext.SaveChanges();

            return true;
        }

        /// <summary>
        /// Updates the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        public bool Update(ProductDTO product)
        {
            bool status = false;
            var item = this.CurrentDbContext.Products.FirstOrDefault(it => it.Id == product.ProductId);

            if (item != null)
            {
                item.CategoryId = product.CategoryId;
                item.ProductCode = product.ProductCode;
                item.Name = product.Name;
                item.Color = product.Color;
                item.Type = product.Type;
                item.Cost = product.Cost;
                item.Description = product.Description;

                this.CurrentDbContext.Products.Update(item);
                this.CurrentDbContext.SaveChanges();

                status = true;
            }

            return status;
        }

        /// <summary>
        /// Deletes the specified product identifier.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        public bool Delete(Guid productId)
        {
            var product = this.CurrentDbContext.Products.FirstOrDefault(it => it.Id == productId);
            if (product != null)
            {
                this.CurrentDbContext.Products.Remove(product);
                this.CurrentDbContext.SaveChanges();
            }

            var productImages = this.CurrentDbContext.ProductImages.Where(it => it.ProductId == productId);
            if (productImages != null)
            {
                this.CurrentDbContext.ProductImages.RemoveRange(productImages);
                this.CurrentDbContext.SaveChanges();
            }

            return true;
        }
    }
}
