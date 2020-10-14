using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Analystor.Nishomi.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Analystor.Nishomi.Admin.Controllers
{
    public class ProductController : Controller
    {
        /// <summary>
        /// The product service
        /// </summary>
        private readonly IProduct _productService;

        /// <summary>
        /// The category service
        /// </summary>
        private readonly ICategory _categoryService;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<ProductController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productService">The product service.</param>
        /// <param name="logger">The logger.</param>
        public ProductController(IProduct productService,ICategory categoryService, ILogger<ProductController> logger)
        {
            this._productService = productService;
            this._categoryService = categoryService;
            this._logger = logger;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View("~/Views/Product/Index.cshtml");
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ProductDTO product = new ProductDTO();
            product.Categories = _categoryService.Categories();
            return View("~/Views/Product/Create.cshtml",product);
        }

        /// <summary>
        /// Creates the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(ProductDTO product)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                try
                {
                    var duplicate = this._productService.IsDuplicate(product.Name,product.ProductCode, Guid.Empty);
                    if (duplicate)
                    {
                        return Json(new { message = CommonConstants.ProductDuplicate, status = false });
                    }
                    else
                    {
                        status = this._productService.Create(product);
                        return Json(new { message = CommonConstants.SuccessfullyCreated, status = true });
                    }
                }
                catch (Exception ex)
                {
                    this._logger.LogError(ex, "Error in creating product");
                }
            }

            return View("~/Views/Product/Index.cshtml");
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IActionResult Edit(Guid id)
        {
            var product = this._productService.ProductDetail(id);
            product.Categories = this._categoryService.Categories();
            return View("~/Views/Product/Edit.cshtml", product);
        }

        [HttpPost]
        public IActionResult Edit(ProductDTO product)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                try
                {
                    var duplicate = this._productService.IsDuplicate(product.Name, product.ProductCode, product.ProductId);
                    if (duplicate)
                    {
                        return Json(new { message = CommonConstants.ProductDuplicate, status = false });
                    }
                    else
                    {
                        status = this._productService.Update(product);
                        return Json(new { message = CommonConstants.SuccessfullyUpdated, status = true });
                    }
                }
                catch (Exception ex)
                {
                    this._logger.LogError(ex, "Error in updating product");
                }
            }

            return View("~/Views/Product/Index.cshtml");
        }

        public bool Delete(Guid id)
        {
            bool status = false;
            try
            {
                status = this._productService.Delete(id);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error in category delete", ex);
            }

            return status;
        }

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        /// <returns></returns>
        public IActionResult GetProducts(DataTableRequest dataTable)
        {
            var products = this._productService.GetProducts(out int recordsTotal, dataTable.SearchValue, dataTable.Skip, dataTable.PageSize, dataTable.SortColumn, dataTable.SortColumnDirection);

            var items = new
            {
                draw = dataTable.Draw,
                recordsTotal,
                recordsFiltered = recordsTotal,
                data = products
            };

            return Json(items);
        }
    }
}