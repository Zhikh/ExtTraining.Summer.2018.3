using System;
using No7.Solution.Interface;
using No7.Solution.Mappers;

namespace No7.Solution.Concrete
{
    public sealed class Parser : IParser<Trade>
    {
        #region Fields
        private IValidator<string> _validator;
        #endregion

        #region Public API
        /// <summary>
        /// Initializes a new instance of the <see cref="Parser" /> with default validator.
        /// </summary>
        public Parser()
        {
            _validator = new Validator();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Parser" /> with custom validator.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="validator"> is null. </paramref>
        /// </exception>
        public Parser(IValidator<string> validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        /// <summary>
        /// Parses string value
        /// </summary>
        /// <param name="value"> String to parse </param>
        /// <returns> Result of parsing </returns>
        /// <exception cref="ArgumentException"> 
        ///     <paramref name="value"> is invalid. </paramref>
        /// </exception>
        public Trade Parse(string value)
        {
            if (!_validator.IsValid(value))
            {
                throw new ArgumentException($"The {nameof(value)} = {value} is invalid!");
            }

            var fields = value.Split(new char[] { ',' });

            return TradeMapper.ToEntity(fields[0], fields[1], fields[2]);
        }
        #endregion
    }
}
