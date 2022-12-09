using ParseOrders.Extensions;
using ParseOrders.Records;

namespace ParseOrders
{
    internal class Order
    {
        public HeaderRecord? Header;
        public AddressRecord? Address;
        public List<LineItemRecord> LineItems;
        public List<string> Errors;
        
        public bool Success => Errors.Count == 0;

        public Order()
        {
            LineItems = new List<LineItemRecord>();
            Errors = new List<string>();
        }

        public static Order Create(StreamReader stream)
        {
            Order order = new Order();

            string? record = stream.LastReadRecord() ?? stream.ReadRecord();

            while (record != null)
            { 
                if (HeaderRecord.Definition.IsMatch(record))
                {
                    // Header record
                    try { order.Header = HeaderRecord.Create(record); }
                    catch (Exception ex)
                    {
                        var msg = $"An error occurred while parsing this order's header record:  {ex.Message}";
                        order.Errors.Add(msg);
                        Console.Error.WriteLine(msg);
                    }
                }
                else if (AddressRecord.Definition.IsMatch(record))
                {
                    // Address record
                    try { order.Address = AddressRecord.Create(record); }
                    catch (Exception ex)
                    {
                        var msg = $"An error occurred while parsing this order's address record:  {ex.Message}";
                        order.Errors.Add(msg);
                        Console.Error.WriteLine(msg);
                    }
                }
                else if (LineItemRecord.Definition.IsMatch(record))
                {
                    // Line item records
                    try { order.LineItems.Add(LineItemRecord.Create(record)); }
                    catch (Exception ex)
                    {
                        var msg = $"An error occurred while parsing a line item for this order:  {ex.Message}";
                        order.Errors.Add(msg);
                        Console.Error.WriteLine(msg);
                    }
                }

                record = stream.ReadRecord();
                if (HeaderRecord.Definition.IsMatch(record)) // We have started a new order
                {
                    return order;
                }

            }
            return order;
        }
    }
}
