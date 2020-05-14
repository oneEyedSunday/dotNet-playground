using System;
using Microsoft.EntityFrameworkCore;

namespace CRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                Console.WriteLine("Listing Items in Virtual Store...");
                foreach(Product product in db.Products)
                {
                    Console.WriteLine($"Product: {product.Description} priced at ${product.Price:F2}");
                }
                foreach(OrderItem orderItem in db.OrderItems)
                {
                    Console.WriteLine($"Item: {orderItem.Quantity} pieces of {orderItem.Product.Description}");
                }
                foreach(Order order in db.Orders)
                {
                    Console.WriteLine($"Order: {order.Created}");
                }

                Console.WriteLine("Do you want to add a product?(y/N): ");
                char addProduct;
                addProduct =  Convert.ToChar(Console.Read());

                if (addProduct.ToString().ToLower().Equals("y"))
                {


                    CreateProduct(db);
                    CreateOrderItem(db);
                }
               

                Console.WriteLine("Disposing connection...");
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
    }
}
