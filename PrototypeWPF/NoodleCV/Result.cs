using FluentValidation.Results;
using System.Collections.ObjectModel;

namespace NoodleCV
{
    public class Result
    {
        public bool Status;
        public Collection<string> Messages = new();

        public Result()
        {
            Status = true;
        }

        public Result(string message)
        {
            Status = false;
            Messages.Add(message);
        }

        public Result(List<ValidationFailure> errors)
        {
            Status = false;
            foreach (var error in errors)
            {
                Messages.Add(error.ToString());
            }
        }
    }
}