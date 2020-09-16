using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HoagieAndPizza
{

    enum HoagieTops
    {
        HotPeppers, Mayo, Oil, Onion, Oregano, SweetPeppers
    }

    enum PizzaTops
    {
        Bacon, ExtraCheese, Mushrooms, Onion, Peppers, Sausage
    }


    class Toppings
    {
        public bool[] ToppingList;
        public string[] ToppingNames;
        public double[] ToppingPrices;

        public Toppings(int length)
        {
            ToppingList = new bool[length];
            ToppingNames = new string[length];
            ToppingPrices = new double[length];
        }

        public Toppings Copy()
        {
            Toppings copy = new Toppings(ToppingList.Length);
            copy.ToppingList = CopyToppingList();
            copy.ToppingNames = this.CopyToppingNames();
            copy.ToppingPrices = this.CopyToppingPrices();
            return copy;
        }

        public bool[] CopyToppingList()
        {
            bool[] fArray = new bool[ToppingList.Count<bool>()];
            for (int i = 0; i < ToppingList.Length; i++)
            {
                fArray[i] = ToppingList[i];
            }
            return fArray;
        }

        public string[] CopyToppingNames()
        {
            string[] stringArray = new string[ToppingNames.Count<string>()];
            for (int i = 0; i < ToppingNames.Length; i++)
            {
                stringArray[i] = ToppingNames[i];
            }
            return stringArray;
        }

        public double[] CopyToppingPrices()
        {
            double[] numbArray = new double[ToppingPrices.Count<double>()];
            for (int i = 0; i < ToppingPrices.Length; i++)
            {
                numbArray[i] = ToppingPrices[i];
            }
            return numbArray;
        }

        public override string ToString()
        {
            string s = "";
            for (int item = 0; item < ToppingNames.Length; item++)
            {
                if (ToppingList[item] == true)
                {
                    s += "\t" + this.ToppingNames[item] + this.ToppingPrices[item].ToString("C") + "\r\n";
                }
            }
            return s;
        }
    }
    }
