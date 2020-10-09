namespace Analystor.Nishomi.Core
{
    using Analystor.Nishomi.Domain;
    using Analystor.Nishomi.Persistence;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// ProductService
    /// </summary>
    public class ProductService : ServiceBase, IProduct
    {
        /// <summary>
        /// The file manager
        /// </summary>
        private readonly IFileManager _fileManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="contextProvider">The context provider.</param>
        /// <param name="logger">The logger.</param>
        public ProductService(NishomiDbContextProvider contextProvider, IFileManager fileManager/*, ILogger<ServiceBase> logger*/) : base(contextProvider/*, logger*/)
        {
            this._fileManager = fileManager;
        }

        /// <summary>
        /// Gets the product details.
        /// </summary>
        /// <param name="ProductId">The product identifier.</param>
        /// <returns></returns>
        public ProductAPIDTO GetProductDetails(Guid ProductId)
        {
            var product = this.CurrentDbContext.Products
                                        .Include(it => it.Category)
                                        .Include(it => it.ProductImages)
                                        .AsEnumerable()
                                        .FirstOrDefault(it => it.Id == ProductId);
            var item = new ProductAPIDTO()
            {
                ProductId = product.Id,
                ProductCode = product.ProductCode,
                Name = product.Name,
                CategoryName = product.Category.Name,
                Caption = product.Caption,
                Type = product.Type,
                Color = product.Color,
                Cost = product.Cost,
                Description = product.Description,
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
        public List<ProductAPIDTO> FeaturedProducts()
        {
            return this.CurrentDbContext.Products
                                        .Include(it => it.Category)
                                        .Include(it => it.ProductImages)
                                        .Where(it => it.IsFeatured)
                                        .Select(it => new ProductAPIDTO()
                                        {
                                            ProductId = it.Id,
                                            ProductCode = it.ProductCode,
                                            Name = it.Name,
                                            CategoryName = it.Category.Name,
                                            Caption = it.Caption,
                                            Type = it.Type,
                                            Color = it.Color,
                                            Cost = it.Cost,
                                            Description = it.Description,
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
            //this.CurrentDbContext.SaveChanges();

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
                ProductCode = product.ProductCode,
                Name = product.Name,
                Color = product.Color,
                Type = product.Type,
                Caption=product.Caption,
                Cost = product.Cost,
                Description = product.Description,
                IsFeatured=product.IsFeatured
            };
            if (product.Files.Count > 0)
            {
                foreach (var file in product.Files)
                {
                    byte[] fileBytes;
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    string relativePath = string.Format(CommonConstants.ProductImagePath, entry.Id);
                    var path = this._fileManager.CreateFileAsync(fileBytes, CommonConstants.ResourcesFolder, relativePath, file.FileName);
                    this.SaveProductImage(entry.Id, path);
                }
            }

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
                item.Caption = product.Caption;
                item.Cost = product.Cost;
                item.Description = product.Description;
                item.IsFeatured = product.IsFeatured;

                this.CurrentDbContext.Products.Update(item);
                this.CurrentDbContext.SaveChanges();

                status = true;
            }
            if (product.Files.Count > 0)
            {
                foreach (var file in product.Files)
                {
                    byte[] fileBytes;
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    string relativePath = string.Format(CommonConstants.ProductImagePath, item.Id);
                    var path = this._fileManager.CreateFileAsync(fileBytes, CommonConstants.ResourcesFolder, relativePath, file.FileName);
                    this.SaveProductImage(item.Id, path);
                }
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

        public List<ProductDetailsDTO> GetProducts(out int totalRecords, string keyword, int skip, int pageSize, string sortBy, string sortHow)
        {
            var query = this.CurrentDbContext.Products.AsQueryable();

            totalRecords = query.Count();

            if (keyword != null)
            {
                query = query.Where(it => it.Name.ToLower().StartsWith(keyword.ToLower()));
                totalRecords = query.Count();
            }

            return query.Skip(skip).Take(pageSize).Select(it => new ProductDetailsDTO()
            {
                ProductId = it.Id,
                //CategoryId = it.CategoryId,
                ProductCode = it.ProductCode,
                ProductName = it.Name,
                Caption = it.Caption,
                Description = it.Description,
                Type = it.Type,
                Color = it.Color,
                Cost = it.Cost
            }).ToList();
        }

        /// <summary>
        /// Determines whether the specified name is duplicate.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified name is duplicate; otherwise, <c>false</c>.
        /// </returns>
        public bool IsDuplicate(string name, string code, Guid id)
        {
            if (id != Guid.Empty)
            {
                return this.CurrentDbContext.Products.Where(it => it.Id != id && it.Name == name && it.ProductCode == code).Count() > 0;
            }
            else
            {
                return this.CurrentDbContext.Products.Where(it => it.Name == name && it.ProductCode == code).Count() > 0;
            }
        }
    }
}
