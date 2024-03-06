using AutoMapper;
using GeekShopping.CardAPI.Data.ValueObjects;
using GeekShopping.CardAPI.Model;
using GeekShopping.CardAPI.Model.Context;
using GeekShopping.CardAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CardAPI.Repository
{
	public class CardRepository : ICardRepository
	{
		private readonly MySQLContext _context;
		private readonly IMapper _mapper;

		public CardRepository(MySQLContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<bool> ApplyCoupon(string userId, string couponCode)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> ClearCard(string userId)
		{
			throw new NotImplementedException();
		}

		public async Task<CardVO> FindCardByUserId(string userId)
		{
			Card card = new Card
			{
				CardHeader = await _context.CardHeaders.FirstOrDefaultAsync(
				c => c.UserId == userId
				)
			};

			card.CardDetail = _context.CardDetails
				.Where(c => c.CardHeaderId == card.CardHeader.Id)
				.Include(c => c.Product);
			;
			return _mapper.Map<CardVO>(card);
		}

		public async Task<bool> RemoveCoupon(string userId)
		{

		}

		public async Task<bool> RemoveFromCard(long cardDetailsId)
		{
			try
			{
				CardDetail cardDetail = await _context.CardDetails.FirstOrDefaultAsync(
					c => c.Id == cardDetailsId
					);

				int total = _context.CardDetails
					.Where(c => c.CardHeaderId == cardDetail.CardHeaderId).Count();

				_context.CardDetails.Remove(cardDetail);
				if (total == 1)
				{
					var carHeaderToRemove = await _context.CardHeaders.FirstOrDefaultAsync(
						c => c.Id == cardDetail.CardHeaderId);
					_context.CardHeaders.Remove(carHeaderToRemove);
				}
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{

				return false;
			}
		}

		public async Task<CardVO> SaveOrUpdate(CardVO cardVO)
		{
			Card card = _mapper.Map<Card>(cardVO);
			var product = await _context.Product.FirstOrDefaultAsync(p =>
				p.Id == cardVO.CardDetailsVO.FirstOrDefault().ProductId
			);
			if (product == null)
			{
				_context.Product.Add(card.CardDetail.FirstOrDefault().Product);
			}

			var cardHeader = await _context.CardHeaders.AsNoTracking().FirstOrDefaultAsync(ch =>
				ch.UserId == card.CardHeader.UserId
			);
			if (cardHeader == null)
			{
				_context.CardHeaders.Add(card.CardHeader);
				await _context.SaveChangesAsync();

				card.CardDetail.FirstOrDefault().CardHeaderId = card.CardHeader.Id;
				card.CardDetail.FirstOrDefault().Product = null;
				_context.CardDetails.Add(card.CardDetail.FirstOrDefault());
				await _context.SaveChangesAsync();
			}
			else
			{
				var cardDetail = await _context.CardDetails.AsNoTracking().FirstOrDefaultAsync(
					p => p.ProductId == cardVO.CardDetailsVO.FirstOrDefault().ProductId &&
					p.CardHeaderId == cardHeader.Id);
				if (cardDetail == null)
				{
					card.CardDetail.FirstOrDefault().CardHeaderId = card.CardHeader.Id;
					card.CardDetail.FirstOrDefault().Product = null;
					_context.CardDetails.Add(card.CardDetail.FirstOrDefault());
					await _context.SaveChangesAsync();
				}
				else
				{
					card.CardDetail.FirstOrDefault().Product = null;
					card.CardDetail.FirstOrDefault().Count += cardDetail.Count;
					card.CardDetail.FirstOrDefault().Id = cardDetail.Id;
					card.CardDetail.FirstOrDefault().CardHeaderId = cardDetail.CardHeaderId;
					_context.CardDetails.Update(card.CardDetail.FirstOrDefault());
					await _context.SaveChangesAsync();
				}
			}

			return _mapper.Map<CardVO>(card);
		}
	}
}
