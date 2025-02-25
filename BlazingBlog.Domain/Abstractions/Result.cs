namespace BlazingBlog.Domain.Abstractions
{
    public class Result
    {
        public bool Success { get; }
        public bool Failure => !Success;
        public string? Error { get; }

        protected Result(bool success, string? errorMessage = null)
        {
            Success = success;
            Error = errorMessage;
        }

        public static Result Ok() => new Result(true);

        public static Result Fail(string errorMessage) => new Result(false, errorMessage);

        public static Result<T> Ok<T>(T value) => new Result<T>(value, true, string.Empty);

        public static Result<T> Fail<T>(string errorMessage) => new Result<T>(default, false, errorMessage);

        public static Result<T> FromValue<T>(T? value) => value != null ? Ok(value) : Fail<T>("Provided value is null");
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        protected internal Result(T? value, bool success, string errorMessage)
            : base(success, errorMessage)
        {
            {
                Value = value;
            }
        }

        public static implicit operator Result<T>(T value) => FromValue(value);

        /// <summary>
        /// Handy method for implicit converstion in the opposite direction (particularly useful for UI)
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator T?(Result<T> result) => result.Value;
    }
}
