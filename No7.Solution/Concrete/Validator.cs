using No7.Solution.Interface;

namespace No7.Solution.Concrete
{
    public sealed class Validator : IValidator<string>
    {
        public bool IsValid(string value)
        {
            var fields = value.Split(new char[] { ',' });

            if (fields.Length != 3)
            {
                return false;
            }

            return true;
        }
    }
}
