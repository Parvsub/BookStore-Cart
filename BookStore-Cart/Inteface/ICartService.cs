using BookStore_Cart.Entity;

namespace BookStore_Cart.Inteface
{
    public interface ICartService
    {
        Task<CartEntity> AddToCart(string token, int userId, int bookId, int quantity);
        IEnumerable<CartEntity> GetCartItems();
        bool DeleteCart(int bookid);
       /* decimal CalculatePriceForUser(int userId);*/

    }
}
