namespace No7.Solution.Console
{
    // поля лучше сделать свойствами: могут быть добавлены проверки на валидацию
    internal class TradeRecord
    {
        internal string DestinationCurrency;
        internal float Lots;
        internal decimal Price;
        internal string SourceCurrency;
    }
}