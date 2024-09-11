using metafar_challenge.Data;
using metafar_challenge.Models;
using Newtonsoft.Json;

namespace metafar_challenge.Seed
{
    public class DbInitializer
    {
        public static async Task Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Cards.Any())
            {
                return;
            }

            var seedDataFile = Path.Combine(Directory.GetCurrentDirectory(), "Seed", "seed-data.json");
            var seedData = File.ReadAllText(seedDataFile);
            var cards = JsonConvert.DeserializeObject<List<CardSeedData>>(seedData);

            if (cards != null)
            {
                foreach (var card in cards)
                {
                    context.Cards.Add(new Card
                    {
                        CardNumber = card.CardNumber,
                        Pin = card.Pin,
                        IsBlocked = card.IsBlocked,
                        FailedAttempts = card.FailedAttempts
                    });
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
