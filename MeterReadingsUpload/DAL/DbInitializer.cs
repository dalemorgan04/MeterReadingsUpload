using CsvHelper;
using MeterReadingsUpload.DomainModels;
using MeterReadingsUpload.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MeterReadingsUpload.DAL
{
    public class DbInitializer
    {
        public static void Initialize(DataContext context, IConfiguration configuration)
        {
            context.Database.EnsureCreated();
            if (configuration.GetSection("SeedFile").Exists())
            {
                var seedFileLocation = configuration.GetSection("SeedFile").Value;
                try
                {
                    using (var reader = new StreamReader(seedFileLocation))
                    using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var accounts = csvReader.GetRecords<Account>().ToList();
                        context.Accounts.AddRange(accounts);
                        context.SaveChanges();
                    }
                }
                catch (ReaderException e)
                {
                    throw new Exception(e.Message);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
    }
}
