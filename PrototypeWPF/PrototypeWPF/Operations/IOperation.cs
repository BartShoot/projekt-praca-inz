using System;
using System.Windows.Controls;
using OpenCvSharp;

namespace PrototypeWPF.Operations
{
    public interface IOperation
    {
        Mat Input { get; set; }

        Mat Output { get; set; }

        string Name { get; }

        string Description { get; }

        Func<Mat> GetFunc { get; }

        UserControl ParametersView { get; }
    }
}