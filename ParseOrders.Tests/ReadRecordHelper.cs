using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParseOrders.Extensions;

namespace ParseOrders.Tests.Extensions
{
    public static class ReadRecordHelper
    {
        static MemoryStream? _ms;
        static StreamWriter? _sw;
        static StreamReader? _sr;

        public static void Close()
        {
            if (_ms != null)
                _ms.Dispose();
            if (_sw != null)
                _sw.Dispose();
        }

        public static StreamReader Seed(params string[] recs)
        {
            Close();
            _ms = new MemoryStream();
            _sw = new StreamWriter(_ms);

            _ms.Seek(0, SeekOrigin.Begin);
            foreach(string s in recs)
                _sw.WriteLine(s);
            _sw.Flush();
            _ms.Seek(0, SeekOrigin.Begin);
            _sr = new StreamReader(_ms);
            _sr.ClearRecordDefs();
            return new StreamReader(_ms);
        }
    }
}
