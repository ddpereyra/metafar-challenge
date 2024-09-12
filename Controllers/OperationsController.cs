using metafar_challenge.DTOs;
using metafar_challenge.Models;
using metafar_challenge.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace metafar_challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationService _operationService;

        public OperationsController(IOperationService operationService)
        {
            _operationService = operationService;
        }

        /// <summary>
        /// Get the balance of a card.
        /// </summary>
        /// <param name="cardNumber">The card number to retrieve the balance for.</param>
        /// <returns>The balance of a card.</returns>
        /// <response code="200">Returns the balance.</response>
        /// <response code="400">Card or account not found.</response>
        [HttpGet("balance/{cardNumber}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBalance([FromRoute] string cardNumber)
        {
            try
            {
                return Ok(await _operationService.GetBalance(cardNumber));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
