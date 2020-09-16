using System.Text;

namespace HoagieAndPizza
{
    class Product
    {
       
       
            public Toppings tops = new Toppings(6);
            public double BasePrice { get; set; }
            public string Name { get; set; }
            public string Size { get; set; }
            public string Type { get; set; }

            public Product(Toppings t)
            {
                this.tops = t;
            }

            public Product(Toppings t, double bp, string n, string s, string typ)
            {

                this.tops = t;
                BasePrice = bp;
                Name = n;
                Size = s;
                Type = typ;
            }

            public Product()
            {
                this.tops = null;
            }

            public Product(double basePrice, string name, string size, string type)
            {
                this.tops = new Toppings(6);
                BasePrice = basePrice;
                Name = name;
                Size = size;
                Type = type;
            }

            public double Price()
            {
                double sum = 0;
                if (Type == "Pizza" || Type == "Hoagie")
                {
                    for (int i = 0; i < tops.ToppingPrices.Length; i++)
                    {
                        if (tops.ToppingList[i] == true)
                            sum += tops.ToppingPrices[i];
                    }
                    return this.BasePrice + sum;
                }
                else
                {
                    return this.BasePrice;
                }
            }

            public override string ToString()
            {
                string s = "" + Name +
                " " + Type + " " + 
                Size + " $" +
                BasePrice + 
                "\r\n";

                return s;
            }

        }
    }

