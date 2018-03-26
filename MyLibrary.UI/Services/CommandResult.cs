using System.Collections.Generic;
using MyLibrary.UI.Domain;

namespace MyLibrary.UI.Services
{
    /// <summary>
    /// Command result
    /// Succeeded - true if command executed sucessfully, otherwise false
    /// Data - command data result
    /// </summary>
    public sealed class CommandResult<TResult>
    {
        public TResult Data { get; private set; }

        public bool Succeeded { get; private set; }

        private readonly List<AppError> _errors = new List<AppError>();
        
        public void Success(TResult data)
        {
            if (_errors.Count == 0)
            {
                Data = data;
                
                Succeeded = true;                
            }
        }

        public void AddError(string errorCode, string errorMessage)
        {
            AddError(new AppError(errorCode, errorMessage));
        }        
        
        public void AddError(params AppError[] errors)
        {
            Succeeded = false;
            _errors.AddRange(errors);
        }

        public IEnumerable<AppError> GetErrors()
        {
            return _errors;
        }
    }    
}