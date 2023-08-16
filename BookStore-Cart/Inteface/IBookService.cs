using BookStore_Cart.Entity;

namespace BookStore_Cart.Inteface
{
    public interface IBookService
    {
        Task<BookEntity> GetBookbyId(int id);
    }
}
