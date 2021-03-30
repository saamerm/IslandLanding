using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
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
[assembly: ExportRenderer(typeof(CustomEditorControl), typeof(CustomEditorRenderer))]
namespace IslandLanding.Droid.Renderer
{
  public class CustomEditorRenderer : EditorRenderer
  {
    public CustomEditorRenderer(Context context) : base(context)
    {
    }

    protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
    {
      base.OnElementChanged(e);
      if (Control != null)
      {
        GradientDrawable gd = new GradientDrawable();
        gd.SetColor(global::Android.Graphics.Color.Transparent);
        Control.Background = gd;
      }
    }
  }
}