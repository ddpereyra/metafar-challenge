using metafar_challenge.DTOs;
using metafar_challenge.Models;
using metafar_challenge.Services.Interfaces;

namespace metafar_challenge.Services
{
    public class OperationService : IOperationService
    {
        private readonly ICardService _cardService;

        public OperationService(ICardService cardService)
        {
            _cardService = cardService;
        }
        public async Task<BalanceDto> GetBalance(string cardNumber)
        {
            Card card = await _cardService.GetValidatedCard(cardNumber);

            return new BalanceDto
            {
                AccountHolderName = card.BankAccount.AccountHolderName,
                AccountNumber = card.BankAccount.AccountNumber,
                Balance = card.BankAccount.Balance,
                LastWithdrawalDate = card.BankAccount.Transactions.OrderByDescending(dt => dt.TransactionDate).Select(dt => dt.TransactionDate).FirstOrDefault(),
            };
        }
    }
}
