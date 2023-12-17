using FluentValidation;
using OpenCvSharp;

namespace NoodleCV.OpenCvSharp4.Operations;

class CropValidator : AbstractValidator<IReadOnlyList<OperationInput>>

{
    public CropValidator(Mat mat)
    {
        RuleFor(list => list[0].Get<Mat>()).NotNull().WithMessage("No image input");
        RuleFor(list => list[1]).Must(startX => startX.Get<int>() > 0).WithMessage("StartX must be greater than 0");
        RuleFor(list => list[2]).Must(startY => startY.Get<int>() > 0).WithMessage("StartY must be greater than 0");
        RuleFor(list => list[3]).Must(width => width.Get<int>() > 0).WithMessage("Width must be greater than 0");
        RuleFor(list => list[4]).Must(height => height.Get<int>() > 0).WithMessage("Height must be greater than 0");

        RuleFor(list => list[1]).Must(startX => startX.Get<int>() < mat.Width).WithMessage("StartX must be less than image width");
        RuleFor(list => list[2]).Must(startY => startY.Get<int>() < mat.Height).WithMessage("StartY must be less than image height");
        RuleFor(list => list[3]).Must(width => width.Get<int>() < mat.Width).WithMessage("Width must less than image width");
        RuleFor(list => list[4]).Must(height => height.Get<int>() < mat.Height).WithMessage("Height must less than image height");

        RuleFor(list => list[1].Get<int>() + list[2].Get<int>()).Must(widthSum => widthSum < mat.Width).WithMessage("Combined StartX and Width must be less than image width");
        RuleFor(list => list[3].Get<int>() + list[4].Get<int>()).Must(heightSum => heightSum < mat.Height).WithMessage("Combined StartY and Height must be less than image height");
    }
}
