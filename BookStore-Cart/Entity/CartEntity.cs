using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace BookStore_Cart.Entity
{
    public class CartEntity
    {
        [Key]
        public int CartId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [NotMapped]
        public BookEntity Book { get; set; }
        [NotMapped]
        public UserEntity User { get; set; }
        

    }
}
