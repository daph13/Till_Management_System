using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Till_System
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ItemManagement : Page
    {
        //call items declared in App.xaml
        App thisApp = Application.Current as App;
        List<Food> foodList;
        List<Drink> drinkList;
        Food selectedFood;
        Drink selectedDrink;

        public ItemManagement()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //updates the food and drinks lists
            this.Update_Lists();
            //loads list box with items from itemList
            this.Load_AllItems();
            //gets images from solution's Images folder
            this.Get_Images();
        }

        //updates the food and drinks lists
        private void Update_Lists()
        {
            foodList = new List<Food>();
            drinkList = new List<Drink>();

            foreach (var item in thisApp.itemList)
            {
                if (item.GetType() == typeof(Food))
                {
                    foodList.Add((Food)item);
                }
                else if (item.GetType() == typeof(Drink))
                {
                    drinkList.Add((Drink)item);
                }
            }
        }

        //Clears all the text fields and sets the image to a default image
        private void Clear_Fields()
        {
            txtName.Text = "";
            txtPrice.Text = "";
            comboType.SelectedIndex = -1;
            txtPicture.Text = "";
            txtVarious.Text = "";
            txtVolume.Text = "";
            BitmapImage imgDefault = new BitmapImage();
            imgDefault.UriSource = new Uri("ms-appx:///Images/default.png");
            imgItem.Source = imgDefault;

            //deselect listbox
            if(listItems.SelectedIndex != -1)
            {
                listItems.SelectedIndex = -1;
            }

            selectedFood = new Food();
            selectedDrink = new Drink();

        }

        //loads list box with all items from itemList
        private void Load_AllItems()
        {
            //Hides the section for adding and editing items
            editSection.Visibility = Visibility.Collapsed;
            //clears all text fields
            this.Clear_Fields();
            //clear list box and repopulate it
            listItems.Items.Clear();
            foreach (Item item in thisApp.itemList)
            {
                listItems.Items.Add(item.ToString());

            }
        }

        //populate listbox with only food items
        private void Load_Food()
        {
            editSection.Visibility = Visibility.Visible;
            this.Clear_Fields();
            listItems.Items.Clear();
            foreach (Food foodItem in foodList)
            {
                listItems.Items.Add(foodItem.ToString());

            }
        }

        //populate listbox with only drink items
        private void Load_Drinks()
        {
            editSection.Visibility = Visibility.Visible;
            this.Clear_Fields();
            listItems.Items.Clear();
            foreach (Drink drinkItem in drinkList)
            {
                listItems.Items.Add(drinkItem.ToString());

            }
        }

        //Get list of images from Images folder
        private void Get_Images()
        {
            thisApp.images = new List<string>();
            //Get names of files in the Images folder
            string[] fileImages = System.IO.Directory.GetFiles("Images/");
            //Rename the files
            foreach (string file in fileImages)
            {
                 thisApp.images.Add("/" + file);
            }
        }

        private void ListItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //hide edit fields 
            editSection.Visibility = Visibility.Visible;

            if (listItems.SelectedIndex != -1)
            {
                //get selected item
                string selectedString = listItems.SelectedItem.ToString();
                string[] column = selectedString.Split(' ' ,'\n');
                //get first few strings for the id
                string idString = column[2] + " " + column[3] + " " + column[4];
                int number = 0;
                int index = 0;

                //get item index
                foreach(Item item in thisApp.itemList)
                {
                    if(idString == item.DisplayId())
                    {
                        index = number;
                    }
                    number++;
                }

                //if item is a Food item
                if (selectedString.Contains("Food Item"))
                {
                    //display fields related to food
                    this.Display_FoodFields();
                    //get Food item
                    selectedFood = (Food)thisApp.itemList[index];

                    //set fields
                    txtName.Text = selectedFood.Name;
                    txtPrice.Text = selectedFood.Price.ToString("N2");
                    txtPicture.Text = selectedFood.Picture;

                    // get list of ingredients and put them into a string
                    string ingredients = "";
                    int count = selectedFood.Ingredients.Count;
                    int counter = 0;
                    foreach(string item in selectedFood.Ingredients)
                    {
                        counter++;
                        if(counter == count)
                        {
                            ingredients += item;
                        }
                        else
                        {
                            ingredients += item + ",";
                        } 
                        
                    }

                    txtVarious.Text = ingredients;
                    
                    //get selected Food item image
                    BitmapImage food = new BitmapImage();
                    food.UriSource = new Uri("ms-appx://" + selectedFood.Picture);
                    imgItem.Source = food;

                    //get Food Type
                    if (selectedFood.Type.Contains("Hot"))
                    {
                        comboType.SelectedIndex = 0;
                    }
                    else if (selectedFood.Type.Contains("Cold"))
                    {
                        comboType.SelectedIndex = 1;
                    }
                    else
                    {
                        comboType.SelectedIndex = -1;
                    }

                }
                else
                {
                    //if item is a Drink item
                    //display fields related to drink
                    this.Display_DrinkFields();
                    
                    //get Drink item
                    selectedDrink = (Drink)thisApp.itemList[index];

                    //set fields
                    txtName.Text = selectedDrink.Name;
                    txtPrice.Text = selectedDrink.Price.ToString();
                    txtPicture.Text = selectedDrink.Picture;
                    txtVarious.Text = selectedDrink.Flavour;
                    txtVolume.Text = selectedDrink.Volume.ToString();

                    //get selected Drink item image
                    BitmapImage drink = new BitmapImage();
                    drink.UriSource = new Uri("ms-appx://" + selectedDrink.Picture);
                    imgItem.Source = drink;

                    //get Drink Type
                    if (selectedDrink.Type.Contains("Hot"))
                    {
                        comboType.SelectedIndex = 0;
                    }
                    else if (selectedDrink.Type.Contains("Cold"))
                    {
                        comboType.SelectedIndex = 1;
                    }
                    else
                    {
                        comboType.SelectedIndex = -1;
                    }
                }
            }

        }

        //populate listbox with all items
        private void BtnAll_Click(object sender, RoutedEventArgs e)
        {
            this.Load_AllItems();
        }

        //populate listbox with only Food items
        private void BtnFood_Click(object sender, RoutedEventArgs e)
        {
            this.Load_Food();
            this.Display_FoodFields();
        }

        //populate listbox with only Drink items
        private void BtnDrinks_Click(object sender, RoutedEventArgs e)
        {
            this.Load_Drinks();
            this.Display_DrinkFields();
        }

        //display fields related to Food
        private void Display_FoodFields()
        {
            lblItem.Text = "Food";
            lblVolume.Visibility = Visibility.Collapsed;
            txtVolume.Visibility = Visibility.Collapsed;
            lblVarious.Text = "Ingredients:";
        }

        //display fields related to Drink
        private void Display_DrinkFields()
        {
            lblItem.Text = "Drink";
            lblVolume.Visibility = Visibility.Visible;
            txtVolume.Visibility = Visibility.Visible;
            lblVarious.Text = "Flavour:";
        }

        //Clear all fields
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            this.Clear_Fields();
        }

        //Add Item
        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Food food;
            Drink drink;
            MessageDialog dialog;

            //error handling
            if(txtName.Text == "" || txtPrice.Text =="" || txtVarious.Text == "")
            {
                dialog = new MessageDialog("Fields cannot be empty");
                await dialog.ShowAsync();
            }
            else if (comboType.SelectedIndex == -1)
            {
                dialog = new MessageDialog("Please choose a type");
                await dialog.ShowAsync();
            }
            else if (double.Parse(txtPrice.Text) < 0)
            {
                dialog = new MessageDialog("Value cannot be less than 0");
                await dialog.ShowAsync();
            }
            else
            {
                    
                try
                {

                    //if the textbox is empty, display default image
                    string found = "";

                    if (txtPicture.Text == "")
                    {
                        txtPicture.Text = "/Images/default.png";
                    }
                    else
                    {
                        //check if image from textbox exists
                        foreach (string image in thisApp.images)
                        {
                            if (image.Contains(txtPicture.Text))
                            {
                                found = image;
                            }
                        }

                        //if image does not exist, display default image
                        if (found == "")
                        {
                            txtPicture.Text = "/Images/default.png";
                        }
                        else
                        {
                            //set correct image name to textbox
                            txtPicture.Text = found;
                        }
                    }


                    double price = double.Parse(txtPrice.Text);
               
                    string name = txtName.Text;
                    string type = ((ComboBoxItem)comboType.SelectedItem).Content.ToString();
                    string picture = txtPicture.Text;
                    string flavour = txtVarious.Text;
                    int volume;
                    string ingredientsText;
                    string[] columns;

                    //check if Drink Item field exists
                    if (txtVolume.Visibility == Visibility.Visible)
                    {
                        //error handling
                        if (txtVolume.Text == "")
                        {
                            dialog = new MessageDialog("Please enter the volume");
                        }
                        else
                        {
                            try
                            {
                                //add Drink Item to itemList
                                volume = int.Parse(txtVolume.Text);
                                drink = new Drink(name, price, type, picture, flavour, volume);
                                thisApp.itemList.Add(drink);

                                //update food and drink lists
                                this.Update_Lists();
                                //clear listbox
                                listItems.Items.Clear();

                                //repopulate listbox with Drink items
                                foreach (Drink d in drinkList)
                                {
                                    listItems.Items.Add(d.ToString());
                                }
                            }
                            //catch exceptions
                            catch (FormatException)
                            {
                                dialog = new MessageDialog("Please enter a valid number", "Wrong format");
                                await dialog.ShowAsync();
                            }
                            catch (Exception exception)
                            {
                                dialog = new MessageDialog(exception.Message, "Error");
                                await dialog.ShowAsync();
                            }
                        }
                    }
                    else
                    {
                        //if item is a Food item
                        ingredientsText = txtVarious.Text;
                        //split ingredients string to get individual ingredients
                        columns = ingredientsText.Split(',');

                        //add to a string list of ingredients
                        List<string> ingredients = new List<string>();

                        foreach (string s in columns)
                        {
                            ingredients.Add(s);
                        }

                        //add Food item
                        food = new Food(name, price, type, picture, ingredients);
                        thisApp.itemList.Add(food);

                        //update food and drink lists
                        this.Update_Lists();
                        //clear list box
                        listItems.Items.Clear();

                        //repopulate listbox with Food items
                        foreach (Food f in foodList)
                        {
                            listItems.Items.Add(f.ToString());
                        }
                    }
                }
                //catch exception
                catch(FormatException)
                {
                    dialog = new MessageDialog("Please enter a valid input", "Invalid input");
                    await dialog.ShowAsync();
                }
                //clear edit fields
                this.Clear_Fields();
            }


        }

        //Remove Item
        private async void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog;
            //error handling
            if(listItems.SelectedIndex == -1)
            {
                dialog = new MessageDialog("Please select an item to remove", "No Item Selected");
                await dialog.ShowAsync();
            }
            else
            {
                //confirm if user wants to remove the item
                dialog = new MessageDialog("Are you sure you want to remove this item?", "Confirmation");
                var yesCommand = new UICommand("Yes");
                var noCommand = new UICommand("No");
                dialog.Commands.Add(yesCommand);
                dialog.Commands.Add(noCommand);

                var command = await dialog.ShowAsync();

                //if user selects "Yes"
                if (command == yesCommand)
                {
                    //remove selected item from itemList
                    Item selectedItem = new Item();
                    foreach (Item item in thisApp.itemList)
                    {
                        if (listItems.SelectedItem.ToString().Contains(item.DisplayId()))
                        {
                            selectedItem = item;
                        }
                    }

                    //remove selected item from listbox
                    thisApp.itemList.Remove(selectedItem);
                    //add removed item to removedItems list
                    thisApp.removedItems.Add(selectedItem);

                    dialog = new MessageDialog(selectedItem.Name + " has been removed", "Item Removed");
                    await dialog.ShowAsync();

                    //update food and drink lists
                    this.Update_Lists();

                    //if item removed is a Food item
                    if (selectedItem is Food)
                    {
                        //repopulate lisbox with Food items
                        this.Load_Food();
                    }
                    else
                    {
                        //else repopulate listbox with Drink items
                        this.Load_Drinks();
                    }
                }

            }
        }

        //Update Item
        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog;

            //if item is not selected, prompt user to select an item
            if (listItems.SelectedIndex == -1)
            {
                dialog = new MessageDialog("Please select an item to update", "Select an Item");
                await dialog.ShowAsync();
            }
            else
            {
                //error handling
                if (txtName.Text == "" || txtPrice.Text == "" || txtVarious.Text == "")
                {
                    dialog = new MessageDialog("Fields cannot be empty");
                    await dialog.ShowAsync();
                }
                else if (comboType.SelectedIndex == -1)
                {
                    dialog = new MessageDialog("Please choose a type");
                    await dialog.ShowAsync();
                }
                else if (double.Parse(txtPrice.Text) < 0)
                {
                    dialog = new MessageDialog("Value cannot be less than 0");
                    await dialog.ShowAsync();
                }
                else
                {
                    //confirm if user wants to update the item
                    dialog = new MessageDialog("Are you sure you want to update this item?", "Confirmation");
                    var yesCommand = new UICommand("Yes");
                    var noCommand = new UICommand("No");
                    dialog.Commands.Add(yesCommand);
                    dialog.Commands.Add(noCommand);

                    var command = await dialog.ShowAsync();

                    //if user selects "Yes"
                    if (command == yesCommand)
                    {
                        try
                        {

                            string found = "";
                            //if text field is empty, set picture to default image
                            if (txtPicture.Text == "")
                            {
                                txtPicture.Text = "/Images/default.png";
                            }
                            else
                            {
                                //else if image string is found, get the image 
                                foreach (string image in thisApp.images)
                                {
                                    if (image.Contains(txtPicture.Text))
                                    {
                                        found = image;
                                    }
                                }

                                //if picture is not found, set picture to default image
                                if (found == "")
                                {
                                    txtPicture.Text = "/Images/default.png";
                                }
                                else
                                {
                                    //else set the image
                                    txtPicture.Text = found;
                                }
                            }

                            double price = double.Parse(txtPrice.Text);

                            string name = txtName.Text;
                            string type = ((ComboBoxItem)comboType.SelectedItem).Content.ToString();
                            string picture = txtPicture.Text;
                            string flavour = txtVarious.Text;
                            int volume;
                            string ingredientsText;
                            string[] columns;
                            int index;

                            //if Drink field is visible
                            if (txtVolume.Visibility == Visibility.Visible)
                            {
                                if (txtVolume.Text == "")
                                {
                                    dialog = new MessageDialog("Please enter the volume");
                                }
                                else
                                {
                                    try
                                    {
                                        volume = int.Parse(txtVolume.Text);
                                        
                                        //update selectedDrink values
                                        selectedDrink.Name = name;
                                        selectedDrink.Price = price;
                                        selectedDrink.Type = type;
                                        selectedDrink.Picture = picture;
                                        selectedDrink.Flavour = flavour;
                                        selectedDrink.Volume = volume;

                                        //update food and drink lists
                                        this.Update_Lists();
                                        //clear listsbox
                                        listItems.Items.Clear();

                                        //repopulate listbox with Drink items
                                        foreach (Drink d in drinkList)
                                        {
                                            listItems.Items.Add(d.ToString());
                                        }
                                    }
                                    //catch errors
                                    catch (FormatException)
                                    {
                                        dialog = new MessageDialog("Please enter a valid number", "Wrong format");
                                        await dialog.ShowAsync();
                                    }
                                    catch (Exception exception)
                                    {
                                        dialog = new MessageDialog(exception.Message, "Error");
                                        await dialog.ShowAsync();
                                    }
                                }
                            }
                            else
                            {

                                ingredientsText = txtVarious.Text;
                                //split ingredients string to get each ingredient
                                columns = ingredientsText.Split(',');

                                List<string> ingredients = new List<string>();

                                foreach (string s in columns)
                                {
                                    ingredients.Add(s);
                                }

                                //update selectedFood values
                                selectedFood.Name = name;
                                selectedFood.Price = price;
                                selectedFood.Type = type;
                                selectedFood.Picture = picture;
                                selectedFood.Ingredients = ingredients;

                                //update food and drink lists
                                this.Update_Lists();
                                //clear listbox
                                listItems.Items.Clear();

                                //repopulate listbox with Food items
                                foreach (Food f in foodList)
                                {
                                    listItems.Items.Add(f.ToString());
                                }
                            }
                        }
                        //catch errors
                        catch (FormatException)
                        {
                            dialog = new MessageDialog("Please enter a valid input", "Invalid input");
                            await dialog.ShowAsync();
                        }

                        //clear edit fields
                        this.Clear_Fields();
                        dialog = new MessageDialog("Item has been successfully updated");
                        
                    }
                }
            }
        }

        private async void BtnAddImage_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog d;
            //open a FilePicker
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            //set the location to the Pictures Library
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            //show files with the following extensions
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            //get solution's path
            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            //get the path of the solution's Images folder
            string path = root + @"\Images";

            //open the Filepicker where one can only pick one file
            //get the selected file
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {

                try
                {
                    //get the Images folder as a StorageFolder to store the selected file
                    StorageFolder copyToFolder = await StorageFolder.GetFolderFromPathAsync(path);

                    //copy the selected file to the Images folder
                    StorageFile copiedFile = await file.CopyAsync(copyToFolder, file.Name.ToString(), NameCollisionOption.ReplaceExisting);

                    d = new MessageDialog("Picked photo: " + file.Name);
                    await d.ShowAsync();

                    //create a list of image strings from the Images folder
                    this.Get_Images();
                    
                    //display the name of the file in the textbox
                    txtPicture.Text = "/Images/" + file.Name;

                    //display the image
                    BitmapImage item = new BitmapImage();
                    item.UriSource = new Uri("ms-appx://" + txtPicture.Text);
                    imgItem.Source = item;
                }
                //catch the error if the file clicked already exists
                catch (System.IO.FileLoadException)
                {
                    d = new MessageDialog("This picture already exists in this application");
                    await d.ShowAsync();
                }
            }

        }

        //get images from Images folder and display them
        private async void BtnFindImage_Click(object sender, RoutedEventArgs e)
        {
      
            MessageDialog d;
            //create ContentDialog called PicturePicker
            PicturePicker picturePicker = new PicturePicker();
            BitmapImage item = new BitmapImage();

            //show the ContentDialog
            var optionPicked = await picturePicker.ShowAsync();
            switch (optionPicked)
            {   
                //if he user clicks the Primary (OK) button
                case ContentDialogResult.Primary:
                    //if picture is picked
                    if (thisApp.picturePicked != "")
                    {
                        //display selected picture in textbox
                        txtPicture.Text = thisApp.picturePicked;
                        //reset picturePicked variable
                        thisApp.picturePicked = "";
                        
                        //display image picked
                        item.UriSource = new Uri("ms-appx://" + txtPicture.Text);
                        imgItem.Source = item;

                    }
                    //if no picture is picked
                    else
                    {
                        //display message
                        d = new MessageDialog("Nothing picked");
                        await d.ShowAsync();
                        //clear textbox
                        txtPicture.Text = "";

                        //display default image
                        item.UriSource = new Uri("ms-appx:///Images/default.png");
                        imgItem.Source = item;
                    }
                    //close ContentDialog
                    break;
                    //if Secondary (Cancel) button is clicked
                    //close ContentDialog
                case ContentDialogResult.Secondary:
                    break;
            }
        }

  
    }
}
