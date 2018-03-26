using System;

namespace MyLibrary.UI.Domain.Library
{
    /// <summary>
    /// The book from library user started to read
    /// </summary>
    public class MyReadingBook : MyBook
    {
        public MyReadingBook(Guid userId, Book book, DateTime startDate) : base(userId, book)
        {
            StartDate = startDate;
            PercentDone = 0;
        }

        public DateTime StartDate { get; }
        
        public byte PercentDone { get; private set; }
        
        /// <summary>
        /// Manage the reading progress
        /// </summary>
        public void SetReadingProgress(byte percentDone, DateTime progressDate)
        {
            new Validator()
                .AddErrorIfWrongValue(() => percentDone > 100, AppError.WrongReadingProgressValue)
                .ThrowIfErrors();

            PercentDone = percentDone;
        }

        public void FinishReading(DateTime finishedOn)
        {
            
        }
    }    
}