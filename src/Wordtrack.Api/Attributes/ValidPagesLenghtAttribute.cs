using System.ComponentModel.DataAnnotations;

namespace Wordtrack.Api.Attributes
{
    public class ValidPagesLenghtAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var pages = int.Parse(value.ToString());
            var isValid = pages > 0 && pages <= 7312;
            return isValid;
        }
    }
}
