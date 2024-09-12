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
        private readonly ICardService _cardService;
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration, ICardService cardService)
        {
            _cardService = cardService;
            _configuration = configuration;
        }

        public async Task<string> Login(string cardNumber, string pin)
        {
            Card card = await _cardService.GetValidatedCard(cardNumber);

            if (card.Pin != pin)
            {
                card.FailedAttempts++;

                if (card.FailedAttempts >= int.Parse(_configuration["MaxFailedAttempts"]))
                {
                    card.IsBlocked = true;
                }

                await _cardService.UpdateDb(card);

                throw new Exception("Invalid PIN.");
            }

            card.FailedAttempts = 0;
            await _cardService.UpdateDb(card);

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
