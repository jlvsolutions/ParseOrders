namespace ParseOrders.Extensions
{
    public static class StreamReaderExtension
    {
        private static List<RecordDef> _recordDefs = new List<RecordDef>();
        private static string? _lastRecord = null;

        /// <summary>
        /// Reads records from the given stream and returns the first record that 
        /// matches any of the valid record definitions which have been added
        /// via the <see cref="AddRecordDef(StreamReader, RecordDef)"/> extension method.
        /// </summary>
        /// <param name="reader">The stream to read from.</param>
        /// <returns>The first record that matches any given <see cref="RecordDef"/> spec, or <see cref="null"/>
        /// if the end of the input stream is reached.</returns>
        public static string? ReadRecord(this StreamReader reader)
        {
            string? rec;

            while ((rec = reader.ReadLine()) != null)
            {
                if (IsValidRec(rec))
                {
                    _lastRecord = rec;
                    return rec;
                }
            }
            return null;
        }

        /// <summary>
        /// Adds a <see cref="RecordDef"/> to the collection of definitions
        /// that this <see cref="StreamReader"/> will use in determining if
        /// a record is valid when using the <see cref="ReadRecord"/> extension.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="recordDef"></param>
        public static void AddRecordDef(this StreamReader reader, RecordDef recordDef)
        {
            _recordDefs.Add(recordDef);
        }

        public static void ClearRecordDefs(this StreamReader reader)
        {
            _recordDefs.Clear();
        }
        private static bool IsValidRec(string rec)
        {
            return _recordDefs.Any(d => rec.Length == d.Length && rec.StartsWith(d.LineTypeId));
        }

        public static string? LastReadRecord(this StreamReader reader)
        {
            return _lastRecord;
        }
    }
}
