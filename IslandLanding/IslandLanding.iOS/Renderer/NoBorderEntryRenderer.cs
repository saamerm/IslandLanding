using CoreGraphics;
using Foundation;
using IslandLanding.Controls;
using IslandLanding.iOS.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(NoBorderEntry), typeof(NoBorderEntryRenderer))]
namespace IslandLanding.iOS.Renderer
{
 public class NoBorderEntryRenderer: EntryRenderer
  {
    protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
    {
      base.OnElementChanged(e);

      if (Control != null)
      {
        var roundedView = Element as NoBorderEntry;


        Control.LeftView = new UIView(new CGRect(0, 0, 15, Control.Frame.Height));
        Control.RightView = new UIView(new CGRect(0, 0, 15, Control.Frame.Height));
        Control.LeftViewMode = UITextFieldViewMode.Always;
        Control.RightViewMode = UITextFieldViewMode.Always;
        Control.BorderStyle = UITextBorderStyle.None;
      }
    }

  }
}