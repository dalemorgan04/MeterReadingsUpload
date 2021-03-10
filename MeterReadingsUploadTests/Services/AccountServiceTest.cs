using MeterReadingsUpload.DAL;
using MeterReadingsUpload.DomainModels;
using MeterReadingsUpload.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeterReadingsUploadTests.Services
{
    [TestFixture]
    public class AccountServiceTest
    {

        private IAccountService _accountService;
        private Mock<DataContext> _mockContext;
        private Mock<DbSet<MeterReading>> _mockSet;
        private IQueryable<MeterReading> readingsList;

        [OneTimeSetUp]
        public void Initialize()
        {
            readingsList = new List<MeterReading>
            {
                new MeterReading() { AccountId = 1, MeterReadingDateTime = DateTime.Now, MeterReadingId = 1, MeterReadValue = 99 }
            }.AsQueryable();

            _mockSet = new Mock<DbSet<MeterReading>>();
            _mockSet.As<IQueryable<MeterReading>>().Setup(m => m.Provider).Returns(readingsList.Provider);
            _mockSet.As<IQueryable<MeterReading>>().Setup(m => m.Expression).Returns(readingsList.Expression);
            _mockSet.As<IQueryable<MeterReading>>().Setup(m => m.ElementType).Returns(readingsList.ElementType);
            _mockSet.As<IQueryable<MeterReading>>().Setup(m => m.GetEnumerator()).Returns(readingsList.GetEnumerator());

            _mockContext = new Mock<DataContext>();
            _mockContext.Setup(c => c.Set<MeterReading>()).Returns(_mockSet.Object);
            _mockContext.Setup(c => c.MeterReadings).Returns(_mockSet.Object);

            _accountService = new AccountService(_mockContext.Object);
        }

        [TestCase]
        public void Can_Add_MeterReading()
        {
            //This test is unfinished, ran out of time

            //Arrange
            //int id = 1;
            //MeterReading meterReading = new MeterReading() { AccountId = 1, MeterReadingDateTime = DateTime.Now, MeterReadingId = 1, MeterReadValue = 99 };
            //_mockSet.Setup(m => m.Add(meterReading)).Returns( m =>
            //{
            //    m.MeterReadingId = id;
            //    return m;
            //});

            ////Act
            //_accountService.AddMeterReadig(meterReading);

            ////Assert
            //Assert.AreEqual(id, meterReading.MeterReadingId);
            //_mockContext.Verify(mocks => mocks.SaveChangesAsync(), Times.Once());
        }
    }
}