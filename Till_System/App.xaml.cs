using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Till_System
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>

        public List<Item> itemList;
        public List<Order> ordersList;
        public List<Item> allOrderedItems;
        public List<string> images;
        public string picturePicked = "";
        public List<Item> removedItems;

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            ordersList = new List<Order>();
            allOrderedItems = new List<Item>();
            removedItems = new List<Item>();

            //add items to itemList
            itemList = new List<Item>()
        {
            new Food("Berry Cheesecake", 5.00, "Cold", "/Images/berry_cheesecake.jpg", new List<string>{"berries", "whipped cream"}),
            new Food("Macaron", 2.00,"Cold", "/Images/macarons.jpg", new List<string>{"cream"}),
            new Food("Brownie", 2.00, "Cold", "/Images/brownies.jpg", new List<string>{"cream"}),
            new Food("Cookie", 2.00, "Cold", "/Images/cookie.jpg", new List<string> { "cream" }),
            new Food("Cream Puff", 0.50, "Cold", "/Images/cream_puff.jpg", new List<string> { "cream" , "custard"}),
            new Drink("Hot Chocolate", 3.00, "Hot", "/Images/hot_chocolate.jpg", "Chocolate", 400),
            new Drink("Cappucino", 3.00,"Hot", "/Images/cappucino.jpg", "Cappucino", 400),
            new Drink("Coke", 1.50, "Cold", "/Images/coke.jpg", "Coke", 330),
            new Drink("Sprite", 1.50, "Cold", "/Images/sprite.jpg", "Lemon", 330),
            new Drink("Latte", 3.00, "Cold", "/Images/latte.jpg", "Coffee", 330)
        };
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(NavigationPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
