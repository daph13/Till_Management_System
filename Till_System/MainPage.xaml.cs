using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Till_System
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ImgAdmin_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //Navigate to the Admin Page
            Frame.Navigate(typeof(ItemManagement));
        }

        private void ImgOrder_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //Navigate to the Order Page
            Frame.Navigate(typeof(OrderPage));
        }

        private void ImgStats_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //Navigate to the Statistics Page
            Frame.Navigate(typeof(SalesStatsPage));
        }
    }
}
