using Loading.Framework29._01.Basic_class;
using Microsoft.EntityFrameworkCore;

namespace Loading.Framework29._01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                if (db.Database.CanConnect())
                {
                    Console.WriteLine("База подключена");
                }
                else
                {
                    Console.WriteLine("Не судьба");
                }

                var stationeries = new List<Stationery> {
                    new Stationery { Name = "Ручка", Type = "Письменные принадлежности", Num = 100, Price = 12.5m },
                    new Stationery { Name = "Карандаш", Type = "Письменные принадлежности", Num = 200, Price = 8.0m },
                    new Stationery { Name = "Линейка", Type = "Измерительные инструменты", Num = 50, Price = 20.0m },
                    new Stationery { Name = "Блокнот", Type = "Бумажная продукция", Num = 75, Price = 35.5m }};
                db.Stationeries.AddRange(stationeries);
                var managers = new List<Manager> {
                    new Manager { Name = "Иван Петров" },
                    new Manager { Name = "Анна Смирнова" },
                    new Manager { Name = "Алексей Кузнецов" },
                    new Manager { Name = "Ольга Орлова" }};
                db.Managers.AddRange(managers);

                var customers = new List<Customer>
                    {new Customer { Name = "ООО Альфа" },
                    new Customer { Name = "ООО Бета" },
                    new Customer { Name = "ООО Гамма" },
                    new Customer { Name = "ООО Дельта" } };
                db.Customers.AddRange(customers);
                db.SaveChanges();

                var sales = new List<Sale>
                {
                    new Sale { CustomerId = customers[0].Id, ManagerId = managers[0].Id, StationeryId = stationeries[0].Id, Sold = 20, SalePrice = 18m, SaleDate = DateTime.Now },
                    new Sale { CustomerId = customers[1].Id, ManagerId = managers[1].Id, StationeryId = stationeries[1].Id, Sold = 15, SalePrice = 10m, SaleDate = DateTime.Now },
                    new Sale { CustomerId = customers[2].Id, ManagerId = managers[2].Id, StationeryId = stationeries[2].Id, Sold = 30, SalePrice = 19m, SaleDate = DateTime.Now },
                    new Sale { CustomerId = customers[3].Id, ManagerId = managers[3].Id, StationeryId = stationeries[3].Id, Sold = 25, SalePrice = 12m, SaleDate = DateTime.Now }
                };
                db.Sales.AddRange(sales);
                db.SaveChanges();

                Console.WriteLine("Продажи -> ");
                var salesList = db.Sales.Include(s => s.Customer).Include(s => s.Manager).Include(s => s.Stationery).ToList();
                foreach (var s in salesList)
                {
                    Console.WriteLine($"Дата: {s.SaleDate}, Товар: {s.Stationery.Name}, Менеджер: {s.Manager.Name}, " +
                             $"Покупатель: {s.Customer.Name}, Количество: {s.Sold}, Цена: {s.SalePrice} грн.");
                }

                // 3 задание
                //Console.WriteLine("Все типы");
                //foreach (var s in stationeries.ToList())
                //{
                //    Console.WriteLine($"Тип: {s.Type}");
                //}

                //Console.WriteLine("Менеджер по продажам: ");
                //foreach(var a in managers.ToList())
                //{
                //    Console.WriteLine($"- {a.Name}");
                //}

                //Console.WriteLine("\nКанцтовары с мак.продажами: ");
                //var MNum = stationeries.Max(s => s.Num);
                //var MNumItems = stationeries.Where(s => s.Num == MNum).ToList();
                //foreach(var a in MNumItems)
                //{
                //    Console.WriteLine($"- {a.Name} (Количество: {a.Num})");
                //}

                //Console.WriteLine("\nКанцтовары с min.продажами: ");
                //var MinNum = stationeries.Min(s => s.Num);
                //var MinNumItems = stationeries.Where(s => s.Num == MinNum).ToList();
                //foreach (var a in MinNumItems)
                //{
                //    Console.WriteLine($"- {a.Name} (Количество: {a.Num})");
                //}

                //Console.WriteLine("\nКанцтовары с мак.ценой: ");
                //var MPrice = stationeries.Max(s => s.Price);
                //var MPriceItems = stationeries.Where(s => s.Price == MPrice).ToList();
                //foreach (var a in MPriceItems)
                //{
                //    Console.WriteLine($"- {a.Name} (Цена: {a.Price})");
                //}

                //Console.WriteLine("\nКанцтовары с min.ценой: ");
                //var MinPrice = stationeries.Min(s => s.Price);
                //var MinPriceItems = stationeries.Where(s => s.Price == MinPrice).ToList();
                //foreach (var a in MinPriceItems)
                //{
                //    Console.WriteLine($"- {a.Name} (Цена: {a.Price})");
                //}


                string typeS1 = "Письменные принадлежности";
                Console.WriteLine("$\"\\nКанцтовары типа \\\"{typeFilter}\\\":");
                var filterType = stationeries.Where(s => s.Type == typeS1).ToList();
                foreach(var filter in filterType)
                {
                    Console.WriteLine($"- {filter.Name}");
                }
                
                string manager1 = "Анна Смирнова";
                Console.WriteLine($"\nКанцтовары, проданные менеджером {manager1}:");
                var managerSales = db.Sales.Include(s => s.Stationery)
                                            .Include(s => s.Manager)
                                            .Where(s => s.Manager.Name == manager1).ToList();
                foreach(var s in managerSales)
                {
                    Console.WriteLine($"- {s.Stationery.Name} (Количество: {s.Sold})");
                }

                string customerName = "ООО Гамма";
                Console.WriteLine($"\nКанцтовары, купленные {customerName}:");
                var customerSales = db.Sales.Include(s => s.Stationery)
                    .Include(s => s.Customer)
                    .Where(s => s.Customer.Name == customerName).ToList();
                foreach (var s in customerSales)
                    Console.WriteLine($"- {s.Stationery.Name} (Количество: {s.Sold})");


                Console.WriteLine("\nСамая недавняя продажа:");
                var latestSale = db.Sales.Include(s => s.Customer).Include(s => s.Manager).Include(s => s.Stationery)
                    .OrderByDescending(s => s.SaleDate).FirstOrDefault();
                if (latestSale != null)
                {
                    Console.WriteLine($"Дата: {latestSale.SaleDate}, Товар: {latestSale.Stationery.Name}, Менеджер: {latestSale.Manager.Name}, " +
                                      $"Покупатель: {latestSale.Customer.Name}, Количество: {latestSale.Sold}, Цена: {latestSale.SalePrice} грн.");
                }

                Console.WriteLine("\nСреднее количество товаров по каждому типу:");
                var avgQuantities = db.Stationeries
                    .GroupBy(s => s.Type)
                    .Select(g => new { Type = g.Key, AvgQuantity = g.Average(s => s.Num) })
                    .ToList();
                foreach (var item in avgQuantities)
                    Console.WriteLine($"- {item.Type}: {item.AvgQuantity:F2}");
            }
        }
    }
}
