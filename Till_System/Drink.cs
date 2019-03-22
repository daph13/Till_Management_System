using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Till_System
{
    public class Drink :Item
    {
        private string flavour;
        private int volume;

        public Drink()
            :base()
        {
            this.Flavour = "";
            this.Volume = 0;
        }

        public Drink(string name, double price, string type, string picture, string flavour, int volume)
            :base(name, price, type, picture)
        {
            this.Flavour = flavour;
            this.Volume = volume;
        }

        public string Flavour
        {
            get { return this.flavour; }
            set { this.flavour = value; }
        }

        public int Volume
        {
            get { return this.volume; }
            set { this.volume = value; }
        }

        public override string ToString()
        {
            return "Drink " + base.ToString() + "\nFlavour : " + Flavour + "\nVoume : " + Volume + "ml";
        }

    }
}
