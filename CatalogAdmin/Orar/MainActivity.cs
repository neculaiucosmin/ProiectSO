using Android.Content;
using Android.Preferences;

namespace Orar;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity : Activity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        
        // Set our view from the "main" layout resource
        SetContentView(Resource.Layout.activity_main);



    }

    // protected override void OnStart()
    // {
    //     base.OnStart();
    //     if (IsSetupNeeded())
    //     { 
    //         var intent = new Intent(this, typeof(SetupActivity));
    //         StartActivity(intent);
    //     }
    // }

    private bool IsSetupNeeded()
    {
        ISharedPreferences? sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(this);
        string grp =  sharedPreferences.GetString("grp", string.Empty);
        string year = sharedPreferences.GetString("year", string.Empty);
        if (!string.IsNullOrEmpty(grp)&&!string.IsNullOrEmpty(year))
        {
            return false;
        }

        return true;
    }
}