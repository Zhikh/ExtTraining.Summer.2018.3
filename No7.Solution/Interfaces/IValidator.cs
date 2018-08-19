namespace No7.Solution.Interface
{
    public interface IValidator<in TInput>
    {
        /// <summary>
        /// Checks value on valid
        /// </summary>
        /// <param name="value"> Value to check </param>
        /// <returns> If value is valid, it's true, else - false </returns>
        bool IsValid(TInput value);
    }
}
