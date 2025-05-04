//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using BookSphere.Api.Models.Foundations.Readers;
using Microsoft.EntityFrameworkCore;

namespace BookSphere.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Reader> Readers { get; set; }
    }
}
