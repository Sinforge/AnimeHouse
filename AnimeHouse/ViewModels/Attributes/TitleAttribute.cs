using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AnimeHouse.ViewModels.Attributes
{
    public class TitleAttribute: ValidationAttribute
    {
        Lang _lang;
        public TitleAttribute(Lang lang)
        {
            _lang = lang;
        }
        public override bool IsValid(object? value)
        {
            string? Title = value as string;
            if (Regex.IsMatch(Title, "^[А-Яа-я]+$") && _lang == Lang.En ) {
                return false;
            }
            else if (Regex.IsMatch(Title, "^[A-Za-z]+$") && _lang == Lang.Ru)
            {
                return false;
            }
            return true;

        }

    }
    public enum Lang
    {
        Ru,
        En
    }
}
