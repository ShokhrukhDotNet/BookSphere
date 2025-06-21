//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using BookSphere.Api.Models.Foundations.Readers;
using BookSphere.Api.Models.Foundations.Readers.Exceptions;

namespace BookSphere.Api.Services.Foundations.Readers
{
    public partial class ReaderService
    {
        private void ValidateReaderOnAdd(Reader reader)
        {
            ValidateReaderNotNull(reader);

            Validate(
                (Rule: IsInvalid(reader.Id), Parameter: nameof(Reader.Id)),
                (Rule: IsInvalid(reader.FirstName), Parameter: nameof(Reader.FirstName)),
                (Rule: IsInvalid(reader.LastName), Parameter: nameof(Reader.LastName)),
                (Rule: IsInvalid(reader.DateOfBirth), Parameter: nameof(Reader.DateOfBirth)));
        }

        private static void ValidateReaderNotNull(Reader reader)
        {
            if (reader is null)
            {
                throw new NullReaderException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static void ValidateReaderId(Guid readerId) =>
            Validate((Rule: IsInvalid(readerId), Parameter: nameof(Reader.Id)));

        private static void ValidateStorageReader(Reader maybeReader, Guid readerId)
        {
            if (maybeReader is null)
            {
                throw new NotFoundReaderException(readerId);
            }
        }

        private static void ValidateReaderOnModify(Reader reader)
        {
            ValidateReaderNotNull(reader);

            Validate(
                (Rule: IsInvalid(reader.Id), Parameter: nameof(Reader.Id)),
                (Rule: IsInvalid(reader.FirstName), Parameter: nameof(Reader.FirstName)),
                (Rule: IsInvalid(reader.LastName), Parameter: nameof(Reader.LastName)),
                (Rule: IsInvalid(reader.DateOfBirth), Parameter: nameof(Reader.DateOfBirth)));
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidReaderException = new InvalidReaderException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidReaderException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidReaderException.ThrowIfContainsErrors();
        }
    }
}
