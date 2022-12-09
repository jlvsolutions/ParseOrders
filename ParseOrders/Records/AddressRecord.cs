using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseOrders.Records
{
    internal class AddressRecord : IRecordDef
    {
        public readonly static RecordDef Definition = new("200", 160);

        public ushort Type;
        public string Address1 = "";  // if problems, at least not null
        public string Address2 = "";
        public string City = "";
        public string State = "";
        public string Zip = "";

        public static AddressRecord Create(string record)
        {
            return new()
            {
                Type = ushort.Parse(record.Substring(0, 3)),
                Address1 = record.Substring(3, 50).Trim(),
                Address2 = record.Substring(53, 50).Trim(),
                City = record.Substring(103, 50).Trim(),
                State = record.Substring(153, 2).Trim(),
                Zip = record.Substring(155, 5).Trim()
            };
        }
    }
}
