using System;

namespace MyLibrary.UI.Domain.Library
{
    /// <summary>
    /// Book writer
    /// </summary>
    public class Author
    {
        public Author(Guid id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid Id { get; }
        
        public string FirstName { get; }
        
        public string LastName { get; }

        /// <summary>
        /// Factory method to get author from repo or create new one
        /// </summary>
        public static Author GetOrCreate(
            string firstName, 
            string lastName, 
            Func<Author> findAuthor)
        {
            if (findAuthor == null)
            {
                throw new InvalidOperationException();
            }
            
            Author author = findAuthor();

            if (author == null)
            {
                author = Create(firstName, lastName);
            }

            return author;
        }
        
        private static Author Create(string firstName, string lastName)
        {
            new Validator()
                .AddErrorIfWrongValue(() => string.IsNullOrEmpty(firstName), AppError.AuthorFirstNameNotSet)
                .AddErrorIfWrongValue(() => string.IsNullOrEmpty(lastName), AppError.AuthorLastNameNotSet)
                .ThrowIfErrors();
            
            var id = Guid.NewGuid();
            return new Author(id, firstName, lastName);
        }
    }    
}