using metafar_challenge.Data;
using metafar_challenge.Models;
using metafar_challenge.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace metafar_challenge.Repositories
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        private readonly AppDbContext _context;

        public CardRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Card> GetByCardNumber(string cardNumber)
        {
            return await _context.Cards
                .Include(c => c.BankAccount)
                .ThenInclude(b => b.Transactions)
                .FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
        }
    }
}
