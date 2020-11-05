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
    /// CategoryService
    /// </summary>
    /// <seealso cref="Analystor.Nishomi.Core.ServiceBase" />
    public class CategoryService : ServiceBase, ICategory
    {
        /// <summary>
        /// The file manager
        /// </summary>
        private readonly IFileManager _fileManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService"/> class.
        /// </summary>
        /// <param name="contextProvider">The context provider.</param>
        /// <param name="logger">The logger.</param>
        public CategoryService(NishomiDbContextProvider contextProvider,IFileManager fileManager/*, ILogger<ServiceBase> logger*/) : base(contextProvider/*, logger*/)
        {
            this._fileManager = fileManager;
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        public List<CategoryDTO> GetCategories(out int totalRecords, string keyword, int skip, int pageSize, string sortBy, string sortHow)
        {
            var query = this.CurrentDbContext.Categories.AsQueryable();

            totalRecords = query.Count();

            if (keyword != null)
            {
                query = query.Where(it => it.Name.ToLower().StartsWith(keyword.ToLower()));
                totalRecords = query.Count();
            }

            return query.Skip(skip).Take(pageSize).Select(it => new CategoryDTO()
            {
                CategoryId = it.Id,
                Name = it.Name,
                Caption = it.Caption,
                Description = it.Description,
                Url = it.Url
            }).ToList();
        }

        /// <summary>
        /// Categorieses this instance.
        /// </summary>
        /// <returns></returns>
        public List<CategoryDTO> Categories()
        {
            return this.CurrentDbContext.Categories
                                        .Select(it => new CategoryDTO()
                                        {
                                            CategoryId = it.Id,
                                            Name = it.Name,
                                            Caption = it.Caption,
                                            Description = it.Description,
                                            Url = it.Url
                                        }).ToList();
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        public CategoryDTO GetCategory(Guid categoryId)
        {
            return this.CurrentDbContext.Categories
                                        .Where(it => it.Id == categoryId)
                                        .Select(it => new CategoryDTO()
                                        {
                                            CategoryId = it.Id,
                                            Name = it.Name,
                                            Caption = it.Caption,
                                            Description = it.Description,
                                            Url = it.Url,
                                            Image=it.Image
                                        }).FirstOrDefault();
        }

        /// <summary>
        /// Gets the category products.
        /// </summary>
        /// <returns></returns>
        public List<CategoryDTO> GetCategoryProducts()
        {
            return this.CurrentDbContext.Categories
                                        .Include(it=>it.Products)
                                        .ThenInclude(it=>it.ProductImages)
                                        .AsEnumerable()
                                        .Select(it => new CategoryDTO()
                                        {
                                            CategoryId = it.Id,
                                            Name = it.Name,
                                            Caption = it.Caption,
                                            Description = it.Description,
                                            Url = it.Url,
                                            Image=it.Image,
                                            Products=it.Products.Select(pc=> new ProductAPIDTO()
                                            {
                                                ProductId=pc.Id,
                                                ProductCode=pc.ProductCode,
                                                Name=pc.Name,
                                                Type=pc.Type,
                                                Color=pc.Color,
                                                Cost=pc.Cost,
                                                Description=pc.Description,
                                                CategoryName=it.Name,
                                                Images=pc.ProductImages.Select(im=> new ImagesListDTO()
                                                {
                                                    ImageUrl=im.Url,
                                                    IsMainImage=im.IsMainImage
                                                }).ToList()
                                            }).ToList()
                                        }).ToList();
        }

        /// <summary>
        /// Creates the specified category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public bool Create(CategoryDTO category)
        {
            byte[] fileBytes;
            Category entry = new Category()
            {
                Name = category.Name,
                Caption = category.Caption,
                Description = category.Description,
                Url = category.Url
            };
            using (var ms = new MemoryStream())
            {
                category.File.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            string relativePath = string.Format(CommonConstants.CatogoryImagePath, entry.Id);
            var path = this._fileManager.CreateFileAsync(fileBytes, CommonConstants.ResourcesFolder, relativePath, category.File.FileName);

            using (var ms = new MemoryStream())
            {
                category.ModelImage.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            var modelImagePath = this._fileManager.CreateFileAsync(fileBytes, CommonConstants.ResourcesFolder, relativePath, category.ModelImage.FileName);

            entry.Url = path;
            entry.Image = modelImagePath;

            this.CurrentDbContext.Categories.Add(entry);
            this.CurrentDbContext.SaveChanges();

            return true;
        }

        /// <summary>
        /// Updates the specified category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public bool Update(CategoryDTO category)
        {
            bool status = false;
            byte[] fileBytes;
            var item = this.CurrentDbContext.Categories.FirstOrDefault(it => it.Id == category.CategoryId);

            if (item != null)
            {
                item.Name = category.Name;
                item.Caption = category.Caption;
                item.Description=category.Description;
                //item.Url = category.Url != "" ? category.Url : item.Url;
                if (category.File != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        category.File.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    string relativePath = string.Format(CommonConstants.CatogoryImagePath, item.Id);
                    item.Url = this._fileManager.CreateFileAsync(fileBytes, CommonConstants.ResourcesFolder, relativePath, category.File.FileName);
                }
                if (category.ModelImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        category.ModelImage.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    string relativePath = string.Format(CommonConstants.CatogoryImagePath, item.Id);
                    item.Image = this._fileManager.CreateFileAsync(fileBytes, CommonConstants.ResourcesFolder, relativePath, category.ModelImage.FileName);
                }

                this.CurrentDbContext.Categories.Update(item);
                this.CurrentDbContext.SaveChanges();
                status = true;
            }

            return status;
        }

        /// <summary>
        /// Deletes the specified category identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        public bool Delete(Guid categoryId)
        {
            bool status = false;
            var category = this.CurrentDbContext.Categories.FirstOrDefault(it => it.Id == categoryId);

            if (category != null)
            {
                this.CurrentDbContext.Categories.Remove(category);
                this.CurrentDbContext.SaveChanges();

                status = true;
            }

            return status;
        }

    }
}
