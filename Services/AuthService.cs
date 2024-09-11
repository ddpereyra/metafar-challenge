using metafar_challenge.Models;
using metafar_challenge.Repositories.Interfaces;
using metafar_challenge.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace metafar_challenge.Services
{
    public class AuthService : IAuthService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IConfiguration _configuration;

        public AuthService(ICardRepository cardRepository, IConfiguration configuration)
        {
            _cardRepository = cardRepository;
            _configuration = configuration;
        }

        public async Task<string> Login(string cardNumber, string pin)
        {
            var card = await _cardRepository.GetByCardNumber(cardNumber);

            if (card == null)
                throw new Exception("Card not found.");

            if (card.IsBlocked)
                throw new Exception("Card is blocked.");

            if (card.Pin != pin)
            {
                card.FailedAttempts++;

                if (card.FailedAttempts >= int.Parse(_configuration["MaxFailedAttempts"]))
                {
                    card.IsBlocked = true;
                }

                await _cardRepository.Update(card);

                throw new Exception("Invalid PIN.");
            }

            card.FailedAttempts = 0;
            await _cardRepository.Update(card);

            return GenerateJwtToken(card);
        }

        private string GenerateJwtToken(Card card)
        {
            var base64Key = _configuration["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(base64Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("CardNumber", card.CardNumber)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
