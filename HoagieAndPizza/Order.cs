using System;
using System.Collections.Generic;
using System.Text;

namespace HoagieAndPizza
{
    // Order
    // SalesTax
    // SetDate
    // SubTotal
    // ToString
    // Total
        class Order : List<Product>
        {
            public DateTime Date { get; set; }
            public int OrderNumber { get; set; }

            public Order() { }


            public double SalesTax()
            {
                double tax = 0.08;
                return SubTotal() * tax;
            }

            public double SubTotal()
            {
                double subTotal = 0.0;
                foreach (Product p in this)
                {
                    subTotal += p.Price();
                }
                return subTotal;
            }

            public double Total()
            {
                return SubTotal() + SalesTax();
            }

            public DateTime SetDate()
            {
                Date = DateTime.Now;
                return Date;
            }

            public override string ToString()
            {
                string s = "Rish s Deli\r\n";
                s += "RECIEPT NUMBER : " 
                + OrderNumber + 
                "\r\n";
                s += "DATE&TIME " 
                + Date.ToShortDateString() 
                + " " +
                    Date.ToShortTimeString() 
                    + "\r\n\r\n";
                foreach (Product p in this)
                {
                    if (p.Type == "Pizza" || p.Type == "Hoagie")
                    {
                        s += p.ToString() + p.tops.ToString() + "Product Total: " + p.Price().ToString("C") + "\r\n\r\n";
                    }
                    else
                    {
                        s += p.ToString() + "\r\n\r\n";
                    }
                }

                s += "\r\n\r\nSUB-TOTAL " + SubTotal().ToString("C") + "\r\n";
                s += "SALES TAX: " + SalesTax().ToString("C") + "\r\n";
                s += "TOTAL: " + Total().ToString("C") + "\r\n";

                return s;
            }
        }
    }
