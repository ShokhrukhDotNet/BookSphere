//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BookSphere.Api.Models.Foundations.Books;

namespace BookSphere.Api.Models.Foundations.Readers
{
    public class Reader
    {
        public Guid ReaderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        [JsonIgnore]
        public List<Book>? Books { get; set; }
    }
}
