using GeekShopping.CardAPI.Data.ValueObjects;

namespace GeekShopping.CardAPI.Repository.Interface
{
	public interface ICardRepository
	{
		Task<CardVO> FindCardByUserId(string userId);

		Task<CardVO> SaveOrUpdate(CardVO card);

		Task<bool> RemoveFromCard(long cardDetailsId);

		Task<bool> ApplyCoupon(string userId, string couponCode);

		Task<bool> RemoveCoupon(string userId);

		Task<bool> ClearCard(string userId);
	}
}
