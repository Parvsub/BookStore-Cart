using BookStore_Cart.Entity;

namespace BookStore_Cart.Inteface
{
    public interface IUserService
    {
        Task<UserEntity> GetUser(string jwtToken);
    }
}
