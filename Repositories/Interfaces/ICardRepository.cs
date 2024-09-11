using metafar_challenge.Models;
using static metafar_challenge.Repositories.Interfaces.IRepository;

namespace metafar_challenge.Repositories.Interfaces
{
    public interface ICardRepository : IRepository<Card>
    {
        Task<Card> GetByCardNumber(string cardNumber);
    }
}
