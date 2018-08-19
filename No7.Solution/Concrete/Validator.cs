using No7.Solution.Interface;

namespace No7.Solution.Concrete
{
    public sealed class Validator : IValidator<string>
    {
        #region Public API
        /// <summary>
        /// Checks value on valid
        /// </summary>
        /// <param name="value"> Value to check </param>
        /// <returns> If value is valid, it's true, else - false </returns>
        public bool IsValid(string value)
        {
            var fields = value.Split(new char[] { ',' });

            if (fields.Length != 3)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
