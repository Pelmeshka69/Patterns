using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _28._05
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }
        public List<string> Items { get; set; }
        public string Status { get; set; }
    }

    public class OrderManager
    {
        private List<Order> orders = new List<Order>();

        public void PlaceOrder(Order order)
        {
            order.Status = "Placed";
            orders.Add(order);
            Console.WriteLine($"Order placed for customer: {order.CustomerName}");
        }

        public void UpdateOrderStatus(Order order, string newStatus)
        {
            var existingOrder = orders.FirstOrDefault(o => o.OrderId == order.OrderId);
            if (existingOrder != null)
            {
                existingOrder.Status = newStatus;
                Console.WriteLine($"Order status updated to: {newStatus}");
            }
        }

        public Order GetOrder(int orderId)
        {
            return orders.FirstOrDefault(o => o.OrderId == orderId);
        }
    }

    public class NotificationManager
    {
        public void SendNotification(Order order, string message)
        {
            Console.WriteLine($"Notification for Order ID: {order.OrderId}, Message: {message}");
        }
    }

    public class OrderManagerFacade
    {
        private OrderManager orderManager;
        private NotificationManager notificationManager;

        public OrderManagerFacade()
        {
            orderManager = new OrderManager();
            notificationManager = new NotificationManager();
        }

        public void PlaceOrder(Order order)
        {
            orderManager.PlaceOrder(order);
            notificationManager.SendNotification(order, "New order placed.");
        }

        public void UpdateOrderStatus(Order order, string newStatus)
        {
            orderManager.UpdateOrderStatus(order, newStatus);
            notificationManager.SendNotification(order, $"Order status changed to {newStatus}.");
        }

        public Order GetOrder(int orderId)
        {
            return orderManager.GetOrder(orderId);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            OrderManagerFacade facade = new OrderManagerFacade();
            Order order = new Order
            {
                OrderId = 1,
                CustomerName = "John Doe",
                DeliveryAddress = "123 Main St",
                Items = new List<string> { "Item1", "Item2" }
            };
            facade.PlaceOrder(order);
            facade.UpdateOrderStatus(order, "Shipped");
            Console.WriteLine($"Order Status: {facade.GetOrder(1).Status}");
        }
    }
}
