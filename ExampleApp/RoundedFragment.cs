using System;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.Graphics;
using MakeramenRoundedImageView;

namespace ExampleApp
{
    public class RoundedFragment : Fragment
    {
        const string ARG_EXAMPLE_TYPE = "example_type";

        private ExampleType exampleType;

        public static RoundedFragment GetInstance(ExampleType exampleType)
        {
            RoundedFragment f = new RoundedFragment();
            Bundle args = new Bundle();
            args.PutString(ARG_EXAMPLE_TYPE, Enum.GetName(typeof(ExampleType), exampleType));
            f.Arguments = args;
            return f;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Arguments != null)
            {
                exampleType = (ExampleType) Enum.Parse(typeof(ExampleType), Arguments.GetString(ARG_EXAMPLE_TYPE));
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_rounded, container, false);

            StreamAdapter adapter = new StreamAdapter(this, Activity);

            adapter.Add(new StreamItem(Activity,
                Resource.Drawable.photo1, "Tufa at night", "Mono Lake, CA", ImageView.ScaleType.Center));
            adapter.Add(new StreamItem(Activity,
                Resource.Drawable.photo2, "Starry night", "Lake Powell, AZ", ImageView.ScaleType.CenterCrop));
            adapter.Add(new StreamItem(Activity,
                Resource.Drawable.photo3, "Racetrack playa", "Death Valley, CA", ImageView.ScaleType.CenterInside));
            adapter.Add(new StreamItem(Activity,
                Resource.Drawable.photo4, "Napali coast", "Kauai, HI", ImageView.ScaleType.FitCenter));
            adapter.Add(new StreamItem(Activity,
                Resource.Drawable.photo5, "Delicate Arch", "Arches, UT", ImageView.ScaleType.FitEnd));
            adapter.Add(new StreamItem(Activity,
                Resource.Drawable.photo6, "Sierra sunset", "Lone Pine, CA", ImageView.ScaleType.FitStart));
            adapter.Add(new StreamItem(Activity,
                Resource.Drawable.photo7, "Majestic", "Grand Teton, WY", ImageView.ScaleType.FitXy));
            adapter.Add(new StreamItem(Activity,
                Resource.Drawable.black_white_tile, "TileMode", "REPEAT", ImageView.ScaleType.FitXy,
                Shader.TileMode.Repeat));
            adapter.Add(new StreamItem(Activity,
                Resource.Drawable.black_white_tile, "TileMode", "CLAMP", ImageView.ScaleType.FitXy,
                Shader.TileMode.Clamp));
            adapter.Add(new StreamItem(Activity,
                Resource.Drawable.black_white_tile, "TileMode", "MIRROR", ImageView.ScaleType.FitXy,
                Shader.TileMode.Mirror));

            view.FindViewById<ListView>(Resource.Id.main_list).Adapter = adapter;
            return view;
        }

        private class StreamItem
        {
            internal Bitmap mBitmap;
            internal string mLine1;
            internal string mLine2;
            internal ImageView.ScaleType mScaleType;
            internal Shader.TileMode mTileMode;

            public StreamItem(Context c, int resid, string line1, string line2, ImageView.ScaleType scaleType)
                : this(c, resid, line1, line2, scaleType, Shader.TileMode.Clamp)
            {
                
            }

            public StreamItem(Context c, int resid, string line1, string line2, ImageView.ScaleType scaleType,
                Shader.TileMode tileMode)
            {
                mBitmap = BitmapFactory.DecodeResource(c.Resources, resid);
                mLine1 = line1;
                mLine2 = line2;
                mScaleType = scaleType;
                mTileMode = tileMode;
            }
        }

        private class StreamAdapter : ArrayAdapter<StreamItem> 
        {
            private readonly RoundedFragment mFragment;
            private readonly LayoutInflater mInflater;

            public StreamAdapter(RoundedFragment fragment, Context context)
                : base(context, 0)
            {
                mFragment = fragment;
                mInflater = LayoutInflater.From(Context);
            }

            public override View GetView(int position, View convertView, ViewGroup parent) 
            {
                ViewGroup view;
                if (convertView == null)
                {
                    if (mFragment.exampleType == ExampleType.SELECT_CORNERS)
                    {
                        view = (ViewGroup)mInflater.Inflate(Resource.Layout.rounded_item_select, parent, false);
                    }
                    else
                    {
                        view = (ViewGroup)mInflater.Inflate(Resource.Layout.rounded_item, parent, false);
                    }
                }
                else
                {
                    view = (ViewGroup)convertView;
                }

                StreamItem item = GetItem(position);

                RoundedImageView iv = view.FindViewById<RoundedImageView>(Resource.Id.imageView1);
                iv.Oval = (mFragment.exampleType == ExampleType.OVAL);
                iv.SetImageBitmap(item.mBitmap);
                iv.SetScaleType(item.mScaleType);
                iv.TileModeX = item.mTileMode;
                iv.TileModeY = item.mTileMode;

                view.FindViewById<TextView>(Resource.Id.textView1).Text = item.mLine1;
                view.FindViewById<TextView>(Resource.Id.textView2).Text = item.mLine2;
                view.FindViewById<TextView>(Resource.Id.textView3).Text = item.mScaleType.ToString();

                return view;
            }
        }

        public enum ExampleType
        {
            DEFAULT,
            OVAL,
            SELECT_CORNERS
        }
    }
}