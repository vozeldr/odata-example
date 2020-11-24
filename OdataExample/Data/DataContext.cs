using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OdataExample.Data.Models;

namespace OdataExample.Data
{
    public sealed class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer
                {
                    CustomerId = Guid.Parse("D066AFA6-1F64-4EBC-9EBD-71CE77C86171"),
                    Name = "Bob Smith",
                    Orders = new List<Order>
                    {
                        new Order
                        {
                            OrderId = Guid.Parse("690114E8-1107-492A-BEDD-19F3137757D2"),
                            Name = "Order One",
                            CustomerId = Guid.Parse("D066AFA6-1F64-4EBC-9EBD-71CE77C86171"),
                            OrderItems = new List<OrderItem>
                            {
                                new OrderItem
                                {
                                    OrderItemId = Guid.NewGuid(),
                                    Product = "Product 1",
                                    OrderId = Guid.Parse("690114E8-1107-492A-BEDD-19F3137757D2")
                                },
                                new OrderItem
                                {
                                    OrderItemId = Guid.NewGuid(),
                                    Product = "Product 2",
                                    OrderId = Guid.Parse("690114E8-1107-492A-BEDD-19F3137757D2")
                                }
                            }
                        },
                        new Order
                        {
                            OrderId = Guid.Parse("25E82EC3-F544-4F9D-B102-31C9EBBB2C60"),
                            Name = "Order Two",
                            CustomerId = Guid.Parse("D066AFA6-1F64-4EBC-9EBD-71CE77C86171"),
                            OrderItems = new List<OrderItem>
                            {
                                new OrderItem
                                {
                                    OrderItemId = Guid.NewGuid(),
                                    Product = "Product 1",
                                    OrderId = Guid.Parse("25E82EC3-F544-4F9D-B102-31C9EBBB2C60")
                                },
                                new OrderItem
                                {
                                    OrderItemId = Guid.NewGuid(),
                                    Product = "Product 2",
                                    OrderId = Guid.Parse("25E82EC3-F544-4F9D-B102-31C9EBBB2C60")
                                }
                            }
                        }
                    }
                },
                new Customer
                {
                    CustomerId = Guid.Parse("CD5C3E08-76B9-4B51-B913-253CC16EF37C"), Name = "Jane Dough"
                }
            };

            Customers.AddRange(customers);
            Orders.AddRange(customers.SelectMany(c => c.Orders));
            OrderItems.AddRange(customers.SelectMany(customer => customer.Orders)
                .SelectMany(order => order.OrderItems));
            SaveChanges();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
