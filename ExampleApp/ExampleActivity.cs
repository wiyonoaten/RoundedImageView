using Android.OS;
using Android.Views;
using Android.Support.V7.App;
using Android.App;
using Android.Widget;

namespace ExampleApp
{
    [Activity(Label = "ExampleActivity", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class ExampleActivity : AppCompatActivity, AdapterView.IOnItemSelectedListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.example_activity);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            Spinner navSpinner = FindViewById<Spinner>(Resource.Id.spinner_nav);

            navSpinner.Adapter = ArrayAdapter.CreateFromResource(
                navSpinner.Context,
                Resource.Array.action_list,
                Android.Resource.Layout.SimpleSpinnerDropDownItem);

            navSpinner.OnItemSelectedListener = this;

            if (savedInstanceState == null)
            {
                navSpinner.SetSelection(0);
            }
        }
        
        public void OnItemSelected(AdapterView parent, View view, int position, long id)
        {
            Android.Support.V4.App.Fragment newFragment;
            switch (position)
            {
                default:
                case 0:
                    // bitmap
                    newFragment = RoundedFragment.GetInstance(RoundedFragment.ExampleType.DEFAULT);
                    break;
                case 1:
                    // oval
                    newFragment = RoundedFragment.GetInstance(RoundedFragment.ExampleType.OVAL);
                    break;
                case 2:
                    // select
                    newFragment = RoundedFragment.GetInstance(RoundedFragment.ExampleType.SELECT_CORNERS);
                    break;
                //case 3:
                //    // picasso
                //    newFragment = new PicassoFragment();
                //    break;
                case 4:
                    // color
                    newFragment = new ColorFragment();
                    break;
            }

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.fragment_container, newFragment)
                .Commit();
        }

        public void OnNothingSelected(AdapterView parent)
        {
        }
    }
}