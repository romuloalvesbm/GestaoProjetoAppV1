using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Presentation.Contracts
{
    public class Result
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public bool IsFailure => !Success;

        public static Result Fail(string message)
        {
            return new Result
            {
                Success = false,
                Message = message
            };
        }

        public static Result Ok(string message)
        {
            return new Result
            {
                Success = true,
                Message = message
            };
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>
            {
                Success = false,
                Message = message,
                Value = default
            };
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>
            {
                Success = true,
                Message = string.Empty,
                Value = value
            };
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; set; }

    }
}
