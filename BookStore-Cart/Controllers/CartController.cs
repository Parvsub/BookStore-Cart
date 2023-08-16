using BookStore_Cart.Entity;
using BookStore_Cart.Inteface;
using BookStore_Cart.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookStore_Cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cart;
        public ResponseEntity response;
        public CartController(ICartService cart)
        {
            _cart = cart;
            response = new ResponseEntity();
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]
        public async Task<ResponseEntity> AddtoCart(int bookId, int quatity)
        {
            string jwtTokenWithBearer = HttpContext.Request.Headers["Authorization"];
            string jwtToken = jwtTokenWithBearer.Substring("Bearer ".Length);

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));

            CartEntity newCart = await _cart.AddToCart(jwtToken, userId, bookId, quatity);

            if (newCart != null)
            {
                response.Data = newCart;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
            }
            return response;
        }
        [Authorize]
        [HttpPost]
        [Route("Delete")]
        public ResponseEntity DeleteCart(int bookid)
        {
            bool result = _cart.DeleteCart(bookid);
            if (result)
            {
                response.Data = result;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Book Not Found";
            }
            return response;
        }
        [Authorize]
        [HttpGet]
        [Route("GetOrders")]

        public ResponseEntity GetCartItems()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));
            IEnumerable<CartEntity> orders = _cart.GetCartItems();

            if (orders.Any())
            {
                response.Data = orders;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "something gone wrong, Please Validate again";
            }
            return response;
        }

       /* [HttpGet("calculateTotalPriceForUser")]
        public IActionResult CalculateTotalPriceForUser(int userId )
        {
            decimal totalPrice = _cart.CalculatePriceForUser(userId);

            return Ok(new
            {
                totalPrice,
                isSuccess = true,
                message = "Total price calculated successfully."
            });
        }*/
    }

}
    
