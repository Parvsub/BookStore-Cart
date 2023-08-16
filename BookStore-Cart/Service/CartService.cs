using BookStore_Cart.Context;
using BookStore_Cart.Entity;
using BookStore_Cart.Inteface;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookStore_Cart.Service
{
    public class CartService : ICartService
    {
        private readonly CartContext _db;
        private readonly IBookService _book;
        private readonly IUserService _user;
        public CartService(CartContext db, IBookService book, IUserService user)
        {
            _db = db;
            _book = book;
            _user = user;
        }
        public async Task<CartEntity> AddToCart(string token, int userId, int bookId, int quantity)
        {
            var existingCartItem = await _db.CartItems
                .FirstOrDefaultAsync(c => c.BookId == bookId && c.UserId == userId);

            if (existingCartItem == null) {
                var book = await _book.GetBookbyId(bookId);
                var user = await _user.GetUser(token);

                CartEntity newCart = new CartEntity()
                {
                    BookId = bookId,
                    UserId = userId,
                    Quantity = quantity,
                    Book = book,
                    User = user
                };
                _db.CartItems.Add(newCart);
                _db.SaveChanges();
                return newCart;
            }
            else
            {
                existingCartItem.Quantity++;

            }
            _db.SaveChanges();
            return existingCartItem;
        }

       /* public decimal CalculatePriceForUser(int BookId, int userId)
        {
            var cartItems = _db.CartItems.Where(cart => cart.UserId == userId);

            decimal totalPrice = cartItems.Sum(cartItems => cartItems. * cartItems.Quantity);
            return totalPrice;
        }*/

        public bool DeleteCart(int bookid)
        {
            CartEntity cart = _db.CartItems.FirstOrDefault(x => x.BookId == bookid);

            if (cart != null)
            {
                _db.CartItems.Remove(cart);
                _db.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public IEnumerable<CartEntity> GetCartItems()
        {
            IEnumerable<CartEntity> cartEntities = _db.CartItems;
            return cartEntities;
        }
    }
}
