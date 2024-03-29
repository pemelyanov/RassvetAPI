﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RassvetAPI.Models.ResponseModels;
using RassvetAPI.Util;
using RassvetAPI.Util.UsefulExtensions;
using System.Linq;

namespace RassvetAPI.Controllers
{
    /// <summary>
    /// В контроллере переопределен метод вывода ошибок валидации.
    /// Все ошибки выводятся в виде коллекции строк.
    /// </summary>
    public abstract class ApiControllerBase : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(a => a.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage);

                context.Result = new OkObjectResult(ResponseBuilder.Create(code: 400, errors: errors.GroupErrors()));
            }
            base.OnActionExecuting(context);
        }
    }
}
