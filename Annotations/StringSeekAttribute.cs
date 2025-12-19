using System.ComponentModel.DataAnnotations;

namespace _18._10_Best_Films_MVC.Annotations
{
    public class StringSeekAttribute : ValidationAttribute
    {
        private static string stringForCheck;

        public StringSeekAttribute(string str)
        {
            stringForCheck = str;
        }

        public override bool IsValid(object? value)
        {
            if (value != null)
            {
                string? val = value.ToString();
                if (val.Contains(stringForCheck)) return true;
            }
            return false;
        }
    }
}
