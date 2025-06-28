//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System.Collections.Generic;
using BookSphere.Api.Models.Foundations.Books;
using BookSphere.Api.Models.Foundations.Readers;

namespace BookSphere.Api.Models.Foundations.ReaderBooks
{
    public class ReaderBook
    {
        public Reader Reader { get; set; }
        public List<Book> Books { get; set; }
    }
}
