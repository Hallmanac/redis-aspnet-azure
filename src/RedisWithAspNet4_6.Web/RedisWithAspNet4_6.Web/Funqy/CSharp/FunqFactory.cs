namespace Funqy.CSharp
{
    /// <summary>
    /// A factory class to help generate new FunqResult objects
    /// </summary>
    public class FunqFactory
    {
        /// <summary>
        /// Creates a new <see cref="FunqResult"/> with the IsSuccessful property set to true.
        /// </summary>
        /// <param name="message">Optional message that is saved to the Message property on the <see cref="FunqResult"/></param>
        public static FunqResult KeepGroovin(string message = null)
        {
            return new FunqResult(true, message);
        }


        /// <summary>
        /// Creates a new <see cref="FunqResult{T}"/> with the IsSuccessful property set to true.
        /// </summary>
        /// <param name="result">The underlying value of the new <see cref="FunqResult{T}"/></param>
        /// <param name="message">Optional message that is saved to the Message property on the <see cref="FunqResult{T}"/></param>
        public static FunqResult<T> KeepGroovin<T>(T result, string message = null)
        {
            return new FunqResult<T>(result, true, message);
        }

        /// <summary>
        /// Creates a new <see cref="FunqResult{T}"/> with the IsSuccessful property set to true.
        /// </summary>
        /// <typeparam name="T">The type of the underlying Result property</typeparam>
        /// <param name="funqResultResult">An existing FunqResult to pass along</param>
        /// <param name="message">Optional message that is saved to the Message property on the <see cref="FunqResult{T}"/></param>
        public static FunqResult<T> KeepGroovin<T>(FunqResult<T> funqResultResult, string message = null)
        {
            return new FunqResult<T>(funqResultResult.Result, true, message);
        }

        /// <summary>
        /// Creates a new <see cref="FunqResult"/> with the IsSuccessful property set to false.
        /// </summary>
        /// <param name="message">Optional message that is saved to the Message property on the <see cref="FunqResult"/></param>
        public static FunqResult Fail(string message)
        {
            return new FunqResult(false, message);
        }

        /// <summary>
        /// Creates a new <see cref="FunqResult{T}"/> with the IsSuccessful property set to false.
        /// </summary>
        /// <typeparam name="T">The type of the underlying Result value</typeparam>
        /// <param name="message">Optional message that is saved to the Message property on the <see cref="FunqResult{T}"/></param>
        /// <param name="result">The underlying value of the new <see cref="FunqResult{T}"/></param>
        public static FunqResult<T> Fail<T>(string message, T result = default(T))
        {
            return new FunqResult<T>(result, false, message);
        }

        /// <summary>
        /// Creates a new <see cref="FunqResult{T}"/> with the IsSuccessful property set to false.
        /// </summary>
        /// <typeparam name="T">The type of the underlying Result value</typeparam>
        /// <param name="message">Optional message that is saved to the Message property on the <see cref="FunqResult{T}"/></param>
        /// <param name="funqResultResult">An existing FunqResult to pass along</param>
        /// <returns></returns>
        public static FunqResult<T> Fail<T>(string message, FunqResult<T> funqResultResult = null)
        {
            return funqResultResult == null ? new FunqResult<T>(default(T), false, message) : new FunqResult<T>(funqResultResult.Result, false, message);
        }
    }
}