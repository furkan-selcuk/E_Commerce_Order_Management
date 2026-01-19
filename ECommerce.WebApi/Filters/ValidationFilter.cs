using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.WebApi.Filters
{
    public class ValidationFilter : IActionFilter
    {
        // action çalışmadan önce kontrol edilir
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //sonradan büyütülmek istenirse diye yazdım:)
        }
    }
}
