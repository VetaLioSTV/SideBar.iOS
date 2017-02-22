using System;

using UIKit;
using Xamarin.SideMenu;

namespace SideBar.iOS
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		UIGestureRecognizer _navBarGesture;

		SideMenuManager _sideMenuManager;

		UISegmentedControl _presentationMode, _menuBlurStyle;

		UISlider _menuFadeStrength, _menuShadowOpacity, _menuScreenWidth, _menuTransformScaleFactor;

		UISwitch _menuFadeStatusBar;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.NavigationItem.SetLeftBarButtonItem(
				new UIBarButtonItem("Left Menu", UIBarButtonItemStyle.Plain, (sender, e) =>
				{
					PresentViewController(_sideMenuManager.LeftNavigationController, true, null);
				}),
				false);



			_sideMenuManager = new SideMenuManager();

			View.BackgroundColor = UIColor.White;

			SetupSideMenu();

		}

		void SetupSideMenu()
		{
			_sideMenuManager.LeftNavigationController = new UISideMenuNavigationController(_sideMenuManager, new SampleTableView(), leftSide: true);

			// Enable gestures. The left and/or right menus must be set up above for these to work.
			// Note that these continue to work on the Navigation Controller independent of the View Controller it displays!
			//_sideMenuManager.AddPanGestureToPresent(toView: this.NavigationController?.NavigationBar);
			_sideMenuManager.AddScreenEdgePanGesturesToPresent(toView: this.NavigationController?.View);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_navBarGesture != null)
					this.NavigationController?.NavigationBar?.RemoveGestureRecognizer(_navBarGesture);

				//if (_navControllerGesture != null)
				//    this.NavigationController?.View.RemoveGestureRecognizer(_navControllerGesture);
			}

			base.Dispose(disposing);
		}
	}
}