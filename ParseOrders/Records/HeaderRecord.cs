using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseOrders.Records
{
    internal class HeaderRecord : IRecordDef
    {
        public readonly static RecordDef Definition = new("100", 180);

        public ushort Type;
        public uint OrderNumber;
        public ushort TotalItems;
        public double TotalCost;
        public DateTime OrderDate;
        public string CustomerName = "";  // if problems, at least not null
        public string CustomerPhone = "";
        public string CustomerEmail = "";
        public bool Paid;
        public bool Shipped;
        public bool Completed;

        public static HeaderRecord Create(string record)
        {
            return new()
            {
                Type = ushort.Parse(record.Substring(0, 3)),
                OrderNumber = uint.Parse(record.Substring(3, 10)),
                TotalItems = ushort.Parse(record.Substring(13, 5)),
                TotalCost = double.Parse(record.Substring(18, 10)),
                OrderDate = DateTime.Parse(record.Substring(28, 19)), // check for valid such as leap year
                CustomerName = record.Substring(47, 50).Trim(),
                CustomerPhone = record.Substring(97, 30).Trim(),
                CustomerEmail = record.Substring(127, 50).Trim(),
                Paid = record[177] == '1',
                Shipped = record[178] == '1',
                Completed = record[179] == '1'
            };
        }
    }
}
