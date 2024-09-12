using metafar_challenge.Data;
using metafar_challenge.Models;
using metafar_challenge.Repositories.Interfaces;
using metafar_challenge.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace metafar_challenge.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<Card> GetValidatedCard(string cardNumber)
        {
            Card card = await _cardRepository.GetByCardNumber(cardNumber);

            if (card == null)
                throw new Exception("Card not found.");

            if (card.IsBlocked)
                throw new Exception("Card is blocked.");

            return card;
        }

        public async Task UpdateDb(Card card)
        {
            await _cardRepository.Update(card);
        }
    }
}
