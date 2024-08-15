using LibraryV4.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV4.Domain.Factories
{
    public static class BookFactory
    {
        public static Book CreateBook(string title, string author, int publicationYear, string isbn)
        {
            return new Book
            {
                Title = title,
                Author = author,
                PublicationYear = publicationYear,
                ISBN = isbn
            };
        }
    }
}
