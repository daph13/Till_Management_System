using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Till_System
{
    public class Order
    {
        private int id;
        private List<Item> orderItems;
        private int numItems;
        private double totalAmount;
        private double amountPaid;
        private double discount;
        private double afterDiscount;
        private double change;
        private static int count = 1;


        public Order()
        {
            this.ID = count;
            this.OrderItems = new List<Item>();
            this.NumItems = 0;
            this.TotalAmount = 0;
            this.AmountPaid = 0;
            this.Discount = 0;
            this.AfterDiscount = 0;
        }

        public Order(List<Item> items, double amountPaid, double discount)
        {
            this.ID = count;
            this.OrderItems = items;
            this.NumItems = this.NumItems;
            this.TotalAmount = this.TotalAmount;
            this.AmountPaid = amountPaid;
            this.Discount = discount;
            this.AfterDiscount = this.AfterDiscount;
            count++;
        }

        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public List<Item> OrderItems
        {
            get { return this.orderItems; }
            set { this.orderItems = value; }
        }

        public int NumItems
        {
            get { return this.numItems; }
            set
            {
                this.numItems = OrderItems.Count;
            }
        }

        public double TotalAmount
        {
            get { return this.totalAmount; }
            set
            {
                foreach (Item item in OrderItems)
                {
                   this.totalAmount += item.Price;
                }

            }
        }

        public double AmountPaid
        {
            get { return this.amountPaid; }
            set { this.amountPaid = value; }
        }

        public double Discount
        {
            get { return this.discount; }
            set { this.discount = value; }
        }

        public double AfterDiscount
        {
            get { return this.afterDiscount; }
            set
            {
                this.afterDiscount = this.TotalAmount * (1 - this.Discount);
            }
        }

        public double Change
        {
            get { return this.change; }
            set { this.change = value; }
        }



        public override string ToString()
        {
            return "Order ID : " + ID + "\nNumber of Items :\t" + NumItems + "\nAmount :\t£" + AfterDiscount.ToString("N2");
        }
        


    }
}
