﻿namespace No7.Solution.Console
{
    // поля лучше сделать свойствами: могут быть добавлены проверки на валидацию
    // + поля должны private :)
    internal class TradeRecord
    {
        internal string DestinationCurrency;
        internal float Lots;
        internal decimal Price;
        internal string SourceCurrency;
    }
}