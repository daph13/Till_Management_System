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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Till_System
{
    public sealed partial class PicturePicker : ContentDialog
    {
        //call items declared in App.xaml
        App thisApp = Application.Current as App;
        public PicturePicker()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            //populate gridPictures with images from the images list
            gridPictures.ItemsSource = thisApp.images;
        }

        private void GridPictures_ItemClick(object sender, ItemClickEventArgs e)
        {
            //get the clicked GridView item
            int index = gridPictures.Items.IndexOf(e.ClickedItem);
            //get the image from the images list
            thisApp.picturePicked = thisApp.images[index];
        }
    }
}
