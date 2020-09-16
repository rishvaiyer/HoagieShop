using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace HoagieAndPizza
{
    //GetConnection
    //GetConnectionString
    // GetMaxLineItemID
    //MaxOrderID
    // GetProductPrice
    // GetToppings
    // InsertLineItem
    // InsertLineItemToppings
    // InsertOrder



    class ProductDB
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }


        public static string GetConnectionString()
        {
            return File.ReadAllText(@"..\..\HoagieAndPizzaConStr.txt");
        }


        //Getting MAX ID’s It’s likely that MS Access is multi-threaded.  
        //There seems to be a concurrency issue when inserting an Order or LineItem and
        //immediately retrieving the ID.  The insert is not completed before the query for the MAX ID.
        //To fix this, close the connection and reopen it before calling the MAX ID methods.
        public static int GetMaxLineItemID()
        {
            int LineItemID = -1;
            SqlConnection conn = GetConnection();
            SqlCommand comm = new SqlCommand("SELECT MAX(ID) FROM LineItems", conn);
            try
            {
                conn.Open();
               LineItemID = Convert.ToInt32(comm.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
               LineItemID = -1;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return LineItemID;
        }


        public static int GetMaxOrderID()
        {
            int orderID = -1;
            SqlConnection conn = GetConnection();
            SqlCommand comm = new SqlCommand("SELECT MAX(ID) FROM Orders", conn);
            try
            {
                conn.Open();
                orderID = Convert.ToInt32(comm.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                orderID = -1;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return orderID;
        }


        public static double GetProductPrice(string name)
        {
            double price = 0.0;
            SqlConnection conn = GetConnection();

            SqlCommand comm = new SqlCommand("SELECT * FROM Products WHERE Name = @Name", conn);
            comm.Parameters.AddWithValue("@Name", name);
            try
            {
                conn.Open();
                SqlDataReader dr = comm.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    price = Convert.ToDouble(dr["BasePrice"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                price = 0.0;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return price;
        }
      
     

        public static Toppings GetToppings(string productType)
        {
            int i = 0;
            Toppings tops = null;
            SqlConnection conn = GetConnection();
            SqlCommand comm = new SqlCommand("SELECT * FROM Toppings WHERE ProductType = @ProductType ORDER BY Name", conn);
            comm.Parameters.AddWithValue("@ProductType", productType);
            try
            {
                tops = new Toppings(6);
                conn.Open();
                SqlDataReader dr = comm.ExecuteReader(CommandBehavior.SingleResult);
                while (true)
                {
                    if (!dr.Read())
                    {
                        break;
                    }
                    tops.ToppingList[i] = false;

                    tops.ToppingNames[i] = (string)dr["Name"];
                    tops.ToppingPrices[i] = Convert.ToDouble(dr["Price"]);
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                tops = null;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return tops;
        }

        public static int InsertLineItem(int OrderID, Product product)
        {
            return InsertLineItem(OrderID, product.Name, product.Size, product.BasePrice, product.Price(), product.tops);
        }
            

        public static int InsertLineItem(int orderID, string productName, string productSize, double productBasePrice, double productTotalPrice, Toppings tops)
        {
            int id = -1;
            int lineItemID = -1;
            SqlConnection conn = GetConnection();
            SqlCommand comm = new SqlCommand("INSERT INTO LineItems (OrderID, ProductName, ProductSize, ProductBasePrice, ProductTotalPrice) VALUES (@OrderID, @ProductName, @ProductSize, @ProductBasePrice, @ProductTotalPrice)", conn);
            comm.Parameters.AddWithValue("@OrderID", orderID);
            comm.Parameters.AddWithValue("@ProductName", productName);
            comm.Parameters.AddWithValue("@ProductSize", productSize);
            comm.Parameters.AddWithValue("@ProductBasePrice", productBasePrice);
            comm.Parameters.AddWithValue("@ProductTotalPrice", productTotalPrice);
            try
            {
                conn.Open();
                id = comm.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                lineItemID = GetMaxLineItemID();
                if (tops != null)
                {
                    int i = 0;
                    while (true)
                    {
                        if (i >= tops.ToppingList.Length)
                        {
                            break;
                        }
                        if (tops.ToppingList[i])
                        {
                            InsertLineItemToppings(lineItemID, tops.ToppingNames[i], tops.ToppingPrices[i]);
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                id = -1;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return id;
        }

        public static int InsertLineItemToppings(int LineItemID, string Name, double Price)
        {
            //orderid
            //prodname
            //prodbaseprice
            //prod total
            // toppings obj
            int LineItemToppingID= 0;
            SqlConnection conn = GetConnection();
            SqlCommand comm = new SqlCommand("INSERT INTO LineItemToppings (LineItemID, Name, Price) VALUES (@LineItemID, @Name, @Price)", conn);
            comm.Parameters.AddWithValue("@LineItemID", LineItemID);
            comm.Parameters.AddWithValue("@Name", Name);
            comm.Parameters.AddWithValue("@Price", Price);
            try
            {
                conn.Open();
                LineItemToppingID = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                LineItemToppingID = 0;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return LineItemToppingID;
        }

        public static int InsertOrder(Order order)
        {
            return InsertOrder(order.Date, order.SubTotal(), order.SalesTax(), order.Total(), order);
        }
            

        public static int InsertOrder(DateTime date, double subTotal, double salesTax, double total, List<Product> order)
        {
          
            int orderID = -1;
            SqlConnection conn = GetConnection();
            SqlCommand comm = new SqlCommand("INSERT INTO Orders (Date, SubTotal, SalesTax, Total) VALUES (@Date, @SubTotal, @SalesTax, @Total)", conn);
            comm.Parameters.AddWithValue("@Date", date);
            comm.Parameters.AddWithValue("@SubTotal", subTotal);
            comm.Parameters.AddWithValue("@SalesTax", salesTax);
            comm.Parameters.AddWithValue("@Total", total);
            try
            {
                conn.Open();
                orderID = comm.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                orderID = GetMaxOrderID();
                foreach (Product product in order)
                {
                    InsertLineItem(orderID, product); // call insertlineitem to insert each line item
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                orderID = -1;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return orderID;
        }
    }
}

