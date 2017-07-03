using Android.App;
using Android.Widget;
using Android.OS;

namespace Connect3
{
    [Activity(Label = "Connect3", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private ImageView _image00;
        private ImageView _image01;
        private ImageView _image02;
        private ImageView _image10;
        private ImageView _image11;
        private ImageView _image12;
        private ImageView _image20;
        private ImageView _image21;
        private ImageView _image22;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            initFields();
        }

        private void initFields()
        {
            _image00 = FindViewById<ImageView>(Resource.Id.imageView00);
            _image00.Click += _image00_Click;
            _image01 = FindViewById<ImageView>(Resource.Id.imageView01);
            _image01.Click += _image00_Click;
            _image02 = FindViewById<ImageView>(Resource.Id.imageView02);
            _image02.Click += _image00_Click;
            _image10 = FindViewById<ImageView>(Resource.Id.imageView10);
            _image10.Click += _image00_Click;
            _image11 = FindViewById<ImageView>(Resource.Id.imageView11);
            _image11.Click += _image00_Click;
            _image12 = FindViewById<ImageView>(Resource.Id.imageView12);
            _image12.Click += _image00_Click;
            _image20 = FindViewById<ImageView>(Resource.Id.imageView20);
            _image20.Click += _image00_Click;
            _image21 = FindViewById<ImageView>(Resource.Id.imageView21);
            _image21.Click += _image00_Click;
            _image22 = FindViewById<ImageView>(Resource.Id.imageView22);
            _image22.Click += _image00_Click;
        }

        private void _image00_Click(object sender, System.EventArgs e)
        {
            ImageView imageView = (ImageView) sender;
            imageView.TranslationY = -1000f;
            imageView.SetImageResource(Resource.Drawable.yellow);
            imageView.Animate().TranslationYBy(1000f).Rotation(360).SetDuration(500);

        }
    }
}

