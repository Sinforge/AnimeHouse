using System.ComponentModel.DataAnnotations;

namespace AnimeHouse.Models.Attributes
{
    public class NicknameAttribute: ValidationAttribute
    {
        string[] _banWords;
        public NicknameAttribute(string[] banWords)
        {
            _banWords = banWords;
        }
        public override bool IsValid(object? value)
        {
            string? newVal = value as string;
            Console.WriteLine(newVal);
            foreach(var word in _banWords)
            {
                if(newVal.Contains(word))
                {
                    return false;
                }
            }
            return true;

        }

    }
}
