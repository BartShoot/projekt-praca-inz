using FluentValidation;

namespace NoodleCV.OpenCvSharp4.Operations;

public class LoadImageValidator : AbstractValidator<IReadOnlyList<OperationInput>>
{
    public LoadImageValidator()
    {
        RuleFor(list => list[0].Get<string>()).NotNull().WithMessage("No image path");
        RuleFor(list => list[0].Get<string>()).Must(path => File.Exists(path)).WithMessage("File doesn't exsist");
    }
}
