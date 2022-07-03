using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ProEShop.Common.Constants;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;

namespace ProEShop.Common.Attributes;

public class CheckModelStateInRazorPagesAttribute : Attribute, IAsyncPageFilter
{
    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        return Task.CompletedTask;
    }

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        var modelState = context.ModelState;
        if (!modelState.IsValid)
        {
            context.Result = new JsonResult(new JsonResultOperation(false, PublicConstantStrings.ModelStateErrorMessage)
            {
                Data = modelState.GetModelStateErrors()
            });
        }
        else
            await next();
    }
}
