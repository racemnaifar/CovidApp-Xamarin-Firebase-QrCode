using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using XF_FirebaseProjectClient.Models;
using XF_FirebaseProjectClient.Renderers;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;

namespace XF_FirebaseProjectClient.Droid
{
    public class BitmapRenderer : IBarcodeRenderer<Bitmap>
    {
        private readonly IQRColor qRColor;

        public BitmapRenderer(IQRColor qRColor)
        {
            this.qRColor = qRColor;
        }

        public Bitmap Render(BitMatrix matrix, BarcodeFormat format, string content)
        {
            return Render(matrix, format, content, new EncodingOptions());
        }

        public Bitmap Render(BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options)
        {
            var width = matrix.Width;
            var height = matrix.Height;
            var pixels = new int[width * height];
            var outputIndex = 0;
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    pixels[outputIndex] = matrix[x, y] ? qRColor.Foreground(x, y, "").ToAndroid().ToArgb() : qRColor.Background(x, y).ToAndroid().ToArgb();
                    outputIndex++;
                }
            }

            var bitmap = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
            bitmap.SetPixels(pixels, 0, width, 0, 0, width, height);
            return bitmap;
        }
    }
}