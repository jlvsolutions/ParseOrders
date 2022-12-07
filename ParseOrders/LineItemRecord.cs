using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseOrders
{
    internal class LineItemRecord
    {
        private ushort _type;
        private byte _lineNumber;
        private ushort _quantity;
        private double _costEach;
        private double _costTotal;
        private string _description = "";

        public static LineItemRecord Create(string record)
        {
            LineItemRecord lineItemRecord = new LineItemRecord();
            lineItemRecord._type = ushort.Parse(record.Substring(0, 3));
            lineItemRecord._lineNumber = byte.Parse(record.Substring(3, 2));
            lineItemRecord._quantity = byte.Parse(record.Substring(5, 5));
            lineItemRecord._costEach = double.Parse(record.Substring(10, 10));
            lineItemRecord._costTotal = double.Parse(record.Substring(20, 10));
            lineItemRecord._description = record.Substring(30, 50).Trim();
            return lineItemRecord;
        }

        public ushort Type => _type;
        public byte LineNumber => _lineNumber;
        public ushort Quantity => _quantity;
        public double CostEach => _costEach;
        public double CostTotal => _costTotal;
        public string Description => _description;
    }
}
