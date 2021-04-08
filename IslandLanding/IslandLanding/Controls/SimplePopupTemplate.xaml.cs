using IslandLanding.ViewModel;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace IslandLanding.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SimplePopupTemplate : PopupPage
	{
		public SimplePopupTemplate ()
		{
			InitializeComponent ();
			BindingContext = new EmptyPopupViewModel();
		}
	}
}