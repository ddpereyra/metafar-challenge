using metafar_challenge.Models;

namespace metafar_challenge.Services.Interfaces
{
    public interface ICardService
    {
        Task<Card> GetValidatedCard(string cardNumber);
        Task UpdateDb(Card card);
    }
}
