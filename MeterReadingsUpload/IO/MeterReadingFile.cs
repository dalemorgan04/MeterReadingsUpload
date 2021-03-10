using System;

namespace MeterReadingsUpload.IO
{
    public class MeterReadingFile
    {
        public int AccountId { get; set; }
        public string MeterReadingDateTime { get; set; } //CsvHelper isn't able to convert upload sheet format to datetime
        public int MeterReadValue { get; set; }
    }
}