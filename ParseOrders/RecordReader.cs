using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParseOrders.Records;

namespace ParseOrders
{
    public class RecordReader : StreamReader
    {
        private readonly List<RecordDef> _recordDefs;
        public string? LastReadRecord;

        public RecordReader(string fileName) : base(fileName)
        {
            _recordDefs = new List<RecordDef>();
            LastReadRecord = null;
        }

        public RecordReader(Stream stream) : base(stream)
        {
            _recordDefs = new List<RecordDef>();
            LastReadRecord = null;
        }

        public string? ReadRecord()
        {
            string? rec;

            while ((rec = ReadLine()) != null)
            {
                if (IsValidRec(rec))
                {
                    LastReadRecord = rec;
                    return rec;
                }
            }
            return null;
        }

        public void AddRecordDef(RecordDef recordDef)
        {
            _recordDefs.Add(recordDef);
        }

        public void ClearRecordDefs()
        {
            _recordDefs.Clear();
        }
        private bool IsValidRec(string rec)
        {
            return _recordDefs.Any(d => rec.Length == d.Length && rec.StartsWith(d.LineTypeId));
        }
    }
}
