using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseOrders
{
    internal class Order
    {
        private List<string> _errors;
        private HeaderRecord? _header;
        private AddressRecord? _address;
        private List<LineItemRecord>? _lineItems;

        public Order()
        {
            _errors = new List<string>();
        }
        public static Order Create(StreamReader stream)
        {
            Order order = new Order();

            // Header record
            var record = stream.ReadLine();
            if (record == null) // safety checks
                return order;
            try
            {
                order._header = HeaderRecord.Create(record);
            }
            catch(Exception ex)
            {
                var msg = $"An error occurred while parsing this order's header record:  {ex.Message}";
                order._errors.Add(msg);
                Console.Error.WriteLine(msg);
            }

            // Address record
            record = stream.ReadLine();
            if (record == null)
                return order;
            try
            {
                order._address = AddressRecord.Create(record);
            }
            catch(Exception ex)
            {
                var msg = $"An error occurred while parsing this order's address record:  {ex.Message}";
                order._errors.Add(msg);
                Console.Error.WriteLine(msg);
            }

            order._lineItems = new List<LineItemRecord>();
            while (stream.Peek() == '3')
            {
                record = stream.ReadLine();
                try
                {
                    order._lineItems.Add(LineItemRecord.Create(record!));  // peek ensures record not null
                }
                catch(Exception ex)
                {
                    var msg = $"An error occurred while parsing a line item for this order:  {ex.Message}";
                    order._errors.Add(msg);
                    Console.Error.WriteLine(msg);
                }
            }
            // End of line items or EOF reached.

            return order;
        }

        public HeaderRecord? Header => _header;
        public AddressRecord? Address => _address;
        public List<LineItemRecord>? lineItems => _lineItems;
        public bool Success => _errors.Count == 0;
        public List<string> Errors => _errors;

    }
}
