using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Widget;

namespace Orar;
[Activity(Name = "orar.setupactivity")]
public class SetupActivity:Activity
{
    public override void OnCreate(Bundle? savedInstanceState, PersistableBundle? persistentState)
    {
        base.OnCreate(savedInstanceState, persistentState);
        SetContentView(Resource.Layout.activity_setup);
        Button btn = FindViewById<Button>(Resource.Id.save);
        RadioGroup radioGroup = FindViewById<RadioGroup>(Resource.Id.radioGroup);
        int selectedId = radioGroup.CheckedRadioButtonId;
        string grp = FindViewById<EditText>(Resource.Id.grp).Text;
        btn.Click += (sender, e) => {
            if (selectedId != -1 || !string.IsNullOrWhiteSpace(grp))
            {
                RadioButton selectedRadioButton = FindViewById<RadioButton>(selectedId);
                string selectedText = selectedRadioButton.Text;
                ISharedPreferences sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(this);
                ISharedPreferencesEditor editor = sharedPreferences.Edit();
                editor.PutString("grp", grp);
                editor.PutString("year", selectedText);
                editor.Apply();

                Toast.MakeText(this, $"Grupa: {grp}, Anul: {selectedText}", ToastLength.Short).Show();

                Finish();

            }
            else
            {
                Toast.MakeText(this, "Nu ai introdus o grupa sau an.", ToastLength.Short).Show();
            }


        };
    }
}