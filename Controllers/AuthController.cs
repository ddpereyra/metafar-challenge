﻿using metafar_challenge.DTOs;
using metafar_challenge.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace metafar_challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) 
        { 
            _authService = authService;
        }

        /// <summary>
        /// Authenticates a card based on the card number and PIN.
        /// </summary>
        /// <returns>A JWT token if authentication is successful.</returns>
        /// <response code="200">Returns the JWT token.</response>
        /// <response code="400">If the card number or PIN is invalid.</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginDto req)
        {
            try
            {
                string token = await _authService.Login(req.CardNumber, req.Pin);
                return Ok(new { Token = token});
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
