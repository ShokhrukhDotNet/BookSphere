//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Linq;
using BookSphere.Api.Controllers;
using BookSphere.Api.Models.Foundations.Readers;
using BookSphere.Api.Models.Foundations.Readers.Exceptions;
using BookSphere.Api.Services.Processings.Readers;
using Moq;
using RESTFulSense.Controllers;
using Tynamix.ObjectFiller;
using Xeptions;

namespace BookSphere.Api.Tests.Unit.Controllers.Readers
{
    public partial class ReadersControllerTests : RESTFulController
    {
        private readonly Mock<IReaderProcessingService> readerProcessingServiceMock;
        private readonly ReadersController readersController;

        public ReadersControllerTests()
        {
            this.readerProcessingServiceMock = new Mock<IReaderProcessingService>();

            this.readersController = new ReadersController(
                readerProcessingService: this.readerProcessingServiceMock.Object);
        }

        public static TheoryData<Xeption> ValidationExceptions()
        {
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new ReaderValidationException(
                    innerException : someInnerException),

                new ReaderDependencyValidationException(
                    innerException : someInnerException)
            };
        }

        public static TheoryData<Xeption> ServerExceptions()
        {
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new ReaderDependencyException(
                    innerException: someInnerException),

                new ReaderServiceException(
                    innerException: someInnerException)
            };
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static IQueryable<Reader> CreateRandomReaders() =>
            CreateReaderFiller().Create(count: GetRandomNumber()).AsQueryable();

        private static Reader CreateRandomReader() =>
            CreateReaderFiller().Create();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<Reader> CreateReaderFiller()
        {
            var filler = new Filler<Reader>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset)
                .OnProperty(reader => reader.Books).IgnoreIt();

            return filler;
        }
    }
}
