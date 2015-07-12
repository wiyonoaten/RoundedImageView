using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

namespace ExampleApp
{
    public class ColorFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_rounded, container, false);

            StreamAdapter adapter = new StreamAdapter(Activity);
            view.FindViewById<ListView>(Resource.Id.main_list).Adapter = adapter;

            adapter.Add(
                new ColorItem(Android.Resource.Color.DarkerGray, "Tufa at night", "Mono Lake, CA",
                    ImageView.ScaleType.Center));
            adapter.Add(
                new ColorItem(Android.Resource.Color.HoloOrangeDark, "Starry night", "Lake Powell, AZ",
                    ImageView.ScaleType.CenterCrop));
            adapter.Add(
                new ColorItem(Android.Resource.Color.HoloBlueDark, "Racetrack playa", "Death Valley, CA",
                    ImageView.ScaleType.CenterInside));
            adapter.Add(
                new ColorItem(Android.Resource.Color.HoloGreenDark, "Napali coast", "Kauai, HI",
                    ImageView.ScaleType.FitCenter));
            adapter.Add(
                new ColorItem(Android.Resource.Color.HoloRedDark, "Delicate Arch", "Arches, UT",
                    ImageView.ScaleType.FitEnd));
            adapter.Add(
                new ColorItem(Android.Resource.Color.HoloPurple, "Sierra sunset", "Lone Pine, CA",
                    ImageView.ScaleType.FitStart));
            adapter.Add(
                new ColorItem(Android.Resource.Color.White, "Majestic", "Grand Teton, WY",
                    ImageView.ScaleType.FitXy));

            return view;
        }

        private class ColorItem
        {
            internal int mResId;
            internal string mLine1;
            internal string mLine2;
            internal ImageView.ScaleType mScaleType;

            public ColorItem(int resid, string line1, string line2, ImageView.ScaleType scaleType)
            {
                mResId = resid;
                mLine1 = line1;
                mLine2 = line2;
                mScaleType = scaleType;
            }
        }

        private class StreamAdapter : ArrayAdapter<ColorItem> 
        {
            private LayoutInflater mInflater;

            public StreamAdapter(Context context)
                : base(context, 0)
            {
                mInflater = LayoutInflater.From(Context);
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                ViewGroup view;
                if (convertView == null)
                {
                    view = (ViewGroup)mInflater.Inflate(Resource.Layout.rounded_item, parent, false);
                }
                else
                {
                    view = (ViewGroup)convertView;
                }

                ColorItem item = GetItem(position);

                view.FindViewById<ImageView>(Resource.Id.imageView1).SetImageResource(item.mResId);
                view.FindViewById<ImageView>(Resource.Id.imageView1).SetScaleType(item.mScaleType);
                view.FindViewById<TextView>(Resource.Id.textView1).Text = item.mLine1;
                view.FindViewById< TextView>(Resource.Id.textView2).Text = item.mLine2;
                view.FindViewById< TextView>(Resource.Id.textView3).Text = item.mScaleType.ToString();
                return view;
            }
        }
    }
}