using Android.Content;
using Android.OS;

namespace Orar;

public class SetupActivity:Activity
{
    public override void OnCreate(Bundle? savedInstanceState, PersistableBundle? persistentState)
    {
        base.OnCreate(savedInstanceState, persistentState);
        SetContentView(Resource.Layout.activity_setup);
        Thread.Sleep(1000);
        Intent intent = new Intent(this, typeof(SetupActivity));
        StartActivity(intent);
    }
}