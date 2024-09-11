namespace metafar_challenge.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(string cardNumber, string pin);
    }
}
