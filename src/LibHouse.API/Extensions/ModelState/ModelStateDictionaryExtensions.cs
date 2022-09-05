using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LibHouse.API.Extensions.ModelState
{
    internal static class ModelStateDictionaryExtensions
    {
        public static bool NotValid(this ModelStateDictionary modelState)
        {
            return !modelState.IsValid;
        }
    }
}