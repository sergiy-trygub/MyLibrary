using System;

namespace MyLibrary.UI.Domain.Library
{
    /// <summary>
    /// Book in the library
    /// </summary>
    public class MyBook : IAggregateRoot
    {
        public MyBook(Guid userId, Book book)
        {
            UserId = userId;
            Book = book;            
            Created = DateTime.UtcNow;
        }

        public Guid UserId { get; }
        
        public Book Book { get; }
        
        public DateTime Created { get; }
                
        /// <summary>
        /// Start reading
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public MyReadingBook StartReading(DateTime startDate)
        {
            return new MyReadingBook(UserId, Book, startDate);
        }
    }
}