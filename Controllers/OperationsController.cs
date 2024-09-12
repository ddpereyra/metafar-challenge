using metafar_challenge.DTOs;
using metafar_challenge.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        /// Retrieves the balance of a card.
        /// </summary>
        /// <param name="cardNumber">The card number for which to retrieve the balance.</param>
        /// <returns>The balance of the specified card.</returns>
        /// <response code="200">Returns the balance.</response>
        /// <response code="404">Card or account not found.</response>
        [HttpGet("balance/{cardNumber}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBalance([FromRoute] string cardNumber)
        {
            try
            {
                return Ok(await _operationService.GetBalance(cardNumber));
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Performs a money operation on an account.
        /// </summary>
        /// <param name="req">Contains the card number and the amount to operate.</param>
        /// <returns>A summary of the operation.</returns>
        /// <response code="200">Returns a summary of the operation, including the new balance and a success message.</response>
        /// <response code="404">Card not found or insufficient funds.</response>
        [HttpPost("operation")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Operation([FromBody] OperationInDto req)
        {
            try
            {
                return Ok(await _operationService.Operation(req.CardNumber, req.Amount));
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves the full history of operations for a given card, grouped in pages of 10 records each.
        /// </summary>
        /// <param name="cardNumber">The card number to retrieve the operations history for.</param>
        /// <returns>A list of operations, grouped into pages of 10 records.</returns>
        /// <response code="200">Returns the full history of operations, paginated in groups of 10 records per page.</response>
        /// <response code="404">Card not found.</response>
        [HttpGet("historic/{cardNumber}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPaginatedHistoric([FromRoute] string cardNumber)
        {
            try
            {
                return Ok(await _operationService.GetPaginatedHistory(cardNumber));
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }
    }
}
