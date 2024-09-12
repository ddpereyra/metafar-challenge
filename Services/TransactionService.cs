using metafar_challenge.Models;
using metafar_challenge.Repositories.Interfaces;
using metafar_challenge.Services.Interfaces;

namespace metafar_challenge.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository) {
            _transactionRepository = transactionRepository;
        }

        public async Task AddTransaction(Transaction transaction)
        {
            await _transactionRepository.AddAsync(transaction);
        }
    }
}
