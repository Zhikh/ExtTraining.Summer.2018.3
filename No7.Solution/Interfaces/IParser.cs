namespace No7.Solution.Interface
{
    public interface IParser<out TResult>
    {
        /// <summary>
        /// Parses string value
        /// </summary>
        /// <param name="value"> String to parse </param>
        /// <returns> Result of parsing </returns>
        TResult Parse(string value);
    }
}
