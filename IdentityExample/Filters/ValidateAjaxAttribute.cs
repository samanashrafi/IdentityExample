using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft;
using Newtonsoft.Json;
using IdentityExample.DomainClasses;
namespace IdentityExample.Filters
{
    public class ValidateAjaxAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
                return;
            //string[,] errorModel = new string[100,100];
            var modelState = filterContext.Controller.ViewData.ModelState;
            if (!modelState.IsValid)
            {

                //var errorModel =
                //        from x in modelState.Keys
                //        where modelState[x].Errors.Count > 0
                //        select new
                //        {
                //            key = x
                //            //errors = modelState[x].Errors
                //            //.Select(y => y.ErrorMessage)
                //            //.ToList()
                //        };
                List<ErrorList> errorModel = new List<ErrorList>();

                foreach (var item in modelState.Keys)
                {
                    var errorList = new ErrorList
                    {
                        Title = item,
                        Error = modelState[item].Errors[0].ErrorMessage
                    };
                    errorModel.Add(errorList);

                }

                string json = JsonConvert.SerializeObject(errorModel);

                filterContext.Result = new JsonResult()
                {
                    Data = errorModel
                };
                filterContext.HttpContext.Response.StatusCode =
                                                      (int)HttpStatusCode.BadRequest;
            }
        }
    }
}