using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseOrders
{
    internal class AddressRecord
    {
        private ushort _type;
        private string _address1 = "";  // if problems, at least not null
        private string _address2 = "";
        private string _city = "";
        private string _state = "";
        private string _zip = "";

        public static AddressRecord Create(string record)
        {
            AddressRecord addressRecord = new AddressRecord();
            addressRecord._type = ushort.Parse(record.Substring(0, 3));
            addressRecord._address1 = record.Substring(3, 50).Trim();
            addressRecord._address2 = record.Substring(53, 50).Trim();
            addressRecord._city = record.Substring(103, 50).Trim();
            addressRecord._state = record.Substring(153, 2).Trim();
            addressRecord._zip = record.Substring(155, 5).Trim();
            return addressRecord;
        }

        public ushort Type => _type;
        public string Address1 => _address1;
        public string Address2 => _address2;
        public string City => _city;
        public string State => _state;
        public string Zip => _zip;

    }
}
