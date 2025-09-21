using System.ComponentModel.DataAnnotations;

namespace AdvFullstack_Labb2.Helpers
{
    public class OptionalMinLengthAttribute : ValidationAttribute
    {
        private readonly int _minLength;

        public OptionalMinLengthAttribute(int minLength)
        {
            _minLength = minLength;
        }

        public override bool IsValid(object? value)
        {
            var stringValue = value as string;

            if (string.IsNullOrEmpty(stringValue))
            {
                return true;
            }

            return stringValue.Length >= _minLength;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"Om angivet måste {name} vara minst {_minLength} tecken.";
        }
    }
}
