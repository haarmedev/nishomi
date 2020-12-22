using Analystor.Nishomi.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Analystor.Nishomi.Admin
{
    public class DataTableModelBinder : IModelBinder
    {
        /// <summary>
        /// Attempts to bind a model.
        /// </summary>
        /// <param name="bindingContext">The <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext" />.</param>
        /// <returns>
        /// <para>
        /// A <see cref="T:System.Threading.Tasks.Task" /> which will complete when the model binding process completes.
        /// </para>
        /// <para>
        /// If model binding was successful, the <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext.Result" /> should have
        /// <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingResult.IsModelSet" /> set to <c>true</c>.
        /// </para>
        /// <para>
        /// A model binder that completes successfully should set <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext.Result" /> to
        /// a value returned from <see cref="M:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingResult.Success(System.Object)" />.
        /// </para>
        /// </returns>
        /// <exception cref="ArgumentNullException">bindingContext</exception>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var draw = bindingContext.HttpContext.Request.Form["draw"].FirstOrDefault();
            var start = bindingContext.HttpContext.Request.Form["start"].FirstOrDefault();
            var length = bindingContext.HttpContext.Request.Form["length"].FirstOrDefault();

            int orderColumnIndex = 0;
            string sortColumn = string.Empty;

            var orderColumn = bindingContext.HttpContext.Request.Form["order[0][column]"].FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(orderColumn))
            {
                orderColumnIndex = Convert.ToInt32(orderColumn);
            }

            if (orderColumnIndex != 0)
            {
                sortColumn = bindingContext.HttpContext.Request.Form["columns[" + orderColumnIndex + "][name]"].FirstOrDefault();
            }

            var sortColumnDirection = bindingContext.HttpContext.Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = bindingContext.HttpContext.Request.Form["searchText"].FirstOrDefault();
            var additionalDataJson = bindingContext.HttpContext.Request.Form["additionalData"].FirstOrDefault();

            dynamic additionalData = null;

            if (!string.IsNullOrWhiteSpace(additionalDataJson))
            {
                additionalData = JsonConvert.DeserializeObject(additionalDataJson);
            }

            if (string.IsNullOrEmpty(draw))
            {
                return Task.CompletedTask;
            }

            var result = new DataTableRequest
            {
                Draw = string.IsNullOrWhiteSpace(draw) ? 0 : Convert.ToInt32(draw),
                PageSize = length != null ? Convert.ToInt32(length) : 0,
                Skip = start != null ? Convert.ToInt32(start) : 0,
                SearchValue = searchValue,
                SortColumn = sortColumn,
                SortColumnDirection = sortColumnDirection,
                AdditionalData = additionalData
            };

            bindingContext.Result = ModelBindingResult.Success(result);

            return Task.CompletedTask;
        }
    }
}
