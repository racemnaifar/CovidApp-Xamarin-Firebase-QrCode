
using System.ComponentModel;
using Android.Content;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF_FirebaseProjectClient.Droid;
using XF_FirebaseProjectClient.Renderers;

[assembly: ExportRenderer(typeof(ExtendedZXingBarcodeImageView), typeof(ExtendedZXingBarcodeImageViewRenderer))]
namespace XF_FirebaseProjectClient.Droid
{
    public class ExtendedZXingBarcodeImageViewRenderer : ViewRenderer<ExtendedZXingBarcodeImageView, ImageView>
    {
        ExtendedZXingBarcodeImageView formsView;
        ImageView imageView;

        public ExtendedZXingBarcodeImageViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Regenerate();

            base.OnElementPropertyChanged(sender, e);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ExtendedZXingBarcodeImageView> e)
        {
            formsView = Element;

            if (imageView == null)
            {
                imageView = new ImageView(Context);
                base.SetNativeControl(imageView);
            }

            Regenerate();

            base.OnElementChanged(e);
        }

        void Regenerate()
        {
            if (!string.IsNullOrEmpty(formsView?.BarcodeValue))
            {
                var writer = new ZXing.Mobile.BarcodeWriter
                {
                    Renderer = new BitmapRenderer(formsView.QRColor)
                };

                if (formsView != null && formsView.BarcodeOptions != null)
                    writer.Options = formsView.BarcodeOptions;

                writer.Format = formsView.BarcodeFormat;

                var code = formsView.BarcodeValue;

                Device.BeginInvokeOnMainThread(() =>
                {
                    var image = writer.Write(code);
                    imageView.SetImageBitmap(image);
                });
            }
        }
    }
}