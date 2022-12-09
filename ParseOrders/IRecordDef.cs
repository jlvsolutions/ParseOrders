using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseOrders
{
    /// <summary>
    /// Structure to be used to identify valid data file records.
    /// </summary>
    public interface IRecordDef
    {
        public readonly static RecordDef Definition;
    }
}
