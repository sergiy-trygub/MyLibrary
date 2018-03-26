using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyLibrary.UI.Domain;
using MyLibrary.UI.Domain.Library;

namespace MyLibrary.UI.Infrastructure
{
    public interface IMyBookRepository
    {
        Book Find(string isbn);        

        void Save(MyBook myBook);
    }

    public class TempDictionaryMyBookRepository : IMyBookRepository
    {
        private readonly Dictionary<string, MyBook> _dictionary = new Dictionary<string, MyBook>();

        public Book Find(string isbn)
        {
            return _dictionary.ContainsKey(isbn) ? _dictionary[isbn]?.Book : null;
        }

        public void Save(MyBook myBook)
        {
            _dictionary[myBook.Book.Isbn] = myBook;
        }
    }
}