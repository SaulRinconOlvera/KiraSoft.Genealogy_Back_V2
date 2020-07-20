using AutoMapper.Internal;
using KiraSoft.CrossCutting.Operation.Transaction.Implementation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace KiraSoft.Genealogy.Web.API.Utilities.Filter
{
    public class PersonalizedBadRequest
    {
        public static ErrorAnswer<ModelStateDictionary> ProcessAnswerd(ModelStateDictionary modelState) 
        {
            var result = new ErrorAnswer<ModelStateDictionary>("ModelState Error");

            foreach (var error in modelState) 
            {
                string fieldName = error.Key;
                string message = string.Empty;

                error.Value.Errors.ForAll(e => {
                    message += $"{e.ErrorMessage}{Environment.NewLine}";
                });

                result.ErrorList.Add(new ErrorViewModel() { ErrorType = fieldName, Message = message });

            }
            return result;
        }

    }
}
