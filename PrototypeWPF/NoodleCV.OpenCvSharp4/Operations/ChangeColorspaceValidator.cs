using FluentValidation;
using OpenCvSharp;

namespace NoodleCV.OpenCvSharp4.Operations;

public class ChangeColorspaceValidator : AbstractValidator<IReadOnlyList<OperationInput>>
{
    public ChangeColorspaceValidator()
    {
        RuleFor(list => list[0].Get<Mat>()).NotNull().WithMessage("No image input");
    }
}
