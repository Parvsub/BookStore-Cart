using BookStore_Cart.Entity;
using BookStore_Cart.Inteface;
using Newtonsoft.Json;

namespace BookStore_Cart.Service
{
    public class BookService : IBookService
    {
        public async Task<BookEntity> GetBookbyId(int id)
        {
            //BookEntity book = null;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://localhost:7045/api/Book/GetById?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    // apiContent -> string -> convert it as ResponseEntity -> as string -> converted to BookEntity 
                    string apiContent = await response.Content.ReadAsStringAsync();
                    ResponseEntity responseEntity = JsonConvert.DeserializeObject<ResponseEntity>(apiContent);
                    string bookContent = responseEntity.Data.ToString();
                    BookEntity book = JsonConvert.DeserializeObject<BookEntity>(bookContent);
                    return book;
                }
            }
            return null;
        }
    }
}
