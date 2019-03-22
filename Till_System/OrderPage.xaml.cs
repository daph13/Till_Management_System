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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Till_System
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrderPage : Page
    {
        //call items declared in App.xaml
        App thisApp = Application.Current as App;
        List<Food> foodList;
        List<Drink> drinkList;
        List<Item> orderedItems;
        private int orderNumber;
        private double total = 0;
        private const double managementDisc = 0.5;
        private const double staffDisc = 0.25;
        private const double othersDisc = 0.1;
        private double discount = 0;
        private double afterDiscount = 0;
        private double amountPaid = 0;
        public OrderPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //update food and drink lists
            this.Update_Lists();
            //bind the gridItems GridView to foodList
            gridItems.ItemsSource = foodList;
            //get current Order number
            orderNumber = thisApp.ordersList.Count + 1;
            txtOrderNumber.Text = orderNumber.ToString();
            //hide discount stackpanel
            discountPanel.Visibility = Visibility.Collapsed;

        }

        //update food and drink lists
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

        //calculate discounted total and display discount and discounted total
        private void Discount_Fields()
        {
            txtDiscount.Text = discount.ToString("p");

            if (discount != 0)
            {
                afterDiscount = total * (1 - discount);
                txtAfterDiscount.Text = "£" + afterDiscount.ToString("N2");
            }
            else
            {
                afterDiscount = total;
                txtAfterDiscount.Text = "£" + afterDiscount.ToString("N2");
            }
        }

        //Clear textFields and reset values
        private void Clear_Fields()
        {
            total = 0;
            discount = 0;
            afterDiscount = 0;
            amountPaid = 0;
            
            txtTotal.Text = "";
            txtDiscount.Text = "";
            txtAfterDiscount.Text = "";
            txtAmountPaid.Text = "";
        }

        private void Btn7_Click(object sender, RoutedEventArgs e)
        {
            //add number 7 to the textbox
            txtAmountPaid.Text += "7";
        }

        private void Btn8_Click(object sender, RoutedEventArgs e)
        {
            //add number 8 to the textbox
            txtAmountPaid.Text += "8";
        }

        private void Btn9_Click(object sender, RoutedEventArgs e)
        {
            //add number 9 to the textbox
            txtAmountPaid.Text += "9";
        }

        private void Btn4_Click(object sender, RoutedEventArgs e)
        {
            //add number 4 to the textbox
            txtAmountPaid.Text += "4";
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            //add number 1 to the textbox
            txtAmountPaid.Text += "1";
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            //add number 2 to the textbox
            txtAmountPaid.Text += "2";
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            //add number 3 to the textbox
            txtAmountPaid.Text += "3";
        }

        private void Btn0_Click(object sender, RoutedEventArgs e)
        {
            //add number 0 to the textbox
            txtAmountPaid.Text += "0";
        }

        private void Btn00_Click(object sender, RoutedEventArgs e)
        {
            //add number 00 to the textbox
            txtAmountPaid.Text += "00";
        }

        private void BtnDot_Click(object sender, RoutedEventArgs e)
        {
            //add "." to the textbox
            txtAmountPaid.Text += ".";
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            //clear the txtAmountPaid textbox
            txtAmountPaid.Text = "";
        }

        private void Btn5_Click(object sender, RoutedEventArgs e)
        {
            //add number 5 to the textbox
            txtAmountPaid.Text += "5";
        }

        private void Btn6_Click(object sender, RoutedEventArgs e)
        {
            //add number 6 to the textbox
            txtAmountPaid.Text += "6";
        }

        private async void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog;
            orderedItems = new List<Item>();

            ///error handling
            if (afterDiscount != 0)
            {

                if (txtAmountPaid.Text == "")
                {
                    dialog = new MessageDialog("Please enter an amount");
                    await dialog.ShowAsync();
                }
                else
                {
                    try
                    {
                        amountPaid = double.Parse(txtAmountPaid.Text);
                        double change = amountPaid - afterDiscount;

                        if (change < 0)
                        {
                            dialog = new MessageDialog("Amount is not enough");
                            await dialog.ShowAsync();
                            txtAmountPaid.Text = "";
                        }
                        else
                        {

                            if (change == 0.00)
                            {
                                dialog = new MessageDialog("Order finished");
                                await dialog.ShowAsync();
                            }
                            else if (change > 0)
                            {
                                dialog = new MessageDialog("Change: £" + change.ToString("N2"));
                                await dialog.ShowAsync();
                            }

                           //loop through each orderItem in the listbox
                            foreach (string orderItem in listBoxOrder.Items)
                            {
                                //split the orderItem string
                                string[] column = orderItem.Split(' ', '\t');
                                //get string showing the ID
                                string idString = column[0] + " " + column[1] + " " + column[2];

                                //loop through the itemList
                                foreach (Item item in thisApp.itemList)
                                {
                                    //if the orderItem ID string is the same as the item ID string
                                    if (idString == item.DisplayId())
                                    {
                                        //add the Item to the orderedItems list for the current order
                                        orderedItems.Add(item);
                                        //add the Item to the allOrderedItems list
                                        thisApp.allOrderedItems.Add(item);
                                    }

                                }
                            }

                            //create a new Order
                            Order newOrder = new Order(orderedItems, amountPaid, discount);
                            //set the change
                            newOrder.Change = change;
                            //add current order to ordersList
                            thisApp.ordersList.Add(newOrder);

                            //clear the listbox
                            listBoxOrder.Items.Clear();
                            //clear all the textfields and reset values
                            this.Clear_Fields();
                            //set to the next Order number
                            orderNumber = thisApp.ordersList.Count + 1;
                            txtOrderNumber.Text = orderNumber.ToString();


                        }
                    }
                    //catch errors
                    catch (FormatException fE)
                    {
                        dialog = new MessageDialog(fE.Message);
                        await dialog.ShowAsync();
                    }
                    catch (Exception ex)
                    {
                        dialog = new MessageDialog(ex.Message);
                        await dialog.ShowAsync();
                    }
                }
            }

        }

        private void BtnFood_Click(object sender, RoutedEventArgs e)
        {
            //empty the gridDrinkItems GridView
            gridDrinkItems.ItemsSource = null;
            //populate gridItems GridView with Food items
            gridItems.ItemsSource = foodList;
            //hide discount stackpanel
            discountPanel.Visibility = Visibility.Collapsed;

        }

        private void BtnDrinks_Click(object sender, RoutedEventArgs e)
        {
            //empty the gridItems GridView
            gridItems.ItemsSource = null;
            //populate gridDrinkItems GridView with Drink items
            gridDrinkItems.ItemsSource = drinkList;
            //hide discount panel
            discountPanel.Visibility = Visibility.Collapsed;

        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            //if selection is not null
            if (listBoxOrder.SelectedIndex != -1)
            {
                //get index of the selected item
                int index = listBoxOrder.SelectedIndex;

                //get the string of the selected item
                string selectedString = listBoxOrder.SelectedItem.ToString();
                //split the item's string
                string[] column = selectedString.Split(' ', '\t');
                //get the string consisting of the Item's ID
                string idString = column[0] + " " + column[1] + " " + column[2];
                Item tempItem = new Item();

                //loop through the itemList
                foreach (Item item in thisApp.itemList)
                {
                    //is the idString is the same as the item'd string ID in itemList
                    if(idString == item.DisplayId())
                    {
                        //get the Item
                        tempItem = item;
                    }
                }

                //minus the Item's price from the total
                total -= tempItem.Price;
                txtTotal.Text = "£" + total.ToString("N2");
                //remove the item from the listBox
                listBoxOrder.Items.RemoveAt(index);
                //update discounted total and display discount and discounted total
                this.Discount_Fields();

            }
            else
            {
                //else prompt user to select an item to remove
                var dialog = new MessageDialog("Please select an item to remove", "Item not selected");
                await dialog.ShowAsync();
            }

        }

        private async void BtnClearOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog;
            //if listbox is not empty
            if (listBoxOrder.Items.Count != 0)
            {
                //confirm with user to clear listbox
                dialog = new MessageDialog("Are you sure you want to clear the order?", "Confirmation");
                var yesCommand = new UICommand("Yes");
                var noCommand = new UICommand("No");
                dialog.Commands.Add(yesCommand);
                dialog.Commands.Add(noCommand);

                var command = await dialog.ShowAsync();
                //if user selects "Yes"
                if (command == yesCommand)
                {
                    //clear listbox
                    listBoxOrder.Items.Clear();
                    //clear fields and reset values
                    this.Clear_Fields();
                }
            }
            //else tell user the listbox is already empty
            else
            {
                dialog = new MessageDialog("The order is already empty", "It's Empty!");
                await dialog.ShowAsync();
            }
        }

        private void BtnDiscount_Click(object sender, RoutedEventArgs e)
        {
            //empty gridDrinkItems GridView
            gridDrinkItems.ItemsSource = null;
            //empty gridItems GridView
            gridItems.ItemsSource = null;
            //show the discountPanel stackpanel
            discountPanel.Visibility = Visibility.Visible;
        }

        private void GridItems_ItemClick(object sender, ItemClickEventArgs e)
        {
            //get the index of the selected GridView item
            int index = gridItems.Items.IndexOf(e.ClickedItem);

            //get the Food item
            Food food = foodList[index];

            //add the Food item to the first position in the listbox
            listBoxOrder.Items.Insert(0, food.DisplayonOrderList());

            //add Food item price to the total
            total += food.Price;
            //display the total
            txtTotal.Text = "£" + total.ToString("N2");
            //calculate the discounted total and display discount and discounted total
            this.Discount_Fields();

        }

        private void GridDrinkItems_ItemClick(object sender, ItemClickEventArgs e)
        {
            //get the index of the selected GridView item
            int index = gridDrinkItems.Items.IndexOf(e.ClickedItem);

            //get the Drink item
            Drink drink = drinkList[index];
            //add the Drink item to the first position in the listbox
            listBoxOrder.Items.Insert(0,drink.DisplayonOrderList());

            //add Drink item price to the total
            total += drink.Price;
            //display the total
            txtTotal.Text = "£" + total.ToString("N2");
            //calculate the discounted total and display discount and discounted total
            this.Discount_Fields();
        }

        private void BtnStaff_Click(object sender, RoutedEventArgs e)
        {
            //set discount to staff discount
            discount = staffDisc;
            //calculate the discounted total and display discount and discounted total
            this.Discount_Fields();
        }

        private void BtnStudent_Click(object sender, RoutedEventArgs e)
        {
            //set discount to others discount (for student and senior)
            discount = othersDisc;
            //calculate the discounted total and display discount and discounted total
            this.Discount_Fields();
        }

        private void BtnManagement_Click(object sender, RoutedEventArgs e)
        {
            //set discount to management discount
            discount = managementDisc;
            //calculate the discounted total and display discount and discounted total
            this.Discount_Fields();
        }

        private void BtnSenior_Click(object sender, RoutedEventArgs e)
        {
            //set discount to others discount (for student and senior)
            discount = othersDisc;
            //calculate the discounted total and display discount and discounted total
            this.Discount_Fields();
        }

        private void BtnRemoveDisc_Click(object sender, RoutedEventArgs e)
        {
            //set discount to 0
            discount = 0;
            //calculate the discounted total and display discount and discounted total
            this.Discount_Fields();
        }
    }
}
