using FluentValidation.Results;
using System.Collections.ObjectModel;

namespace NoodleCV
{
    public class Result
    {
        public readonly bool Status;
        public readonly Collection<string> Errors = new();

        public Result()
        {
            Status = true;
        }

        public Result(string message)
        {
            Status = false;
            Errors.Add(message);
        }

        public Result(List<ValidationFailure> errors)
        {
            Status = false;
            foreach (var error in errors)
            {
                Errors.Add(error.ToString());
            }
        }

        public static Result Ok() => new Result();

        public static Result Error(string error) => new Result(error);

        public static Result Error(List<ValidationFailure> errors) => new Result(errors);
    }
}