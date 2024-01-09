using FluentValidation;
using OpenCvSharp;

namespace NoodleCV.OpenCvSharp4.Operations;

public class EdgeDetectValidator : AbstractValidator<IReadOnlyList<OperationInput>>
{
    public EdgeDetectValidator()
    {
        RuleFor(list => list[0].Get<Mat>()).NotNull().WithMessage("No image input");
    }
}
