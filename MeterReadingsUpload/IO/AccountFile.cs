using CsvHelper.Configuration.Attributes;

namespace MeterReadingsUpload.IO
{
    public class AccountFile
    {
        [Name("AccountId")]
        public int AccountId { get; set; }

        [Name("FirstName")]
        public string FirstName { get; set; }

        [Name("LastName")]
        public string LastName { get; set; }
    }
}