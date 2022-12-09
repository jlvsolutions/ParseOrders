using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParseOrders.Records;

namespace ParseOrders
{
    internal class Orders
    {
        public List<Order> OrdersList = new();
        public bool HasErrors => OrdersList.Any(o => !o.Success);

        public void ParseFile(string fileName)
        {
            using RecordReader stream = new RecordReader(fileName);
            
            // Inform the reader of valid record definitions.
            stream.AddRecordDef(HeaderRecord.Definition);
            stream.AddRecordDef(AddressRecord.Definition);
            stream.AddRecordDef(LineItemRecord.Definition);

            while (!stream.EndOfStream)
            {
                OrdersList.Add(Order.Create(stream));
            }
            stream.Close();
        }
    }
}
