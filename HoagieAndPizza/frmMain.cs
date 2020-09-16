using System;
using System.Drawing;
using System.Windows.Forms;

//Hide the title bar ControlBox = false; FormBorderStyle = FormBorderStyle.None

//btnMyButton.ForeColor = Color.White; btnMyButton.Font = new Font(btnMyButton.Font.FontFamily, 30);
namespace HoagieAndPizza
{
    public partial class frmMain : Form
    {


        int x;
        int y;

        string dir = @"..\..\";

        Size totalSize;
        Point totalPosition;
        TextBox txtReceipt;
        Size btnSize;
        Point[] btnLocation = new Point[9];
       
        int level = 0;

        private Product product;
        private Order order;
        private Toppings HoagieToppings;
        private Toppings PizzaToppings;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            level = 0;
            WindowState = FormWindowState.Maximized;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            Image image = Image.FromFile(dir + "Images/deli1.jpg", true);
            BackgroundImage = image;
            BackgroundImageLayout = ImageLayout.Stretch;
            x = Size.Width;
            y = Size.Height;
            btnSize = new Size((int) (x / 6.5), (int) (y / 6.5));
            btnLocation[0] = new Point((int) ((x / 6.5) * 0.25), (int) (y / 6.5));
            btnLocation[1] = new Point((int) ((x / 6.5) * 1.5), (int) (y / 6.5));
            btnLocation[2] = new Point((int) ((x / 6.5) * 2.75), (int) (y / 6.5));
            btnLocation[3] = new Point((int) ((x / 6.5) * 0.25), (int) ((y / 6.5) * 2.5));
            btnLocation[4] = new Point((int) ((x / 6.5) * 1.5), (int) ((y / 6.5) * 2.5));
            btnLocation[5] = new Point((int) ((x / 6.5) * 2.75), (int) ((y / 6.5) * 2.5));
            btnLocation[6] = new Point((int) ((x / 6.5) * 0.25), (int) ((y / 6.5) * 5.0));
            btnLocation[7] = new Point((int) ((x / 6.5) * 1.5), (int) ((y / 6.5) * 5.0));
            btnLocation[8] = new Point((int) ((x / 6.5) * 2.75), (int) ((y / 6.5) * 5.0));
            totalSize = new Size((int) (((2.5 * x) / 6.5) - 25.0), y - 50);
            totalPosition = new Point((int) ((x / 6.5) * 4.0), 25);
            order = new Order();
            txtReceipt = new TextBox();
            txtReceipt.Font = new Font(txtReceipt.Font.FontFamily, 12f);
            HoagieToppings = ProductDB.GetToppings("Hoagie");
            PizzaToppings = ProductDB.GetToppings("Pizza");
            DrawInitialForm();
        }

        // buttons : 

        public void btnAddToOrder_Click(object sender, EventArgs e)
        {
            order.SetDate();
            product.BasePrice = ProductDB.GetProductPrice(product.Name);
            order.Add(product);
            txtReceipt.Text = order.ToString();
            DrawInitialForm();
        }


        public void btnAmericanHoagie_Click(object sender, EventArgs e)
        {
            product.Name = "American Hoagie";
            Controls.Clear();
            DrawSizes();
        }

        public void btnBack_Click(object sender, EventArgs e)
        {
            if (level == 1)
            {
                Controls.Clear();
                DrawInitialForm();
            }
            else if (level == 3)
            {
                for (int i = 0; i < product.tops.ToppingList.Length; i++)
                {
                    product.tops.ToppingList[i] = false;
                }
                DrawSizes();
            }
            else
            {
                Controls.Clear();
                if (product.Type == "Hoagie")
                {
                    DrawHoagie();
                }
                else if (product.Type == "Pizza")
                {
                    DrawPizza();
                }
                else if (product.Type == "Fries")
                {
                    DrawFries();
                }
                else if (product.Type == "Soda")
                {
                    DrawSoda();
                }
            }
        }

        public void btnBacon_Click(object sender, EventArgs e)
        {
            Button b= (Button)sender;
            b.ForeColor = Color.Red;
            b.Font = new Font(b.Font.FontFamily, 70f);
            if (product.tops.ToppingList[0])
            {
                product.tops.ToppingList[0] = false;
                b.Text = "";
            }
            else
            {
                product.tops.ToppingList[0] = true;
                b.Text = "X";
            }
        }

        public void btnCancel_Click(object sender, EventArgs e)
        {
            product = null;
            DrawInitialForm();
        }
        //1. Set the Product to null 
       // 2. Clear the Order 
           // 3. Set the TextBox to empty string
          // 4. 
        //Draw the Initial For
        public void btnCancelOrder_Click(object sender, EventArgs e)
        {
            product = null;
            order.Clear();
            txtReceipt.Text = "";
            DrawInitialForm();
        }

        public void btnCheeseFries_Click(object sender, EventArgs e)
        {
            product.Name = "Cheese Fries";
            Controls.Clear();
            DrawSizes();
        }

        public void btnCheesePizza_Click(object sender, EventArgs e)
        {
            product.Name = "Cheese Pizza";
            Controls.Clear();
            DrawSizes();
        }
        public void btnCola_Click(object sender, EventArgs e)
        {
            product.Name = "Cola";
            Controls.Clear();
            DrawSizes();
        }
        public void btnDeluxePizza_Click(object sender, EventArgs e)
        {
            product.Name = "Deluxe Pizza";
            Controls.Clear();
            DrawSizes();
        }
        public void btnDietCola_Click(object sender, EventArgs e)
        {
            product.Name = "Diet Cola";
            Controls.Clear();
            DrawSizes();
        }

        public void btnExtraCheese_Click(object sender, EventArgs e)
        {
            Button b= (Button)sender;
            b.ForeColor = Color.Red;
            b.Font = new Font(b.Font.FontFamily, 70f);
            if (product.tops.ToppingList[1])
            {
                product.tops.ToppingList[1] = false;
                b.Text = "";
            }
            else
            {
                product.tops.ToppingList[1] = true;
                b.Text = "X";
            }
        }
        public void btnFries_Click(object sender, EventArgs e)
        {
            product = new Product();
            product.Type = "Fries";
            Controls.Clear();
            DrawFries();
        }
        public void btnHoagie_Click(object sender, EventArgs e)
        {
            product = new Product(HoagieToppings);
            product.Type = "Hoagie";
            Controls.Clear();
            DrawHoagie();
        }

        public void btnHotPeppers_Click(object sender, EventArgs e)
        {
            Button b= (Button)sender;
            b.ForeColor = Color.Red;
            b.Font = new Font(b.Font.FontFamily, 70f);
            if (product.tops.ToppingList[0])
            {
                product.tops.ToppingList[0] = false;
                b.Text = "";
            }
            else
            {
                product.tops.ToppingList[0] = true;
                b.Text = "X";
            }
        }

        public void btnItalianHoagie_Click(object sender, EventArgs e)
        {
            product.Name = "Italian Hoagie";
            Controls.Clear();
            DrawSizes();
        }


        public void btnLarge_Click(object sender, EventArgs e)
        {
            product.Size = "Large";
            if ((product.Type != "Fries") && (product.Type != "Soda"))
            {
                DrawToppings();
            }
            else
            {
                Controls.Clear();
                DrawBottom(1);
                DrawReceipt();
            }
        }


        public void btnLimon_Click(object sender, EventArgs e)
        {
            product.Name = "Limon";
            Controls.Clear();
            DrawSizes();
        }

        public void btnMayo_Click(object sender, EventArgs e)
        {
            Button b= (Button)sender;
            b.ForeColor = Color.Red;
            b.Font = new Font(b.Font.FontFamily, 70f);
            if (product.tops.ToppingList[1])
            {
                product.tops.ToppingList[1] = false;
                b.Text = "";
            }
            else
            {
                product.tops.ToppingList[1] = true;
                b.Text = "X";
            }
        }


        public void btnMedium_Click(object sender, EventArgs e)
        {
            product.Size = "Medium";
            if ((product.Type != "Fries") && (product.Type != "Soda"))
            {
                DrawToppings();
            }
            else
            {
                Controls.Clear();
                DrawBottom(1);
                DrawReceipt();
            }
        }


        public void btnMushrooms_Click(object sender, EventArgs e)
        {
            Button b= (Button)sender;
            b.ForeColor = Color.Red;
            b.Font = new Font(b.Font.FontFamily, 70f);
            if (product.tops.ToppingList[2])
            {
                product.tops.ToppingList[2] = false;
                b.Text = "";
            }
            else
            {
                product.tops.ToppingList[2] = true;
                b.Text = "X";
            }
        }




        public void btnOil_Click(object sender, EventArgs e)
        {
            Button b= (Button)sender;
            b.ForeColor = Color.Red;
            b.Font = new Font(b.Font.FontFamily, 70f);
            if (product.tops.ToppingList[2])
            {
                product.tops.ToppingList[2] = false;
                b.Text = "";
            }
            else
            {
                product.tops.ToppingList[2] = true;
                b.Text = "X";
            }
        }

        public void btnOnion_Click(object sender, EventArgs e)
        {
            Button b= (Button)sender;
            b.ForeColor = Color.Red;
            b.Font = new Font(b.Font.FontFamily, 70f);
            if (product.Type == "Hoagie")
            {
                if (product.tops.ToppingList[3])
                {
                    product.tops.ToppingList[3] = false;
                    b.Text = "";
                }
                else
                {
                    product.tops.ToppingList[3] = true;
                    b.Text = "X";
                }
            }
            else if (product.Type == "Pizza")
            {
                if (product.tops.ToppingList[3])
                {
                    product.tops.ToppingList[3] = false;
                    b.Text = "";
                }
                else
                {
                    product.tops.ToppingList[3] = true;
                    b.Text = "X";
                }
            }
        }

        public void btnOregano_Click(object sender, EventArgs e)
        {
            Button b= (Button)sender;
            b.ForeColor = Color.Red;
            b.Font = new Font(b.Font.FontFamily, 70f);
            if (product.tops.ToppingList[4])
            {
                product.tops.ToppingList[4] = false;
                b.Text = "";
            }
            else
            {
                product.tops.ToppingList[4] = true;
                b.Text = "X";
            }
        }

        public void btnPepperoniPizza_Click(object sender, EventArgs e)
        {
            product.Name = "Pepperoni Pizza";
            Controls.Clear();
            DrawSizes();
        }

        public void btnPeppers_Click(object sender, EventArgs e)
        {
            Button b= (Button)sender;
            b.ForeColor = Color.Red;
            b.Font = new Font(b.Font.FontFamily, 70f);
            if (product.tops.ToppingList[4])
            {
                product.tops.ToppingList[4] = false;
                b.Text = "";
            }
            else
            {
                product.tops.ToppingList[4] = true;
                b.Text = "X";
            }
        }


        public void btnPizza_Click(object sender, EventArgs e)
        {
            product = new Product(PizzaToppings);
            product.Type = "Pizza";
            Controls.Clear();
            DrawPizza();
        }


        public void btnPizzaFries_Click(object sender, EventArgs e)
        {
            product.Name = "Pizza Fries";
            Controls.Clear();
            DrawSizes();
        }

        public void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            order.SetDate();
            product = null;
            order.Clear();
            txtReceipt.Text = "";
            DrawInitialForm();
             ProductDB.InsertOrder(order);
        }

        public void btnPlainFries_Click(object sender, EventArgs e)
        {
            product.Name = "Plain Fries";
            Controls.Clear();
            DrawSizes();
        }

        public void btnSausage_Click(object sender, EventArgs e)
        {
            Button b= (Button)sender;
            b.ForeColor = Color.Red;
            b.Font = new Font(b.Font.FontFamily, 70f);
            if (product.tops.ToppingList[5])
            {
                product.tops.ToppingList[5] = false;
                b.Text = "";
            }
            else
            {
                product.tops.ToppingList[5] = true;
                b.Text = "X";
            }
        }



        public void btnSmall_Click(object sender, EventArgs e)
        {
            product.Size = "Small";
            if ((product.Type != "Fries") && (product.Type != "Soda"))
            {
                DrawToppings();
            }
            else
            {
                Controls.Clear();
                DrawBottom(1);
                DrawReceipt();
            }
        }


        public void btnSoda_Click(object sender, EventArgs e)
        {
            product = new Product();
            product.Type = "Soda";
            Controls.Clear();
            DrawSoda();
        }

        public void btnSweetPeppers_Click(object sender, EventArgs e)
        {
            Button b= (Button)sender;
            b.ForeColor = Color.Red;
            b.Font = new Font(b.Font.FontFamily, 70f);
            if (product.tops.ToppingList[5])
            {
                product.tops.ToppingList[5] = false;
                b.Text = "";
            }
            else
            {
                product.tops.ToppingList[5] = true;
                b.Text = "X";
            }
        }


        public void btnTunaHoagie_Click(object sender, EventArgs e)
        {
            product.Name = "Tuna Hoagie";
            Controls.Clear();
            DrawSizes();
        }






        // Draws 

        public void DrawBottom(int mode)
        {
            Button b= new Button();
            b.Text = "";
            b.Size = btnSize;
            b.Location = btnLocation[6];
            b.BackgroundImage = Image.FromFile(dir + "Images/back.jpg", true);
            b.BackgroundImageLayout = ImageLayout.Stretch;
            b.Click += new EventHandler(btnBack_Click);
            Controls.Add(b);

            Button b2 = new Button();
            b2.Text = "";
            b2.Size = btnSize;
            b2.Location = btnLocation[7];
            b2.BackgroundImage = Image.FromFile(dir + "Images/cancel.jpg", true);
            b2.BackgroundImageLayout = ImageLayout.Stretch;
            b2.Click += new EventHandler(btnCancel_Click);
            Controls.Add(b2);

            if (mode == 1)
            {
                Button b3 = new Button();
                b3.Text = "";
                b3.Size = btnSize;
                b3.Location = btnLocation[8];
                b3.BackgroundImage = Image.FromFile(dir + "Images/add.jpg", true);
                b3.BackgroundImageLayout = ImageLayout.Stretch;
                b3.Click += new EventHandler(btnAddToOrder_Click);
                Controls.Add(b3);
            }
        }

        public void DrawFries()
        {
            level = 1;
            Controls.Clear();
            Button b= new Button();
            b.Text = "plain fries";
            b.ForeColor = Color.White;
            b.Font = new Font(b.Font.FontFamily, 30f);
            b.Size = btnSize;
            b.Location = btnLocation[0];
            b.BackgroundImage = Image.FromFile(dir + "Images/fries.jpg", true);
            b.BackgroundImageLayout = ImageLayout.Stretch;
            b.Click += new EventHandler(btnPlainFries_Click);
            Controls.Add(b);

            Button b2 = new Button();
            b2.Text = "cheese fries";
            b2.ForeColor = Color.White;
            b2.Font = new Font(b2.Font.FontFamily, 30f);
            b2.Size = btnSize;
            b2.Location = btnLocation[1];
            b2.BackgroundImage = Image.FromFile(dir + "Images/fries.jpg", true);
            b2.BackgroundImageLayout = ImageLayout.Stretch;
            b2.Click += new EventHandler(btnCheeseFries_Click);
            Controls.Add(b2);
            Button b3 = new Button();
            b3.Text = "pizza fries";
            b3.ForeColor = Color.White;
            b3.Font = new Font(b3.Font.FontFamily, 30f);
            b3.Size = btnSize;
            b3.Location = btnLocation[2];
            b3.BackgroundImage = Image.FromFile(dir + "Images/fries.jpg", true);
            b3.BackgroundImageLayout = ImageLayout.Stretch;
            b3.Click += new EventHandler(btnPizzaFries_Click);
            Controls.Add(b3);
            DrawReceipt();
            DrawBottom(0);
        }

        public void DrawHoagie()
        {
            level = 1;
            Controls.Clear();
            Button b= new Button();
            b.Text = "american hoagie";
            b.ForeColor = Color.White;
            b.Font = new Font(b.Font.FontFamily, 30f);
            b.Size = btnSize;
            b.Location = btnLocation[0];
            b.BackgroundImage = Image.FromFile(dir + "Images/hoagie.jpg", true);
            b.BackgroundImageLayout = ImageLayout.Stretch;
            b.Click += new EventHandler(btnAmericanHoagie_Click);
            Controls.Add(b);

            Button b2 = new Button();
            b2.Text = "italian hoagie";
            b2.ForeColor = Color.White;
            b2.Font = new Font(b2.Font.FontFamily, 30f);
            b2.Size = btnSize;
            b2.Location = btnLocation[1];
            b2.BackgroundImage = Image.FromFile(dir + "Images/hoagie.jpg", true);
            b2.BackgroundImageLayout = ImageLayout.Stretch;
            b2.Click += new EventHandler(btnItalianHoagie_Click);
            Controls.Add(b2);

            Button b3 = new Button();
            b3.Text = "tuna hoagie";
            b3.ForeColor = Color.White;
            b3.Font = new Font(b3.Font.FontFamily, 30f);
            b3.Size = btnSize;
            b3.Location = btnLocation[2];
            b3.BackgroundImage = Image.FromFile(dir + "Images/hoagie.jpg", true);
            b3.BackgroundImageLayout = ImageLayout.Stretch;
            b3.Click += new EventHandler(btnTunaHoagie_Click);
            Controls.Add(b3);
            DrawReceipt();
            DrawBottom(0);
        }





     

        public void DrawInitialForm()
        {
            level = 0;
            Controls.Clear();
            Button b= new Button();
            b.Text = "hoagiee";
            b.ForeColor = Color.White;
            Font = new Font(b.Font.FontFamily, 30f);
            b.Size = btnSize;
            b.Location = btnLocation[0];
            b.BackgroundImage = Image.FromFile(dir + "Images/hoagie.jpg", true);
            b.BackgroundImageLayout = ImageLayout.Stretch;
            b.Click += new EventHandler(btnHoagie_Click);
            Controls.Add(b);

            Button b2 = new Button();
            b2.Text = "pizza";
            b2.ForeColor = Color.White;
            b2.Font = new Font(b2.Font.FontFamily, 30f);
            b2.Size = btnSize;
            b2.Location = btnLocation[1];
            b2.BackgroundImage = Image.FromFile(dir + "Images/pizza.jpg", true);
            b2.BackgroundImageLayout = ImageLayout.Stretch;
            b2.Click += new EventHandler(btnPizza_Click);
            Controls.Add(b2);

            Button b3 = new Button();
            b3.Text = "fries";
            b3.ForeColor = Color.White;
            b3.Font = new Font(b3.Font.FontFamily, 30f);
            b3.Size = btnSize;
            b3.Location = btnLocation[2];
            b3.BackgroundImage = Image.FromFile(dir + "Images/fries.jpg", true);
            b3.BackgroundImageLayout = ImageLayout.Stretch;
            b3.Click += new EventHandler(btnFries_Click);
            Controls.Add(b3);

            Button b4 = new Button();
            b4.Text = "soda";
            b4.ForeColor = Color.White;
            b4.Font = new Font(b4.Font.FontFamily, 30f);
            b4.Size = btnSize;
            b4.Location = btnLocation[4];
            b4.BackgroundImage = Image.FromFile(dir + "Images/soda.jpg", true);
            b4.BackgroundImageLayout = ImageLayout.Stretch;
            DrawReceipt();
            if (order.Count > 0)
            {
                DrawPlaceOrder();
            }
            b4.Click += new EventHandler(btnSoda_Click);
            Controls.Add(b4);
           
        }

        public void DrawPizza()
        {
            level = 1;
            Controls.Clear();
            Button b= new Button();
            b.Text = "cheese pizza";
            b.ForeColor = Color.White;
            b.Font = new Font(b.Font.FontFamily, 30f);
            b.Size = btnSize;
            b.Location = btnLocation[0];
            b.BackgroundImage = Image.FromFile(dir + "Images/pizza.jpg", true);
            b.BackgroundImageLayout = ImageLayout.Stretch;
            b.Click += new EventHandler(btnCheesePizza_Click);
            Controls.Add(b);

            Button b2 = new Button();
            b2.Text = "pepperoni pizza";
            b2.ForeColor = Color.White;
            b2.Font = new Font(b2.Font.FontFamily, 30f);
            b2.Size = btnSize;
            b2.Location = btnLocation[1];
            b2.BackgroundImage = Image.FromFile(dir + "Images/pizza.jpg", true);
            b2.BackgroundImageLayout = ImageLayout.Stretch;
            b2.Click += new EventHandler(btnPepperoniPizza_Click);
            Controls.Add(b2);

            Button b3 = new Button();
            b3.Text = "deluxe pizza";
            b3.ForeColor = Color.White;
            b3.Font = new Font(b3.Font.FontFamily, 30f);
            b3.Size = btnSize;
            b3.Location = btnLocation[2];
            b3.BackgroundImage = Image.FromFile(dir + "Images/pizza.jpg", true);
            b3.BackgroundImageLayout = ImageLayout.Stretch;
            b3.Click += new EventHandler(btnDeluxePizza_Click);
            Controls.Add(b3);
            DrawReceipt();
            DrawBottom(0);
        }






        public void DrawPlaceOrder()
        {
            Button b= new Button();
            b.Text = "";
            b.Size = btnSize;
            b.Location = btnLocation[7];
            b.BackgroundImage = Image.FromFile(dir + "Images/cancel.jpg", true);
            b.BackgroundImageLayout = ImageLayout.Stretch;
            b.Click += new EventHandler(btnCancelOrder_Click);
            Controls.Add(b);

            Button b2 = new Button();
            b2.Text = "";
            b2.Size = btnSize;
            b2.Location = btnLocation[8];
            b2.BackgroundImage = Image.FromFile(dir + "Images/placeOrder.jpg", true);
            b2.BackgroundImageLayout = ImageLayout.Stretch;
            b2.Click += new EventHandler(btnPlaceOrder_Click);
            Controls.Add(b2);
        }


        public void DrawReceipt()
        {
            txtReceipt.Multiline = true;
            txtReceipt.Size = totalSize;
            txtReceipt.Location = totalPosition;
            Controls.Add(txtReceipt);
        }

     
        public void DrawSizes()
        {
            level = 2;
            Controls.Clear();
            Button b= new Button();
            b.Text = "";
            b.Size = btnSize;
            b.Location = btnLocation[0];
            b.BackgroundImage = Image.FromFile(dir + "Images/small.jpg", true);
            b.BackgroundImageLayout = ImageLayout.Stretch;
            b.Click += new EventHandler(btnSmall_Click);
            Controls.Add(b);

            Button b2 = new Button();
            b2.Text = "";
            b2.Size = btnSize;
            b2.Location = btnLocation[1];
            b2.BackgroundImage = Image.FromFile(dir + "Images/medium.jpg", true);
            b2.BackgroundImageLayout = ImageLayout.Stretch;
            b2.Click += new EventHandler(btnMedium_Click);
            Controls.Add(b2);

            Button b3 = new Button();
            b3.Text = "";
            b3.Size = btnSize;
            b3.Location = btnLocation[2];
            b3.BackgroundImage = Image.FromFile(dir + "Images/large.jpg", true);
            b3.BackgroundImageLayout = ImageLayout.Stretch;
            b3.Click += new EventHandler(btnLarge_Click);
            Controls.Add(b3);
            DrawReceipt();
            DrawBottom(0);
        }
        public void DrawSoda()
        {
            level = 1;
            Controls.Clear();
            Button b= new Button();
            b.Text = "cola";
            b.ForeColor = Color.White;
            b.Font = new Font(b.Font.FontFamily, 30f);
            b.Size = btnSize;
            b.Location = btnLocation[0];
            b.BackgroundImage = Image.FromFile(dir + "Images/soda.jpg", true);
            b.BackgroundImageLayout = ImageLayout.Stretch;
            b.Click += new EventHandler(btnCola_Click);
            Controls.Add(b);

            Button b2 = new Button();
            b2.Text = "diet cola";
            b2.ForeColor = Color.White;
            b2.Font = new Font(b2.Font.FontFamily, 30f);
            b2.Size = btnSize;
            b2.Location = btnLocation[1];
            b2.BackgroundImage = Image.FromFile(dir + "Images/soda.jpg", true);
            b2.BackgroundImageLayout = ImageLayout.Stretch;
            b2.Click += new EventHandler(btnDietCola_Click);
            Controls.Add(b2);

            Button b3 = new Button();
            b3.Text = "limon";
            b3.ForeColor = Color.White;
            b3.Font = new Font(b3.Font.FontFamily, 30f);
            b3.Size = btnSize;
            b3.Location = btnLocation[2];
            b3.BackgroundImage = Image.FromFile(dir + "Images/soda.jpg", true);
            b3.BackgroundImageLayout = ImageLayout.Stretch;
            b3.Click += new EventHandler(btnLimon_Click);
            Controls.Add(b3);
            DrawReceipt();
            DrawBottom(0);
        }

        public void DrawToppings()
        {
            level = 3;
            Controls.Clear();
            if (product.Type == "Hoagie")
            {
                Button b= new Button();
                b.Text = "";
                b.Size = btnSize;
                b.Location = btnLocation[0];
                b.BackgroundImage = Image.FromFile(dir + "Images/mayo.jpg", true);
                b.BackgroundImageLayout = ImageLayout.Stretch;
                b.Click += new EventHandler(btnMayo_Click);
                Controls.Add(b);

                Button b2 = new Button();
                b2.Text = "";
                b2.Size = btnSize;
                b2.Location = btnLocation[1];
                b2.BackgroundImage = Image.FromFile(dir + "Images/oil.jpg", true);
                b2.BackgroundImageLayout = ImageLayout.Stretch;
                b2.Click += new EventHandler(btnOil_Click);
                Controls.Add(b2);

                Button b3 = new Button();
                b3.Text = "";
                b3.Size = btnSize;
                b3.Location = btnLocation[2];
                b3.BackgroundImage = Image.FromFile(dir + "Images/onions.jpg", true);
                b3.BackgroundImageLayout = ImageLayout.Stretch;
                b3.Click += new EventHandler(btnOnion_Click);
                Controls.Add(b3);

                Button b4 = new Button();
                b4.Text = "";
                b4.Size = btnSize;
                b4.Location = btnLocation[3];
                b4.BackgroundImage = Image.FromFile(dir + "Images/hotPeppers.jpg", true);
                b4.BackgroundImageLayout = ImageLayout.Stretch;
                b4.Click += new EventHandler(btnHotPeppers_Click);
                Controls.Add(b4);

                Button button5 = new Button();
                button5.Text = "";
                button5.Size = btnSize;
                button5.Location = btnLocation[4];
                button5.BackgroundImage = Image.FromFile(dir + "Images/peppers.jpg", true);
                button5.BackgroundImageLayout = ImageLayout.Stretch;
                button5.Click += new EventHandler(btnSweetPeppers_Click);
                Controls.Add(button5);

                Button button6 = new Button();
                button6.Text = "";
                button6.Size = btnSize;
                button6.Location = btnLocation[5];
                button6.BackgroundImage = Image.FromFile(dir + "Images/oregano.jpg", true);
                button6.BackgroundImageLayout = ImageLayout.Stretch;
                button6.Click += new EventHandler(btnOregano_Click);
                Controls.Add(button6);
            }
            else if (product.Type == "Pizza")
            {
                Button button7 = new Button();
                button7.Text = "";
                button7.Size = btnSize;
                button7.Location = btnLocation[0];
                button7.BackgroundImage = Image.FromFile(dir + "Images/sausage.jpg", true);
                button7.BackgroundImageLayout = ImageLayout.Stretch;
                button7.Click += new EventHandler(btnSausage_Click);
                Controls.Add(button7);

                Button button8 = new Button();
                button8.Text = "";
                button8.Size = btnSize;
                button8.Location = btnLocation[1];
                button8.BackgroundImage = Image.FromFile(dir + "Images/bacon.jpg", true);
                button8.BackgroundImageLayout = ImageLayout.Stretch;
                button8.Click += new EventHandler(btnBacon_Click);
                Controls.Add(button8);

                Button button9 = new Button();
                button9.Text = "";
                button9.Size = btnSize;
                button9.Location = btnLocation[2];
                button9.BackgroundImage = Image.FromFile(dir + "Images/mushrooms.jpg", true);
                button9.BackgroundImageLayout = ImageLayout.Stretch;
                button9.Click += new EventHandler(btnMushrooms_Click);
                Controls.Add(button9);

                Button button10 = new Button();
                button10.Text = "";
                button10.Size = btnSize;
                button10.Location = btnLocation[3];
                button10.BackgroundImage = Image.FromFile(dir + "Images/onions.jpg", true);
                button10.BackgroundImageLayout = ImageLayout.Stretch;
                button10.Click += new EventHandler(btnOnion_Click);
                Controls.Add(button10);

                Button button11 = new Button();
                button11.Text = "";
                button11.Size = btnSize;
                button11.Location = btnLocation[4];
                button11.BackgroundImage = Image.FromFile(dir + "Images/peppers.jpg", true);
                button11.BackgroundImageLayout = ImageLayout.Stretch;
                button11.Click += new EventHandler(btnPeppers_Click);
                Controls.Add(button11);

                Button button12 = new Button();
                button12.Text = "";
                button12.Size = btnSize;
                button12.Location = btnLocation[5];
                button12.BackgroundImage = Image.FromFile(dir + "Images/cheese.jpg", true);
                button12.BackgroundImageLayout = ImageLayout.Stretch;
                button12.Click += new EventHandler(btnExtraCheese_Click);
                Controls.Add(button12);
            }
            DrawReceipt();
            DrawBottom(1);
        }

  

    }
}
