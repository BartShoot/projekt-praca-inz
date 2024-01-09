using FluentValidation;
using OpenCvSharp;

namespace NoodleCV.OpenCvSharp4.Operations;

public class ResizeValidator : AbstractValidator<IReadOnlyList<OperationInput>>
{
    public ResizeValidator(Mat mat)
    {
        RuleFor(list => list[0].Get<Mat>()).NotNull().WithMessage("No image input");
        if (mat != null)
        {
            RuleFor(list => list[1]).Must(width => width.Get<int>() < mat.Width).WithMessage("New width must be less than image width");
            RuleFor(list => list[2]).Must(height => height.Get<int>() < mat.Height).WithMessage("New height must be less than image height");
        }
    }
}
