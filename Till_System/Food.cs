using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Till_System
{
    public class Food : Item
    {
   
        private List<string> ingredients;
        

        public Food()
            :base()
        {
            this.Ingredients = new List<string>();
        }

        public Food(string name, double price, string type, string picture, List<string> ingredients)
            :base(name, price, type, picture)
        {
            this.Ingredients = ingredients;
        }


        public List<string> Ingredients
        {
            get { return this.ingredients; }
            set { this.ingredients = value; }
        }

        public override string ToString()
        {
            string ingredients = "";
            int count = Ingredients.Count;
            int counter = 0;
            foreach (string item in Ingredients)
            {
                counter++;
                if (counter == count)
                {
                    ingredients += item;
                }
                else
                {
                    ingredients += item + ",";
                }

            }

            return "Food " + base.ToString() + "\nIngredients : "  + ingredients;
        }

    }
}

