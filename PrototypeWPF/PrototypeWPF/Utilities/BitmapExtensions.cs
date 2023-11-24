using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PrototypeWPF.Utilities
{
    internal static class BitmapExtensions
    {
        // TODO: pick correct ToBitmap function by channel/variable in Mat wrapper
        public static BitmapSource ToBitmapSourceBGR(Bitmap bitmap)
        {
            var bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, bitmap.PixelFormat);

            var bitmapSource = BitmapSource.Create(
                bitmapData.Width, bitmapData.Height,
                bitmap.HorizontalResolution, bitmap.VerticalResolution,
                GetPixelFormat(bitmap.PixelFormat), null,
                bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

            bitmap.UnlockBits(bitmapData);

            return bitmapSource;
        }

        private static System.Windows.Media.PixelFormat GetPixelFormat(System.Drawing.Imaging.PixelFormat pixelFormat)
            => pixelFormat switch
            {
                System.Drawing.Imaging.PixelFormat.Format24bppRgb => PixelFormats.Rgb24,
                System.Drawing.Imaging.PixelFormat.Format8bppIndexed => PixelFormats.Gray8,
                _ => throw new NotSupportedException(),
            };

        public static BitmapSource ToBitmapSourceGrayscale(Bitmap bitmap)
        {
            var bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, bitmap.PixelFormat);

            var bitmapSource = BitmapSource.Create(
                bitmapData.Width, bitmapData.Height,
                bitmap.HorizontalResolution, bitmap.VerticalResolution,
                PixelFormats.Gray8, null,
                bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

            bitmap.UnlockBits(bitmapData);

            return bitmapSource;
        }
    }
}