using CsvHelper;
using CsvHelper.Configuration;
using MeterReadingsUpload.DAL;
using MeterReadingsUpload.DomainModels;
using MeterReadingsUpload.IO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace MeterReadingsUpload.Services
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _context;

        public AccountService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddMeterReadings(StreamReader reader)
        {
            var result = true;
            try
            {
                var malformedRow = false;
                var badData = new List<string>();
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    BadDataFound = x =>
                    {
                        malformedRow = true;
                        badData.Add(x.Context.Parser.RawRecord);
                    }
                };
                using (var csvReader = new CsvReader(reader, config))
                {
                    while (csvReader.Read())
                    {
                        MeterReadingFile record = null;

                        try
                        {
                            //Catches conversion errors, could be handled better
                            record = csvReader.GetRecord<MeterReadingFile>();
                        }
                        catch
                        {
                            malformedRow = true;
                            result = false;
                        }

                        if (!malformedRow)
                        {
                            var meterReading = new MeterReading()
                            {
                                AccountId = record.AccountId,
                                MeterReadingDateTime = DateTime.ParseExact(record.MeterReadingDateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                                MeterReadValue = record.MeterReadValue
                            };
                            await _context.MeterReadings.AddAsync(meterReading);
                            try
                            {
                                _context.SaveChanges();
                            }
                            catch
                            {
                                //If the account doesn't exist we'll get a FK error
                                //Skip
                                result = false;
                            }
                        }

                        malformedRow = false;
                    }
                }
                reader.Dispose();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddMeterReadig(MeterReading meterReading)
        {
            try
            {
                _context.MeterReadings.Add(meterReading);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}