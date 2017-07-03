using Android.App;
using Android.Widget;
using Android.OS;

namespace Connect3
{
    [Activity(Label = "Connect3", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private ImageView _image00;
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
        }

        private void _image00_Click(object sender, System.EventArgs e)
        {
            _image00.SetImageResource(Resource.Drawable.yellow);
            _image00.get
        }
    }
}

