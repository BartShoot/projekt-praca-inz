using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
