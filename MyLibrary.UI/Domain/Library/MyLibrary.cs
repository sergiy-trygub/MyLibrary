using System;

namespace MyLibrary.UI.Domain.Library
{
    /// <summary>
    /// User's library
    /// </summary>
    public class MyLibrary
    {
        public MyLibrary(Guid userId)
        {
            UserId = userId;
        }
        
        public Guid UserId { get; }
        
        public MyBook AddBook(Book book)
        {           
            return new MyBook(UserId, book);
        }
    }
}