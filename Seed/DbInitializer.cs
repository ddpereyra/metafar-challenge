using metafar_challenge.Data;
using metafar_challenge.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace metafar_challenge.Seed
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public DbInitializer(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task Initialize()
        {
            _context.Database.EnsureCreated();

            bool deleteExistingData = _configuration.GetValue<bool>("DeleteExistingData");

            if (_context.Cards.Any() || _context.BankAccounts.Any() || _context.Transactions.Any())
            {
                if (deleteExistingData)
                {
                    _context.Transactions.RemoveRange(_context.Transactions);
                    _context.Cards.RemoveRange(_context.Cards);
                    _context.BankAccounts.RemoveRange(_context.BankAccounts);

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return;
                }
            }

            var seedDataFile = Path.Combine(Directory.GetCurrentDirectory(), "Seed", "seed-data.json");
            var seedData = File.ReadAllText(seedDataFile);
            var bankAccounts = JsonConvert.DeserializeObject<List<BankAccount>>(seedData);

            if (bankAccounts != null)
            {
                _context.BankAccounts.AddRange(bankAccounts);
                await _context.SaveChangesAsync();
            }
        }
    }
}
