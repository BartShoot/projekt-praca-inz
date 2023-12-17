using FluentValidation;
using OpenCvSharp;

namespace NoodleCV.OpenCvSharp4.Operations;

public class BlurValidator : AbstractValidator<IReadOnlyList<OperationInput>>
{
    public BlurValidator()
    {
        RuleFor(list => list[0].Get<Mat>()).NotNull().WithMessage("No image input");
        RuleFor(list => list[1]).Must(size => size.Get<int>() > 0).WithMessage("Size must be greater than 0");
        RuleFor(list => list[1]).Must(size => size.Get<int>() % 2 != 0).WithMessage("Size must be odd");
        RuleFor(list => list[2]).Must(strength => strength.Get<double>() > 0).WithMessage("Strength must be greater than 0");
    }
}
