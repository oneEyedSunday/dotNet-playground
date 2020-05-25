using System;
using System.Linq;
using System.Collections.Generic;

namespace CRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                GetAndRunOperations(db);

                Console.WriteLine("Disposing connection...");
            }
        }

        static char GetSelection()
        {
            Console.WriteLine("What do you want to do: ");
            Console.WriteLine(@"
            1) List Products
            2) List Orders
            3) Add Product
            4) Add Order
            5) Exit
            ");
            char selection = Convert.ToChar(Console.Read());
            Console.ReadLine();

            return selection;
        }

        static void GetAndRunOperations(DatabaseContext db)
        {
            while (true)
            {
                switch (GetSelection())
                {
                    case '1':
                        ListProducts(db);
                        break;

                    case '2':
                        ListOrders(db);
                        break;

                    case '3':
                        CreateProduct(db);
                        break;

                    default:
                        QuitApplication();
                        return;
                }
            }
        }


        static void ListProducts(DatabaseContext db)
        {
            if (db.Products.Count() == 0)
            {
                Console.WriteLine("[+] No Products found");
                return;
            }
            Console.WriteLine("Listing Products in Virtual Store...");
            foreach (Product product in db.Products)
            {
                Console.WriteLine($"[+] Product: {product.Description} priced at ${product.Price:F2}");
            }
        }

        static void ListOrders(DatabaseContext db)
        {
            List<Order> orders = db.Orders.ToList<Order>();
            if (orders.Count == 0)
            {
                Console.WriteLine("[+] No Orders found");
                return;
            }
            Console.WriteLine("Listing Orders in Virtual Store...");
            foreach (Order order in orders)
            {
                Console.WriteLine("[+] Order {0} has {1} items", order.OrderId, order.Items.Count);
                if (order.Items.Count == 0)
                {
                    return;
                }
                Console.WriteLine("Listing them...");
                foreach (OrderItem item in order.Items)
                {
                    Console.WriteLine("[+] {0} pieces of {1}", item.Quantity, item.Product.Description);
                }
            }
        }


        static void CreateProduct(DatabaseContext db)
        {
            Console.WriteLine("You chose to Add a product");
            string description;
            Console.ReadLine();
            Console.WriteLine("Enter the product description: ");
            description = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter the product price: ");
            Double.TryParse(Console.ReadLine(), out double price);
            db.Products.Add(new Product { Price = price, Description = description });
            db.SaveChanges();
            Console.WriteLine("Successfully added product");
        }

        static void CreateOrderItem(DatabaseContext db)
        {
            Console.WriteLine("Creating Order Item");
            Console.ReadLine();
            Console.WriteLine("Provide a Product Id: ");
            int.TryParse(Console.ReadLine(), out int productId);
            Product _product = db.Products.Find(productId);
            if (_product != null)
            {
                Console.WriteLine("Adding order item to product with id {0} and description {1}", _product.ProductId, _product.Description);
                Console.WriteLine("Enter the quantity of {0} you want", _product.Description);
                int.TryParse(Console.ReadLine(), out int quantity);
                OrderItem _item = new OrderItem
                {
                    Quantity = quantity,
                    Product = _product
                };

                Console.WriteLine("Item of quantity {0} and product id {1}", _item.Quantity, _item.Product.ProductId);
                db.OrderItems.Add(_item);

                db.SaveChanges();
            } else
            {
                Console.WriteLine("Failed to find product with id {0}", productId);
                return;
            }
            
        }

        static void QuitApplication()
        {
            Console.WriteLine("Closing Application...");
            Environment.Exit(1);
        }
    }
}
