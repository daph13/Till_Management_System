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
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Till_System
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SalesStatsPage : Page
    {
        //call items declared in App.xaml
        App thisApp = Application.Current as App;
        int totalOrderedItems = 0;
        double totalSales = 0.00;
        List<Item> oldItemsList;
        public SalesStatsPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            oldItemsList = new List<Item>();
            //add current items in itemList to oldItemsList
            foreach(Item item in thisApp.itemList)
            {
                oldItemsList.Add(item);
            }

            //add removed items from itemList to oldItemsList
            foreach(Item item in thisApp.removedItems)
            {
                oldItemsList.Add(item);
            }

            //loop through each order from the ordersList
            foreach (Order order in thisApp.ordersList)
            {
                //add orders to the listbox
                listBoxOrders.Items.Add(order.ToString());
                //add number of items from each order to the total number of ordered items
                totalOrderedItems += order.NumItems;
                //add the sales amount including discount to the total sales
                totalSales += order.AfterDiscount;
                
            }

            //display these values
            txtTotalOrders.Text = thisApp.ordersList.Count.ToString();
            txtTotalItems.Text = totalOrderedItems.ToString();
            txtTotalSales.Text = "£" + totalSales.ToString("N2");

            //get the total sold for each item and display in listbox
            this.Load_Items();

            //arrange the items in oldItemsList in descending order 
            //based on the total of each item sold
            var topSells = oldItemsList.OrderByDescending(x => x.TotalNumSold).ToList();

            //get the first item and if it is not 0
            if(topSells[0].TotalNumSold != 0)
            {
                //display first item name
                txtFirst.Text = topSells[0].Name;
            }
            else
            {
                //else display "None"
                txtFirst.Text = "None";
            }

            //get second item and if it is not 0
            if (topSells[1].TotalNumSold != 0)
            {
                //display second item name
                txtSecond.Text = topSells[1].Name;
            }
            else
            {
                //else display "None"
                txtSecond.Text = "None";
            }

            //get third item and if it is not 0
            if (topSells[2].TotalNumSold != 0)
            {
                //display third item name
                txtThird.Text = topSells[2].Name;
            }
            else
            {
                //else display "None"
                txtThird.Text = "None";
            }


        }

        private void Load_Items()
        {
            //loop through allOrderedItems list
            foreach(Item item in thisApp.allOrderedItems)
            {
                //get the total number of each item sold
                item.TotalNumSold++;

            }
            //loop through oldItemsList
            foreach(Item item in oldItemsList)
            {
                //display the name and total number of each item sold in listbox
                listItemsSummary.Items.Add(item.DisplayItemSummary());
            }

        }

        private void ListBoxOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get index of selected item
            int index = listBoxOrders.SelectedIndex;
            //get the Order item from the ordersList
            Order order = thisApp.ordersList[index];
            string orderSummary = "Order :\t" + order.ID + "\n";
            

            //loop through oldItemsList
            foreach(Item item in oldItemsList)
            {
                //reset the number of each item sold in each order
                item.ResetItemsInOrder();               
            }

            //loop through the orderItems of the order
            foreach(Item item in order.OrderItems)
            {
                //get the number of each item sold in the order
                item.SoldEachOrder++;
            }

            //loop through the oldItemsList
            foreach(Item item in oldItemsList)
            {
                //if the number of the item sold in each order is not 0
                if (item.SoldEachOrder != 0)
                {
                    //display the item
                    orderSummary += "\n" + item.DisplayItemSoldPerOrder();
                }
            }

            //if the order's discount is 0
            if (order.Discount == 0)
            {
                //display text without discount
                orderSummary += "\n\nTotal :\t\t£" + order.TotalAmount.ToString("N2") + "\nAmount Paid :\t£" + order.AmountPaid.ToString("N2") + "\nChange :\t£" + order.Change.ToString("N2");
            }
            else
            {
                //else display text with discount
                orderSummary += "\n\nTotal :\t\t£" + order.AfterDiscount.ToString("N2") + "\nDiscount :\t" + order.Discount.ToString("p") + "\nAmount Paid :\t£" + order.AmountPaid.ToString("N2") + "\nChange :\t£" + order.Change.ToString("N2");
            }

            //clear richtextbox
            txtDisplayOrder.Blocks.Clear();
            Paragraph paragraph = new Paragraph();
            Run run = new Run();
            
            //get string to display
            run.Text = orderSummary;
            
            paragraph.Inlines.Add(run);
            //display the string
            txtDisplayOrder.Blocks.Add(paragraph);

        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            //loop through allOrderedItems list
            foreach (Item item in thisApp.allOrderedItems)
            {
                //set the total number of each item sold to 0
                item.TotalNumSold = 0;

            }
        }
    }
}
