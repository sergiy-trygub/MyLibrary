using System;
using System.Threading.Tasks;
using MyLibrary.UI.Domain;
using MyLibrary.UI.Domain.Library;
using MyLibrary.UI.Infrastructure;

namespace MyLibrary.UI.Services
{
    /// <summary>
    /// Command to add book to library with validation
    /// </summary>
    public class AddBookToLibraryCommand : Command<AddBookToLibraryCommand.AddBookParameters, AddBookToLibraryCommand.AddBookResult>
    {
        private readonly IAuthorRepository _authorRepo;
        private readonly IMyBookRepository _mybookRepo;

        public AddBookToLibraryCommand(IAuthorRepository authorRepo, IMyBookRepository mybookRepo)
        {
            _authorRepo=authorRepo;
            _mybookRepo=mybookRepo;
        }

        protected override void HandleCommand(AddBookParameters input, CommandResult<AddBookResult> commandResult)
        {            
            var library = new Domain.Library.MyLibrary(input.UserId);

            // Factory method to get author from repo or create new one
            var author = Author.GetOrCreate(
                input.AuthorFirstName, 
                input.AuthorLastName,
                () => _authorRepo.Find(input.AuthorFirstName, input.AuthorLastName));

            // Factory method to get book from repo or create new one
            var book = Book.GetOrCreate(
                input.Isbn, 
                input.Title, 
                input.Description, 
                author, 
                (DateTime?)null,
                () => _mybookRepo.Find(input.Isbn));
            
            var mybook = library.AddBook(book);

            _mybookRepo.Save(mybook);
            
            commandResult.Success(new AddBookResult(input.Isbn));
        }
        
        public class AddBookParameters
        {
            public Guid UserId {get; set;}
            public string Isbn {get;set;}
            public string Title {get;set;}
            public string Description {get;set;}
            public string AuthorFirstName {get;set;}
            public string AuthorLastName {get;set;}
        }
        
        public class AddBookResult
        {
            public AddBookResult(string isbn)
            {
                ISBN = isbn;
            }

            public string ISBN { get; }
        }
    }    
}