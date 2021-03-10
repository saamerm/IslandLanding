using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using IslandLanding.Controls;
using IslandLanding.Droid.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(NoBorderEntry), typeof(NoBorderEntryRenderer))]
namespace IslandLanding.Droid.Renderer
{
 public class NoBorderEntryRenderer: EntryRenderer
  {
    public NoBorderEntryRenderer(Context context) : base(context)
    {
    }

    protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
    {
      base.OnElementChanged(e);

      if (this.Control != null)
      {
        var roundedEntry = Element as NoBorderEntry;
        if (roundedEntry != null)
        {
          GradientDrawable gd = new GradientDrawable();
          gd.SetColor(global::Android.Graphics.Color.Transparent);
          this.Control.SetBackgroundDrawable(gd);
          this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
          Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.White));

        }

       

       
      }
    }


  }
}