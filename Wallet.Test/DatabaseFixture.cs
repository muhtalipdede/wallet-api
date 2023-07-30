using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wallet.Model;

namespace Wallet.Test
{
    public class DatabaseFixture : IDisposable
    {
        public WalletDbContext DbContext { get; private set; }
        private DbContextOptions<WalletDbContext> _options;

        public DatabaseFixture()
        {
            _options = new DbContextOptionsBuilder<WalletDbContext>()
                .UseInMemoryDatabase(databaseName: "TestWalletDB")
                .Options;

            DbContext = new WalletDbContext(_options);

            DbContext.Database.EnsureCreated();
            SeedData();
        }

        private void SeedData()
        {

            DbContext.Wallets.Add(new Model.Wallet 
            {
                WalletId = Guid.NewGuid(),
                Name = "Test Wallet",
                Balance = 100,
                Currency = "USD",
                UserId = Guid.NewGuid()
            });

            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
            DbContext.Dispose();
        }
    }
}
