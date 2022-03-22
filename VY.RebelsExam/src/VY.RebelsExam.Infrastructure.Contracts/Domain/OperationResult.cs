using System;
using System.Collections.Generic;
using System.Linq;

namespace VY.RebelsExam.Infrastructure.Contracts.Domain
{
    public class OperationResult
    {
        private readonly List<ErrorObject> _errors = new List<ErrorObject>();
        public IEnumerable<ErrorObject> Errors { get; }
        public void AddError(ErrorObject error)
        {
            _errors.Add(error);
        }
        public void AddErrors(IEnumerable<ErrorObject> errors)
        {
            _errors.AddRange(errors);
        }
        public void AddError(int code, string message)
        {
            _errors.Add(new ErrorObject() { Code = code, Message = message});
        }
        public void AddException(Exception ex)
        {
            _errors.Add(new ErrorObject() { Code= 9999, Message= ex.Message, Exception = ex});
        }
        public bool HasErrors()
        {
            return _errors.Count > 0;
        }

        public bool HasExceptions()
        {
            return _errors.Any(x => x.Exception != null);
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Result { get; set; }
        public void SetResult(T result) { this.Result = result; }

    }
}
