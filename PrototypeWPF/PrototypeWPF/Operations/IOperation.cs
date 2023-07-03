using OpenCvSharp;
using System;

namespace PrototypeWPF.Operations
{
    public interface IOperation
    {
        Mat Image { get; set; }
        string Name { get; }
        string Description { get; }
        Func<Mat> GetFunc { get; }
    }
}
