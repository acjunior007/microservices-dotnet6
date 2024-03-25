using GeekShopping.CardAPI.Data.ValueObjects;
using GeekShopping.CardAPI.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

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

		[HttpPost("add-card")]
		public async Task<ActionResult<CardVO>> AddCard([FromBody] CardVO vo)
		{
			var cards = await _cardRepository.SaveOrUpdate(vo);
			return Ok(cards);
		}

		[HttpPut("update-card/{id}")]
		public async Task<ActionResult<CardVO>> UpdateCard([FromBody] CardVO vo)
		{
			var cards = await _cardRepository.SaveOrUpdate(vo);
			return Ok(cards);
		}

		[HttpDelete("remove-card")]
		public async Task<ActionResult<bool>> RemoveCard(long id)
		{
			var status = await _cardRepository.RemoveFromCard(id);

			return !status ? BadRequest() : Ok(status);
		}
	}
}
