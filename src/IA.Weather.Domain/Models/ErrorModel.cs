using System;

namespace IA.Weather.Domain.Models
{
    public class ErrorModel
    {
        public string ErrorMessage { get; }
        public Exception Exception { get; }

        /// <summary>
        /// Creates a new ErrorModel from an exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="errorMessage">Exception error message will be used if not specified</param>
        /// <returns></returns>
        public static ErrorModel FromException(Exception ex, string errorMessage = null)
        {
            errorMessage = errorMessage ?? ex.Message;
            return new ErrorModel(errorMessage, ex);
        }

        private ErrorModel(string errorMessage, Exception exception)
        {
            ErrorMessage = errorMessage;
            Exception = exception;
        }
    }
}
