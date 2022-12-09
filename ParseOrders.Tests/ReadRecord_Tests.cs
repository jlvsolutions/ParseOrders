using System;
using System.IO;
using System.Text;

using ParseOrders;
using ParseOrders.Records;

namespace ParseOrders.Tests
{
    public class ReadRecord_Tests
    {
        private readonly RecordDef _testDef;

        public ReadRecord_Tests()
        {
            _testDef = new RecordDef("100", 13);
        }

        [Fact]
        public void ReadRecord_ShouldReturnFirstGoodWhenBadLineType()
        {
            // Arrange
            RecordReader rr = ReadRecordHelper.Seed("9001234567890", "1000987654321", "1003333333333");
            rr.AddRecordDef(_testDef);

            // Act
            string? rec = rr.ReadRecord();

            // Assert
            Assert.Equal("1000987654321", rec);
        }

        [Fact]
        public void ReadRecord_ShouldReturnFirstGoodWhenBadRecordLength()
        {
            // Arrange
            RecordReader rr = ReadRecordHelper.Seed("1001234567890MMM", "1000987654321", "1003333333333");
            rr.AddRecordDef(_testDef);

            // Act
            string? rec = rr.ReadRecord();

            // Assert
            Assert.Equal("1000987654321", rec);
        }

        [Fact]
        public void ReadRecord_ShouldReadAllValidRecords()
        {
            // Arrange
            RecordReader rr = ReadRecordHelper.Seed("1001234567890", 
                                                    "1XX000987654321", 
                                                    "1003333333333x", 
                                                    "1001234512345",
                                                    "400111987654321",
                                                    "50011223344556677889900"
                                                    );
            rr.AddRecordDef(_testDef);
            rr.AddRecordDef(new RecordDef("400", 15));
            rr.AddRecordDef(new RecordDef("500", 23));

            // Act
            List<string?> recs = new();
            string? rec;
            while ((rec = rr.ReadRecord()) != null)
                recs.Add(rec);

            // Assert
            Assert.Equal(4, recs.Count);
            Assert.Equal("1001234567890", recs[0]);
            Assert.Equal("1001234512345", recs[1]);
            Assert.Equal("400111987654321", recs[2]);
            Assert.Equal("50011223344556677889900", recs[3]);
        }

        [Fact]
        public void ReadRecord_ShouldSkipOverEmptyRecords()
        {
            // Arrange
            RecordReader rr = ReadRecordHelper.Seed("1001234567890", "\0", "", "100ONETWOFOUR");
            rr.AddRecordDef(_testDef);

            // Act
            List<string?> recs = new();
            string? rec;
            while ((rec = rr.ReadRecord()) != null)
                recs.Add(rec);

            // Assert
            Assert.Equal(2, recs.Count);
            Assert.Equal("1001234567890", recs[0]);
            Assert.Equal("100ONETWOFOUR", recs[1]);
        }

        [Fact]
        public void ReadRecord_ShouldSkipOverRecordsNotDefined()
        { 
            // Arrange
            RecordReader rr = ReadRecordHelper.Seed("1001234567890", "\0", "", "100ONETWOFOUR");
            rr.AddRecordDef(new RecordDef("101", 13));

            // Act
            List<string?> recs = new();
            string? rec;
            while ((rec = rr.ReadRecord()) != null)
                recs.Add(rec);

            // Assert
            Assert.Empty(recs);
        }
    }
}