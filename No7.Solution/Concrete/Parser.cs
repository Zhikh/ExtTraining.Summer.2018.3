using System;
using No7.Solution.Interface;
using No7.Solution.Mappers;

namespace No7.Solution.Concrete
{
    public sealed class Parser : IParser<Trade>
    {
        private IValidator<string> _validator;

        public Parser()
        {
            _validator = new Validator();
        }

        public Parser(IValidator<string> validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public Trade Parse(string value)
        {
            if (!_validator.IsValid(value))
            {
                throw new ArgumentException($"The {nameof(value)} = {value} is invalid!");
            }

            var fields = value.Split(new char[] { ',' });

            return TradeMapper.ToEntity(fields[0], fields[1], fields[2]);
        }
    }
}
