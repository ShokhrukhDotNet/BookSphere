﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using BookSphere.Api.Models.Foundations.Books;
using BookSphere.Api.Models.Foundations.Books.Exceptions;

namespace BookSphere.Api.Services.Foundations.Books
{
    public partial class BookService
    {
        private void ValidateBookOnAdd(Book book)
        {
            ValidateBookNotNull(book);

            Validate(
                (Rule: IsInvalid(book.BookId), Parameter: nameof(Book.BookId)),
                (Rule: IsInvalid(book.BookTitle), Parameter: nameof(Book.BookTitle)),
                (Rule: IsInvalid(book.Author), Parameter: nameof(Book.Author)),
                (Rule: IsInvalid(book.Genre), Parameter: nameof(Book.Genre)),
                (Rule: IsInvalid(book.ReaderId), Parameter: nameof(Book.ReaderId)));
        }

        private static void ValidateBookNotNull(Book book)
        {
            if (book is null)
            {
                throw new NullBookException();
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

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidBookException = new InvalidBookException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidBookException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidBookException.ThrowIfContainsErrors();
        }
    }
}
