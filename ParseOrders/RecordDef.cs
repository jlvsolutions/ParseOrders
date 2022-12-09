using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseOrders
{
    /// <summary>
    /// Identifies a valid data file record.
    /// </summary>
    public struct RecordDef
    {
        /// <summary>
        /// Line type identifier for the record.
        /// </summary>
        public string LineTypeId = "";
        /// <summary>
        /// Length of the record.
        /// </summary>
        public ushort Length = 0;

        public RecordDef(string lineTypeId, ushort length)
        {
            LineTypeId = lineTypeId;
            Length = length;
        }

        public bool IsMatch(string? rec)
        {
            return rec == null ? false : (rec.Length == Length && rec.StartsWith(LineTypeId));
        }
    }
}
