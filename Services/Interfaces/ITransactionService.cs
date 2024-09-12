using metafar_challenge.Models;

namespace metafar_challenge.Services.Interfaces
{
    public interface ITransactionService
    {
        Task AddTransaction(Transaction transaction);
    }
}
