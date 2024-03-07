using GeekShopping.CardAPI.Data.ValueObjects;
using GeekShopping.CardAPI.Repository;
using GeekShopping.CardAPI.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace GeekShopping.CardAPI.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class CardController : ControllerBase
	{
		private readonly ICardRepository _cardRepository;

		public CardController(ICardRepository cardRepository)
		{
			_cardRepository = cardRepository;
		}

		[HttpGet("find-card/{userId}")]
		public async Task<ActionResult<IEnumerable<CardVO>>> FindCardByUserId(string userId)
		{
			var cards = await _cardRepository.FindCardByUserId(userId);
			if (cards == null) { return NotFound(); }
			return Ok(cards);
		}
	}
}
