using Android.App;
using Android.OS;
using Android.Content;
using System.Threading;

namespace Prospects.Cross.Droid
{
    [Activity(Theme = "@style/MainTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle); 
            ThreadPool.QueueUserWorkItem(o => LoadActivity());
        }
        private void LoadActivity()
        {
            RunOnUiThread(() =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            });
        }
    }
}