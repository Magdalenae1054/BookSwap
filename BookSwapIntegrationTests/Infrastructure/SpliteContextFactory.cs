using System.Data.Common;
using BookSwap.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BookSwapIntegrationTests.Infrastructure;

public static class SqliteContextFactory
{
    public static (BookSwapContext ctx, DbConnection conn) Create()
    {
        var conn = new SqliteConnection("DataSource=:memory:");
        conn.Open();

        var options = new DbContextOptionsBuilder<BookSwapContext>()
            .UseSqlite(conn)
            .Options;

        var ctx = new BookSwapContext(options);
        ctx.Database.EnsureCreated(); // stvara tablice + seeding iz OnModelCreating

        return (ctx, conn);
    }
}
