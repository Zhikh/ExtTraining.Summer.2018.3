using System;

namespace No7.Solution
{
    public class Trade
    {
        internal const int DEFAULT_SIZE = 3;

        private string _destinationCurrency;
        private string _sourceCurrency;
        private float _lots;
        private decimal _price;

        public string DestinationCurrency
        {
            get
            {
                return _destinationCurrency;
            }

            set
            {
                if (value.Length != DEFAULT_SIZE)
                {
                    throw new ArgumentException($"The invalid currency name (length should be {DEFAULT_SIZE})");
                }

                _destinationCurrency = value;
            }
        }

        public float Lots
        {
            get
            {
                return _lots;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"The invalid {nameof(Lots)} value!)");
                }

                _lots = value;
            }
        }

        public decimal Price
        {
            get
            {
                return _price;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"The invalid {nameof(Price)} value!)");
                }

                _price = value;
            }
        }

        public string SourceCurrency
        {
            get
            {
                return _sourceCurrency;
            }

            set
            {
                if (value.Length != DEFAULT_SIZE)
                {
                    throw new ArgumentException($"The invalid currency name (length should be {DEFAULT_SIZE})");
                }

                _sourceCurrency = value;
            }
        }

    }
}
