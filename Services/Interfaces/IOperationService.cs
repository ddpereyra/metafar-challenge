using metafar_challenge.DTOs;
using metafar_challenge.Models;

namespace metafar_challenge.Services.Interfaces
{
    public interface IOperationService
    {
        Task<BalanceDto> GetBalance(string cardNumber);
        Task<ResumeOutDto> Operation(string cardNumber, decimal amount);
        Task<PaginatedResult<PaginatedOperations>> GetPaginatedHistory(string cardNumber);
    }
}
