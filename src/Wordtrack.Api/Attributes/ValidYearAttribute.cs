using System;
using System.ComponentModel.DataAnnotations;

namespace Wordtrack.Api.Attributes
{
    public class ValidYearAttribute : ValidationAttribute
    {
        public override bool IsValid(object year)
        {
            var isValid = int.Parse(year.ToString()) <= DateTime.Now.Year;
            return isValid;
        }
    }
}
