using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseOrders
{
    internal class HeaderRecord
    {
        private ushort _type;
        private uint _orderNumber;
        private ushort _totalItems;
        private double _totalCost;
        private DateTime _orderDate;
        private string _customerName = "";  // if problems, at least not null
        private string _customerPhone = "";
        private string _customerEmail = "";
        private bool _paid;
        private bool _shipped;
        private bool _completed;

        public static HeaderRecord Create(string record)
        {
            HeaderRecord headerRecord = new HeaderRecord();

            headerRecord._type = ushort.Parse(record.Substring(0, 3));
            headerRecord._orderNumber = uint.Parse(record.Substring(3, 10));
            headerRecord._totalItems = ushort.Parse(record.Substring(13, 5));
            headerRecord._totalCost = double.Parse(record.Substring(18, 10));
            headerRecord._orderDate = DateTime.Parse(record.Substring(28, 19)); // check for valid such as leap year
            headerRecord._customerName = record.Substring(47, 50).Trim();
            headerRecord._customerPhone = record.Substring(97, 30).Trim();
            headerRecord._customerEmail = record.Substring(127, 50).Trim();
            headerRecord._paid = record.Substring(177, 1) == "1";
            headerRecord._shipped = record.Substring(178, 1) == "1";
            headerRecord._completed = record.Substring(179, 1) == "1";
         
            return headerRecord;
        }

        public ushort Type => _type;
        public uint OrderNumber => _orderNumber;
        public ushort TotalItems => _totalItems;
        public double TotalCost => _totalCost;
        public DateTime OrderDate => _orderDate;
        public string CustomerName => _customerName;
        public string CustomerPhone => _customerPhone;
        public string CustomerEmail => _customerEmail;
        public bool Paid => _paid;
        public bool Shipped => _shipped;
        public bool Completed => _completed;
    }
}
