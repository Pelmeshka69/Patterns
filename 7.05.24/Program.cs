using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._05._24
{
    public class MenuItem
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }

    public class Menu
    {
        private List<MenuItem> menuItems = new List<MenuItem>();

        public void AddMenuItem(MenuItem item)
        {
            menuItems.Add(item);
        }

        public void RemoveMenuItem(MenuItem item)
        {
            menuItems.Remove(item);
        }

        public MenuItem GetMenuItem(string name)
        {
            return menuItems.FirstOrDefault(item => item.Name == name);
        }
    }

    public class Order
    {
        public List<MenuItem> Items { get; set; } = new List<MenuItem>();
        public OrderStatus Status { get; set; }

        public void AddItem(MenuItem item)
        {
            Items.Add(item);
        }

        public void AddIngredient(MenuItem item, string ingredient)
        {
        }
    }

    public class OrderStatus
    {
        public const string Pending = "Очікується";
        public const string Preparing = "Готується";
        public const string Ready = "Готове";
        public const string Delivering = "Доставляється";
        public const string Completed = "Завершено";
    }

    public class OrderStatusNotifier
    {
        public void Notify(Order order)
        {
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.AddMenuItem(new MenuItem { Name = "Піца", Price = 100, Description = "Смачна піца" });
            Order order = new Order();
            order.AddItem(menu.GetMenuItem("Піца"));
            order.AddIngredient(menu.GetMenuItem("Піца"), "Сир");
            OrderStatusNotifier notifier = new OrderStatusNotifier();
            notifier.Notify(order);
        }
    }
}
