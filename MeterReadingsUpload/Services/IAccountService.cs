using MeterReadingsUpload.DomainModels;
using System.IO;
using System.Threading.Tasks;

namespace MeterReadingsUpload.Services
{
    public interface IAccountService
    {
        Task<bool> AddMeterReadings(StreamReader reader);

        Task<bool> AddMeterReadig(MeterReading meterReading);
    }
}