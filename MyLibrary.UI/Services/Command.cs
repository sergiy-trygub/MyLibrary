using System;
using System.Linq;
using System.Threading.Tasks;
using MyLibrary.UI.Domain;

namespace MyLibrary.UI.Services
{
    /// <summary>
    /// Base class for command (Template method pattern)
    /// </summary>
    public abstract class Command<TInput, TOutput> where TInput: class 
    {        
        public CommandResult<TOutput> Handle(TInput input)
        {
            CommandResult<TOutput> result = new CommandResult<TOutput>();

            try
            {
                HandleCommand(input, result);
            }
            catch (DomainException domainException)
            {
                result.AddError(domainException.Errors.ToArray());
            }
            catch (Exception e)
            {
                result.AddError(new AppError("general_error", e.Message));
            }

            return result;
        }
        
        /// <summary>
        /// Implement this method in derived class
        /// </summary>
        protected abstract void HandleCommand(TInput input, CommandResult<TOutput> commandResult);
    }
}