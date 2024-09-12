using metafar_challenge.Models;

namespace metafar_challenge.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction transaction);
    }
}
