namespace Framework.Tests.API.DataObject
{
    public class CommandData
    {
        public string DotNumber { get; set; }
        public string FilingType { get; set; }
        public string UserEmail { get; set; }
    }

    public class CommandProcessorCommand
    {
        public string AggregateName { get; set; }
        public string AggregateId { get; set; }
        public string CommandName { get; set; }
        public object CommandData { get; set; }
        public string QuoteId { get; set; }
    }
}
