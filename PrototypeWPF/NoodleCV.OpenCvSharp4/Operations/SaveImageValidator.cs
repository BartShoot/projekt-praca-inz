using FluentValidation;
using OpenCvSharp;

namespace NoodleCV.OpenCvSharp4.Operations;

public class SaveImageValidator : AbstractValidator<IReadOnlyList<OperationInput>>
{
    public SaveImageValidator()
    {
        RuleFor(list => list[0].Get<Mat>()).NotNull().WithMessage("No image input");
        RuleFor(list => list[1].Get<string>()).NotNull().NotEmpty().WithMessage("Empty image path.");
    }
}
