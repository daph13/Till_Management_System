using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Till_System
{
    public class Item
    {
        private int id;
        private string name;
        private double price;
        private string type;
        private string picture;
        private int totalNumSold = 0;
        private int soldEachOrder = 0;
             
        private static int count = 1;

        public Item()
        {
            this.ID = count;
            this.Name = "";
            this.Price = 0.00;
            this.Type = "";
            this.Picture = "";
        }

        public Item(string name, double price, string type, string picture)
        {
            this.ID = count;
            this.Name = name;
            this.Price = price;
            this.Type = type;
            this.Picture = picture;
            count++;
        }

        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public double Price
        {
            get { return this.price; }
            set { this.price = value; }
        }

        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public string Picture
        {
            get { return this.picture; }
            set { this.picture = value; }
        }

        public int TotalNumSold
        {
            get { return this.totalNumSold; }
            set { this.totalNumSold = value; }
        }

        public int SoldEachOrder
        {
            get { return this.soldEachOrder; }
            set { this.soldEachOrder = value; }
        }

        public void ResetItemsInOrder()
        {
            this.SoldEachOrder = 0;
        }


        public string DisplayId()
        {
            return "ID : " + ID; 
        }

        public override string ToString()
        {
            return "Item ID : " + ID + "\nName : " + Name + "\nPrice : £" + Price.ToString("N2") + "\nType : " + Type;
        }

        public string DisplayonOrderList()
        {
            return "ID : " + ID + "\t" + Name + "\t\t£" + Price.ToString("N2");
        }

        public string DisplayItemSummary()
        {
            return Name + " :\t" + this.TotalNumSold;
        }

        public string DisplayItemSoldPerOrder()
        {
            return Name + " :\t" + this.SoldEachOrder;
        }


    }
}
