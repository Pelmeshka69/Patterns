using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24._04._24
{
    public class Task
    {
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
    }

    public class Controller
    {
        private List<Task> tasks = new List<Task>();

        public void AddTask(string title, DateTime dueDate)
        {
            tasks.Add(new Task { Title = title, CreationDate = DateTime.Now, DueDate = dueDate, Status = "Не виконано" });
        }

        public void MarkTaskAsDone(string title)
        {
            var task = tasks.Find(t => t.Title == title);
            if (task != null)
                task.Status = "Виконано";
        }

        public void RemoveTask(string title)
        {
            tasks.RemoveAll(t => t.Title == title);
        }

        public void ViewTasks()
        {
            foreach (var task in tasks)
            {
                Console.WriteLine($"Назва: {task.Title}, Термін виконання: {task.DueDate}, Статус: {task.Status}");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Controller controller = new Controller();

            while (true)
            {
                Console.WriteLine("1. Додати завдання");
                Console.WriteLine("2. Відзначити завдання як виконане");
                Console.WriteLine("3. Видалити завдання");
                Console.WriteLine("4. Переглянути список завдань");
                Console.WriteLine("5. Вийти");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Write("Введіть назву завдання: ");
                        string title = Console.ReadLine();
                        Console.Write("Введіть термін виконання (формат: рррр-мм-дд): ");
                        DateTime dueDate = DateTime.Parse(Console.ReadLine());
                        controller.AddTask(title, dueDate);
                        break;
                    case "2":
                        Console.Write("Введіть назву завдання: ");
                        title = Console.ReadLine();
                        controller.MarkTaskAsDone(title);
                        break;
                    case "3":
                        Console.Write("Введіть назву завдання: ");
                        title = Console.ReadLine();
                        controller.RemoveTask(title);
                        break;
                    case "4":
                        controller.ViewTasks();
                        break;
                    case "5":
                        return;
                }
            }
        }
    }
}
