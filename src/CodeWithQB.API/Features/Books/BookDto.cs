using CodeWithQB.Core.Models;
using System;

namespace CodeWithQB.API.Features.Books
{
    public class BookDto
    {        
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }
        public string Publisher { get; set; }
    }

    public static class BookExtensions
    {        
        public static BookDto ToDto(this Book book)
            => new BookDto
            {
                BookId = book.BookId,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
                Publisher = book.Publisher
            };
    }
}
