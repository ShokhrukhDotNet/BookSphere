//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Readers;

namespace BookSphere.Api.Services.Foundations.Readers
{
    public interface IReaderService
    {
        ValueTask<Reader> AddReaderAsync(Reader reader);
    }
}
