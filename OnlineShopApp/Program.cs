using System;
using System.Collections.Generic;

namespace OnlineShop
{

    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }

    
    class Order
    {
        public int OrderId { get; set; }
        public List<Product> Products = new List<Product>();
        public string Status { get; set; } = "В обработке";
    }

    
    class User
    {
        public string Username { get; set; }
        public string Role { get; set; } 
    }

    
    class Program
    {
        static List<Product> products = new List<Product>();
        static List<Order> orders = new List<Order>();
        static List<User> users = new List<User>();
        static List<Product> cart = new List<Product>();

        static void Main()
        {
            
            users.Add(new User { Username = "buyer1", Role = "Buyer" });
            users.Add(new User { Username = "seller1", Role = "Seller" });
            users.Add(new User { Username = "admin1", Role = "Admin" });

            Console.WriteLine("Введите ваш логин: ");
            string login = Console.ReadLine();

            User current = users.Find(u => u.Username == login);

            if (current == null)
            {
                Console.WriteLine("Пользователь не найден!");
                return;
            }

            Console.WriteLine($"Добро пожаловать, {current.Username}! Ваша роль: {current.Role}");

            
            switch (current.Role)
            {
                case "Buyer": BuyerMenu(); break;
                case "Seller": SellerMenu(); break;
                case "Admin": AdminMenu(); break;
            }
        }

        
        static void BuyerMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Меню покупателя ---");
                Console.WriteLine("1. Просмотреть товары");
                Console.WriteLine("2. Добавить в корзину");
                Console.WriteLine("3. Оформить заказ");
                Console.WriteLine("4. Отследить заказ");
                Console.WriteLine("0. Выход");

                int c = int.Parse(Console.ReadLine());
                if (c == 0) break;

                switch (c)
                {
                    case 1: ShowProducts(); break;
                    case 2: AddToCart(); break;
                    case 3: CreateOrder(); break;
                    case 4: TrackOrder(); break;
                }
            }
        }

        
        static void SellerMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Меню продавца ---");
                Console.WriteLine("1. Добавить товар");
                Console.WriteLine("2. Обновить товар");
                Console.WriteLine("3. Посмотреть заказы");
                Console.WriteLine("0. Выход");

                int c = int.Parse(Console.ReadLine());
                if (c == 0) break;

                switch (c)
                {
                    case 1: AddProduct(); break;
                    case 2: UpdateProduct(); break;
                    case 3: ShowOrders(); break;
                }
            }
        }

        
        static void AdminMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Меню администратора ---");
                Console.WriteLine("1. Управлять пользователями");
                Console.WriteLine("2. Удалить товар");
                Console.WriteLine("3. Управление заказами");
                Console.WriteLine("0. Выход");

                int c = int.Parse(Console.ReadLine());
                if (c == 0) break;

                switch (c)
                {
                    case 1: ManageUsers(); break;
                    case 2: DeleteProduct(); break;
                    case 3: ManageOrders(); break;
                }
            }
        }

        
        static void ShowProducts()
        {
            Console.WriteLine("\n--- Список товаров ---");
            foreach (var p in products)
                Console.WriteLine($"{p.Id}. {p.Name} - {p.Price}тг");
        }

        static void AddToCart()
        {
            ShowProducts();
            Console.WriteLine("Введите ID товара:");
            int id = int.Parse(Console.ReadLine());

            Product p = products.Find(x => x.Id == id);
            if (p != null)
            {
                cart.Add(p);
                Console.WriteLine("Товар добавлен в корзину!");
            }
        }

        static void CreateOrder()
        {
            Order order = new Order();
            order.OrderId = orders.Count + 1;
            order.Products.AddRange(cart);

            orders.Add(order);
            cart.Clear();

            Console.WriteLine($"Заказ #{order.OrderId} оформлен!");
        }

        static void TrackOrder()
        {
            Console.WriteLine("Введите номер заказа:");
            int id = int.Parse(Console.ReadLine());

            Order order = orders.Find(o => o.OrderId == id);
            if (order != null)
                Console.WriteLine($"Статус заказа: {order.Status}");
        }

    
        static void AddProduct()
        {
            Product p = new Product();
            p.Id = products.Count + 1;

            Console.Write("Название: ");
            p.Name = Console.ReadLine();

            Console.Write("Цена: ");
            p.Price = double.Parse(Console.ReadLine());

            products.Add(p);
            Console.WriteLine("Товар добавлен!");
        }

        static void UpdateProduct()
        {
            ShowProducts();
            Console.Write("ID товара: ");
            int id = int.Parse(Console.ReadLine());

            Product p = products.Find(x => x.Id == id);
            if (p == null) return;

            Console.Write("Новое название: ");
            p.Name = Console.ReadLine();

            Console.Write("Новая цена: ");
            p.Price = double.Parse(Console.ReadLine());

            Console.WriteLine("Товар обновлён!");
        }

        static void ShowOrders()
        {
            Console.WriteLine("\n--- Заказы покупателей ---");
            foreach (var o in orders)
                Console.WriteLine($"Заказ #{o.OrderId}, статус: {o.Status}");
        }

        
        static void ManageUsers()
        {
            Console.WriteLine("\n--- Управление пользователями ---");
            foreach (var u in users)
                Console.WriteLine($"{u.Username} ({u.Role})");
        }

        static void DeleteProduct()
        {
            ShowProducts();
            Console.Write("Введите ID товара для удаления: ");
            int id = int.Parse(Console.ReadLine());

            products.RemoveAll(x => x.Id == id);
            Console.WriteLine("Товар удалён!");
        }

        static void ManageOrders()
        {
            Console.WriteLine("\n--- Управление заказами ---");
            foreach (var o in orders)
                Console.WriteLine($"Заказ #{o.OrderId}, статус: {o.Status}");

            Console.Write("Введите номер заказа для изменения статуса: ");
            int id = int.Parse(Console.ReadLine());

            Order order = orders.Find(o => o.OrderId == id);

            Console.Write("Новый статус: ");
            order.Status = Console.ReadLine();

            Console.WriteLine("Статус обновлён!");
        }
    }
}
