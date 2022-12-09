using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseOrders.Records
{
    internal class LineItemRecord : IRecordDef
    {
        public readonly static RecordDef Definition = new("300", 80);

        public ushort Type;
        public byte LineNumber;
        public ushort Quantity;
        public double CostEach;
        public double CostTotal;
        public string Description = "";

        public static LineItemRecord Create(string record)
        {
            return new()
            {
                Type = ushort.Parse(record.Substring(0, 3)),
                LineNumber = byte.Parse(record.Substring(3, 2)),
                Quantity = byte.Parse(record.Substring(5, 5)),
                CostEach = double.Parse(record.Substring(10, 10)),
                CostTotal = double.Parse(record.Substring(20, 10)),
                Description = record.Substring(30, 50).Trim()
            };
        }
    }
}
