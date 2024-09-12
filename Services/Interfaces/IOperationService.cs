using metafar_challenge.DTOs;

namespace metafar_challenge.Services.Interfaces
{
    public interface IOperationService
    {
        Task<BalanceDto> GetBalance(string cardNumber);
    }
}
