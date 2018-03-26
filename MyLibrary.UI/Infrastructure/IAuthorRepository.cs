using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyLibrary.UI.Domain;
using MyLibrary.UI.Domain.Library;

namespace MyLibrary.UI.Infrastructure
{
    public interface IAuthorRepository
    {
        Author Find(string firstName, string lastName);

        void Save(Author author);
    }

    public class TempDictionaryAuthorRepository : IAuthorRepository
    {
        private readonly Dictionary<Guid, Author> _dictionary = new Dictionary<Guid, Author>();
        
        public Author Find(string firstName, string lastName)
        {
            return _dictionary.Values.FirstOrDefault(a =>
                a.FirstName.Equals(firstName, StringComparison.CurrentCultureIgnoreCase) &&
                a.LastName.Equals(lastName, StringComparison.CurrentCultureIgnoreCase));
        }

        public void Save(Author author)
        {
            _dictionary[author.Id] = author;
        }
    }
}