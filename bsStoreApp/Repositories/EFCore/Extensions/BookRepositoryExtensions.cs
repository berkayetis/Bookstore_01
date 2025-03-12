using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Extensions
{
    public static class BookRepositoryExtensions
    {
        public static IQueryable<Book> FilterBooks(this IQueryable<Book> books, uint minPrice, uint maxPrice)
        {
            var filterBookQuery = books.Where(book => book.Price >= minPrice && book.Price <= maxPrice);
            return filterBookQuery;
        }

        public static IQueryable<Book> Search(this IQueryable<Book> books, string searchTerm)
        {
            if(string.IsNullOrWhiteSpace(searchTerm))
            {
                return books;
            }

            string lowerCaseTerm = searchTerm.Trim().ToLower();
            var filterBookQuery = books.Where(book => book.Title.ToLower().Contains(lowerCaseTerm));
            return filterBookQuery;
        }
    }
}
