using System;

namespace CodeWithQB.Core.Models
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string ImageUrl { get; set; }
        public float Price { get; set; }
    }
}
