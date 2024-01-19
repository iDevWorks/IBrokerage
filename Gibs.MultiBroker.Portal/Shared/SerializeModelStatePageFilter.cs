// inspired by https://andrewlock.net/post-redirect-get-using-tempdata-in-asp-net-core/
// usage: in Startup.ConfigureServices() add `services.AddRazorPages().AddMvcOptions(options => options.Filters.Add<SerializeModelStatePageFilter>());`

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;

namespace Microsoft.AspNetCore.Infrastructure
{
    public class SerializeModelStatePageFilter : IPageFilter
    {
        public class ModelStateTransferValue
        {
            public string Key { get; set; } = string.Empty;
            public string AttemptedValue { get; set; } = string.Empty;
            public object RawValue { get; set; } = string.Empty;
            public ICollection<string> ErrorMessages { get; set; } = new List<string>();
        }


        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            if (context.HandlerInstance is not PageModel page)
                return;

            var serializedModelState = page.TempData[nameof(SerializeModelStatePageFilter)] as string;
            if (string.IsNullOrEmpty(serializedModelState))
                return;

            var modelState = DeserializeModelState(serializedModelState);
            page.ModelState.Merge(modelState);
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context) { }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            if (context.HandlerInstance is not PageModel page)
                return;

            if (page.ModelState.IsValid)
                return;

            if (context.Result is IKeepTempDataResult)
            {
                var modelState = SerializeModelState(page.ModelState);
                page.TempData[nameof(SerializeModelStatePageFilter)] = modelState;
            }
        }


        private static string SerializeModelState(ModelStateDictionary modelState)
        {
            var errorList = modelState
                .Select(kvp => new ModelStateTransferValue
                {
                    Key = kvp.Key,
                    AttemptedValue = kvp.Value!.AttemptedValue!,
                    RawValue = kvp.Value.RawValue!,
                    ErrorMessages = kvp.Value.Errors.Select(err => err.ErrorMessage).ToList(),
                });

            return JsonSerializer.Serialize(errorList);
        }

        private static ModelStateDictionary DeserializeModelState(string serialisedErrorList)
        {
            var errorList = JsonSerializer.Deserialize<List<ModelStateTransferValue>>(serialisedErrorList);
            var modelState = new ModelStateDictionary();

            if (errorList == null)
                return modelState;

            foreach (var item in errorList)
            {
                modelState.SetModelValue(item.Key, item.RawValue, item.AttemptedValue);
                foreach (var error in item.ErrorMessages)
                    modelState.AddModelError(item.Key, error);
            }
            return modelState;
        }
    }
}