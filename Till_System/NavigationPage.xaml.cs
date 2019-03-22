using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Till_System
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NavigationPage : Page
    {
        public NavigationPage()
        {
            this.InitializeComponent();
            //default navigation to MainPage
            currentFrame.Navigate(typeof(MainPage));
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            //navigate to MainPage
            currentFrame.Navigate(typeof(MainPage));
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            //navigate to AdminPage
            currentFrame.Navigate(typeof(ItemManagement));
        }

        private void BtnStats_Click(object sender, RoutedEventArgs e)
        {
            //navigate to SalesStatsPage
            currentFrame.Navigate(typeof(SalesStatsPage));
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            //navigate to OrderPage
            currentFrame.Navigate(typeof(OrderPage));
        }
    }
}
