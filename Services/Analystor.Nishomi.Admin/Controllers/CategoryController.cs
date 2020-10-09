using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Analystor.Nishomi.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Analystor.Nishomi.Admin.Controllers
{
    public class CategoryController : Controller
    {
        /// <summary>
        /// The category service
        /// </summary>
        private readonly ICategory _categoryService;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<CategoryController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        /// <param name="categoryService">The category service.</param>
        public CategoryController(ICategory categoryService, ILogger<CategoryController> logger)
        {
            this._categoryService = categoryService;
            this._logger = logger;
        }


        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View("~/Views/Category/Index.cshtml");
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View("~/Views/Category/Create.cshtml");
        }

        [HttpPost]
        public IActionResult Create(CategoryDTO category)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                try
                {
                    var duplicate = this._categoryService.IsDuplicate(category.Name,Guid.Empty);
                    if (duplicate)
                    {
                        return Json(new { message = CommonConstants.CategoryDuplicate, status = false });
                    }
                    else
                    {
                        status = this._categoryService.Create(category);
                        return Json(new { message = CommonConstants.SuccessfullyCreated, status = true });
                    }
                }
                catch (Exception ex)
                {
                    this._logger.LogError(ex, "Error in creating category");
                }
            }

            return View("~/Views/Category/Index.cshtml");
        }

        /// <summary>
        /// Edits the specified category identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        public IActionResult Edit(Guid id)
        {
            var item = this._categoryService.GetCategory(id);
            return View("~/Views/Category/Edit.cshtml",item);
        }

        [HttpPost]
        public IActionResult Update(CategoryDTO category)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                try
                {
                    var duplicate = this._categoryService.IsDuplicate(category.Name, category.CategoryId);
                    if (duplicate)
                    {
                        return Json(new { message = CommonConstants.CategoryDuplicate, status = false });
                    }
                    else
                    {
                        status = this._categoryService.Update(category);
                        return Json(new { message = CommonConstants.SuccessfullyCreated, status = true });
                    }
                }
                catch (Exception ex)
                {
                    this._logger.LogError(ex, "Error in updating category");
                }
            }

            return View("~/Views/Category/Index.cshtml");
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public bool Delete(Guid id)
        {
            bool status = false;
            try
            {
                status = this._categoryService.Delete(id);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error in category delete", ex);
            }

            return status;
        }

        /// <summary>
        /// Gets the categorys.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        /// <returns></returns>
        public IActionResult GetCategorys(DataTableRequest dataTable)
        {
            var categories = this._categoryService.GetCategories(out int recordsTotal, dataTable.SearchValue, dataTable.Skip, dataTable.PageSize, dataTable.SortColumn, dataTable.SortColumnDirection);

            var items = new
            {
                draw = dataTable.Draw,
                recordsTotal,
                recordsFiltered = recordsTotal,
                data = categories
            };

            return Json(items);
        }
    }
}