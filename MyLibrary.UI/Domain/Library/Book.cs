using System;

namespace MyLibrary.UI.Domain.Library
{
    /// <summary>
    /// Any book
    /// </summary>
    public class Book : Entity
    {
        public Book(string isbn, string title, string description, Author author, DateTime? publishDate = null)
        {
            new Validator()
                .AddErrorIfWrongValue(() => string.IsNullOrEmpty(isbn), AppError.BookIsbnNotSet)
                .AddErrorIfWrongValue(() => string.IsNullOrEmpty(title), AppError.BookTitleNotSet)
                .AddErrorIfWrongValue(() => author == null, AppError.BookAuthorNotSet)
                .ThrowIfErrors();

            Title = title;
            Description = description;
            Author = author;
            Isbn = isbn;
            PublishDate = publishDate;
        }

        public string Isbn { get; }
        
        public string Title { get; }
        
        public string Description { get; }
        
        public Author Author { get; }

        public DateTime? PublishDate {get;}

        
        /// <summary>
        /// Factory method to get book from repo or create new one
        /// </summary>
        public static Book GetOrCreate(string isbn, string title, string description, Author author,
            DateTime? publishDate = null, Func<Book> findBook = null)
        {
            var book = findBook?.Invoke();

            if (book == null)
            {
                new Validator()
                    .AddErrorIfWrongValue(() => author == null, AppError.BookAuthorNotSet)
                    .ThrowIfErrors();
                
                book = new Book(isbn, title, description, author, publishDate );
            }

            return book;
        }
    }    
}