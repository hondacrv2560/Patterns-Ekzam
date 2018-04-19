using Ekzamenation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;


namespace Ekzamenation
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Client> clientList = new List<Client>();
            List<Order> orderList = new List<Order>();
            List<OrderQueue> ordersQueueList = new List<OrderQueue>();
            List<Worker> workerList = new List<Worker>();
            BinaryFormatter binClient = new BinaryFormatter();
            BinaryFormatter binOrder = new BinaryFormatter();
            BinaryFormatter binQueue = new BinaryFormatter();

            int count;
            // Раздел клиент
            int IdClient, IdDiscCard, PhoneClient;
            string NameClient, MiddleNameClient, SurnameClient, CarManufacturer, RegistrationNumber, CarModel;

            // Раздел Заказ
            int IdOrder, day, mounth, hour, min;
            string CarManufacture_r, RegistrationNumber_, CarModel_;
            // Раздел Очередь заказов

            // Раздел персонал
            string NameWorker, MiddleNameWorker, SurnameWorker,PhoneWorker;
            int IdWorker;

            //Load from file
            while (true)
            {

                Console.WriteLine("1. Add Client");
                Console.WriteLine("2. Show all Client");
                Console.WriteLine("3. Add Order");//клиента выбирать из списка, который есть в clientList
                Console.WriteLine("4. Show all Order");
                Console.WriteLine("5. Add Order to Order Queue");
                Console.WriteLine("6. Show Order Queue");
                Console.WriteLine("7. Add worker");
                Console.WriteLine("8. Serialize");
                Console.WriteLine("9. Deserialize");
                Console.WriteLine("r1. Remove Client");//удаление
                Console.WriteLine("r2. Remove Order");//удаление


                switch (Console.ReadLine())
                {
                    
                    case "1":
                        //Console.Write("Статус Клиента: ");                       
                        //Console.WriteLine("Класс автомобиля: ");
                       
                        Console.Write("Введите ID клиента: ");
                        IdClient = int.Parse(Console.ReadLine());
                        Console.Write("Введите ID дисконтной карты: ");
                        IdDiscCard = int.Parse(Console.ReadLine());
                        Console.Write("Введите Имя Клиента: ");
                        NameClient = Console.ReadLine();
                        Console.Write("Введите Отчество Клиента: ");
                        MiddleNameClient = Console.ReadLine();
                        Console.Write("Введите Фамилию Клиента: ");
                        SurnameClient = Console.ReadLine();
                        Console.Write("Введите контактный телефон: ");
                        PhoneClient = int.Parse(Console.ReadLine());
                        Console.Write("Введите роизводителя Т/с: ");
                        CarManufacturer = Console.ReadLine();
                        Console.Write("Введите государственный регистрационный знак:");
                        RegistrationNumber = Console.ReadLine();
                        Console.Write("Введите модель: ");
                        CarModel = Console.ReadLine();
                        

                        Client client = new Client(IdClient, IdDiscCard, NameClient, MiddleNameClient, SurnameClient, PhoneClient, CarManufacturer,
                            RegistrationNumber, CarModel, TypeCar.Crossover);
                        clientList.Add(client);
                        break;
                    case "r1":
                        Console.WriteLine("УДАЛЕНИЕ КЛИЕНТОВ: ");
                        Console.Write("Введите фамилию Клиента для поиска: ");
                        string surNameFind = Console.ReadLine();
                        var Client_f = clientList.Where((x) => x.SurnameClient == surNameFind).FirstOrDefault();
                        if (Client_f!= null)
                        {
                            clientList.Remove(Client_f);
                        }
                        break;
                    case "2":
                        Console.WriteLine("СПИСОК ВСЕХ КЛИЕНТОВ !!!");
                        foreach (var item in clientList)
                        {
                            item.ShowClient();
                        }
                        break;
                    case "3":
                        Console.Write("Введите ID заказа: ");
                        IdOrder = int.Parse(Console.ReadLine());
                        Console.Write("Введите день: ");
                        day = int.Parse(Console.ReadLine());
                        Console.Write("Введите иесяц: ");
                        mounth = int.Parse(Console.ReadLine());
                        Console.Write("Введите время(часы): ");
                        hour = int.Parse(Console.ReadLine());
                        Console.Write("Введите время(минуты): ");
                        min = int.Parse(Console.ReadLine());
                        Console.Write("Введите производителя Т/с: ");
                        CarManufacture_r = Console.ReadLine();
                        Console.Write("Введите регистрационный номер Т/с: ");
                        RegistrationNumber_ = Console.ReadLine();
                        Console.Write("Введите модель Т/с: ");
                        CarModel_ = Console.ReadLine();
                        Order order = new Order(IdOrder, day, mounth, hour, min, CarManufacture_r, RegistrationNumber_, CarModel_, "ewr",2);
                        orderList.Add(order);
                        Console.WriteLine("Select client for order: ");
                        for (int i = 0; i < clientList.Count; i++)
                        {
                            Console.Write(i+1);
                             clientList[i].ShowClient();

                        }
                        var cl= int.Parse(Console.ReadLine())-1;
                        order.client = clientList[cl];
                        break;
                    case "r2":
                        Console.WriteLine("УДАЛЕНИЕ НАРЯД-ЗАКАЗОВ !!! ");
                        Console.Write("Введите номер наряд-заказа: ");
                        var Order_f = orderList.Where((x) => x.IdOrder == 1).FirstOrDefault();
                        if (Order_f != null)
                        {
                            orderList.Remove(Order_f);
                        }
                        break;
                    case "4":
                        foreach (var item in orderList)
                        {
                            item.ShowOrder();
                        }
                        break;
                    case "5":
                        OrderQueue orders = new OrderQueue();
                        ordersQueueList.Add(orders);
                        Console.WriteLine("Select an order to add to the queue: ");
                        for (int i=0; i<orderList.Count;i++)
                        {
                            Console.WriteLine(i+1);
                            orderList[i].ShowOrder();
                        }
                        Console.WriteLine("Select workers: ");
                        for (int i=0;i<workerList.Count;i++)
                        {
                            Console.WriteLine(i + 1);
                            workerList[i].ShowWorker();
                        }
                        var oL = int.Parse(Console.ReadLine()) - 1;
                        orders.order = orderList[oL];
                        var wL = int.Parse(Console.ReadLine()) - 1;
                        orders.worker = workerList[wL];
                        break;
                        case "6":
                        foreach (var item in ordersQueueList)
                        {
                            item.ShowOrderQueue();
                        }
                        break;
                    case "7":
                        Console.Write("Введите ID сотрудника: ");
                        IdWorker = int.Parse(Console.ReadLine());
                        Console.Write("Введите Имя Сотрудника: ");
                        NameWorker = Console.ReadLine();
                        Console.Write(" Введите отчество Сотрудника: ");
                        MiddleNameWorker = Console.ReadLine();
                        Console.Write("Введите фамилию Сотрудника: ");
                        SurnameWorker = Console.ReadLine();
                        Console.Write("Введите контакутный номер телефона сотрудника: ");
                        PhoneWorker = Console.ReadLine(); ;
                        Worker workers = new Worker(PhoneWorker, NameWorker, MiddleNameWorker, SurnameWorker, IdWorker);
                        workerList.Add(workers);
                        break;
                    case "8":
                        //Load to file
                        using (FileStream fs_client = new FileStream("client.dat", FileMode.OpenOrCreate))
                        {
                            binClient.Serialize(fs_client, clientList);
                            Console.WriteLine("Client сериализован");
                        }
                        using (FileStream fs_order = new FileStream("order.dat", FileMode.OpenOrCreate))
                        {
                            binOrder.Serialize(fs_order, orderList);
                            Console.WriteLine("Order сериализован");
                        }
                        using (FileStream fs_orderQueue = new FileStream("queue.dat", FileMode.OpenOrCreate))
                        {
                            binQueue.Serialize(fs_orderQueue, ordersQueueList);
                            Console.WriteLine("OrderQueue сериализован");
                        }
                            break;
                    case "9":
                        //Load from file
                        using (FileStream fs_client = new FileStream("client.dat", FileMode.OpenOrCreate))
                        {
                            clientList = (List<Client>)binClient.Deserialize(fs_client);
                            Console.WriteLine("Client десериализован");
                        }
                        using (FileStream fs_order = new FileStream("order.dat", FileMode.OpenOrCreate))
                        {
                            orderList = (List<Order>)binOrder.Deserialize(fs_order);
                            Console.WriteLine("Order десериализован");
                        }
                        using (FileStream fs_orderQueue = new FileStream("queue.dat", FileMode.OpenOrCreate))
                        {
                            ordersQueueList = (List<OrderQueue>)binQueue.Deserialize(fs_orderQueue);
                            Console.WriteLine("Order десериализован");
                        }
                        break;
                    case "0":
                        return;
                }
            }
        }
    }
        public enum ClientSatys { NAN = 0, VIP = 1, regularCustomer = 2 }
        public enum TypeCar {NAN = 0, Sedan = 1, Wagon = 2, SUV = 3, Crossover = 4, Minivan = 5, Minibus = 6, Motorcycle = 7, Limousine = 8 }

    [Serializable()]
    public class Client//Информация о кленте
    {

        public int IdClient { get; set; }
        public int IdDiscCard { get; set; }
        public string NameClient { get; set; }
        public string MiddleNameClient { get; set; }
        public string SurnameClient { get; set; }
        public int PhoneClient { get; set; }
        public string CarManufacturer { get; set; }
        public string RegistrationNumber { get; set; }
        public string CarModel { get; set; }
        public TypeCar TypeCar { get; set; }
        public ClientSatys ClientSatys { get; set; }

        public Client()
        {
            IdClient = 0;
            IdDiscCard = 0;
            NameClient = "Null";
            MiddleNameClient = "Null";
            SurnameClient = "Null";
            PhoneClient = 0;
            CarManufacturer = "Null";
            RegistrationNumber = "Null";
            CarModel = "Null";
            TypeCar = TypeCar.NAN;
            ClientSatys = ClientSatys.NAN;
        }
        public Client(int IdClient, int IdDiscCard, string NameClient, string MiddleNameClient, string SurnameClient,
            int PhoneClient, string CarManufacturer, string RegistrationNumber, string CarModel, TypeCar TypeCar)
        {
            this.IdClient = IdClient;
            this.IdDiscCard = IdDiscCard;
            this.NameClient = NameClient;
            this.MiddleNameClient = MiddleNameClient;
            this.SurnameClient = SurnameClient;
            this.PhoneClient = PhoneClient;
            this.CarManufacturer = CarManufacturer;
            this.RegistrationNumber = RegistrationNumber;
            this.CarModel = CarModel;
            this.TypeCar = TypeCar;

        }

        public void ShowClient()
        {
            Console.Write("ID клиента: ");
            Console.WriteLine(IdClient);
            Console.Write("ID дисконтной карты: ");
            Console.WriteLine(IdDiscCard);
            //Console.Write("Статус Клиента: ");
            //Console.WriteLine(ClientStatys);
            Console.Write("Имя Клиента: ");
            Console.WriteLine(NameClient);
            Console.Write("Отчество Клиента: ");
            Console.WriteLine(MiddleNameClient);
            Console.Write("Фамилия Клиента: ");
            Console.WriteLine(SurnameClient);
            Console.Write("Контактный телефон: ");
            Console.WriteLine(PhoneClient);
            Console.Write("Производитель Т/с: ");
            Console.WriteLine(CarManufacturer);
            Console.Write("Модель: ");
            Console.WriteLine(CarModel);
            Console.WriteLine("Класс автомобиля: ");
            Console.WriteLine(TypeCar);
            Console.Write("Государственный регистрационный знак: ");
            Console.WriteLine(RegistrationNumber);
        }
    }

    [Serializable()]
    public class Order// Заказ
    {
        public Client client { get; set; }
        public int IdOrder { get; set; }
        public int day { get; set; }
        public int mounth { get; set; }
        public int hour { get; set; }
        public int min { get; set; }
        public string CarManufacturer { get; set; }
        public string RegistrationNumber { get; set; }
        public string CarModel { get; set; }
        //public typeCar TypeCar { get; set; }
        public string TypeOrder { get; set; }

        public Order()
        {
            client = new Client();
            IdOrder = 0;
            day = 1;
            mounth = 1;
            hour = 0;
            min = 0;
            CarManufacturer = "Null";
            RegistrationNumber = "Null";
            CarModel = "Null";
            TypeOrder = "Null";
        }
        public Order(int IdOrder, int day, int mounth, int hour, int min, string CarManufacturer,
            string RegistrationNumber, string CarModel, string TypeOrder, int TypeCar)
        {
            this.IdOrder = IdOrder;
            this.day = day;
            this.mounth = mounth;
            this.hour = hour;
            this.min = min;
            this.CarManufacturer = CarManufacturer;
            this.RegistrationNumber = RegistrationNumber;
            this.CarModel = CarModel;
           
            this.TypeOrder = TypeOrder;
        }

        public void ShowOrder()
        {
            Console.Write("ID клиента: ");
            Console.WriteLine(client.IdClient);
            Console.Write("ID дисконтной карты: ");
            Console.WriteLine(client.IdDiscCard);
            //Console.Write("Статус Клиента: ");
            //Console.WriteLine(client.ClientStatys);
            Console.Write("Имя Клиента: ");
            Console.WriteLine(client.NameClient);
            Console.Write("Отчество Клиента: ");
            Console.WriteLine(client.MiddleNameClient);
            Console.Write("Фамилия Клиента: ");
            Console.WriteLine(client.SurnameClient);
            Console.Write("Контактный телефон: ");
            Console.WriteLine(client.PhoneClient);
            //Console.Write("Производитель Т/с: ");
            //Console.WriteLine(client.CarManufacturer);
            //Console.Write("Модель: ");
            //Console.WriteLine(CarModel);
            //Console.Write("Государственный регистрационный знак: ");
            //Console.WriteLine(client.RegistrationNumber);
            Console.Write("ID заказа: ");
            Console.WriteLine(IdOrder);
            Console.Write("Заказ на(день) : ");
            Console.WriteLine(day);
            Console.Write("Заказ на(месяц) : ");
            Console.WriteLine(mounth);
            Console.Write("Время(часы) : ");
            Console.WriteLine(hour);
            Console.Write("Время(минуты) : ");
            Console.WriteLine(min);
            Console.Write("Производитель Т/с: ");
            Console.WriteLine(CarManufacturer);
            Console.Write("Модель: ");
            Console.WriteLine(CarModel);
            Console.Write("Государственный регистрационный знак: ");
            Console.WriteLine(RegistrationNumber);
            //Console.Write("Класс автомобиля: ");
            //Console.WriteLine(TypeCar);
            Console.Write("ХЗ пока не придумал: ");
            Console.WriteLine(TypeOrder);
        }
    }

    [Serializable]
    public class OrderQueue// Планировщик заказов
    {
       public Order order;
       public Worker worker;
        public int day { get; set; }
        public int mounth { get; set; }
        public int hour { get; set; }
        public int min { get; set; }
        public string CarManufacturer { get; set; }
        public string RegistrationNumber { get; set; }
        public string CarModel { get; set; }
        public string TypeCar { get; set; }

        public OrderQueue()
        {
            order = new Order();
            worker = new Worker();
            day = 1;
            mounth = 1;
            hour = 0;
            min = 0;
            CarManufacturer = "Null";
            RegistrationNumber = "Null";
            CarModel = "Null";
            TypeCar = "Null";
        }
        public OrderQueue(int day, int mounth, int hour, int min, string CarManufacturer, string RegistrationNumber,
            string CarModel, string TypeCar)
        {
            this.day = day;
            this.mounth = mounth;
            this.hour = hour;
            this.min = min;
            this.CarManufacturer = CarManufacturer;
            this.RegistrationNumber = RegistrationNumber;
            this.CarModel = CarModel;
            this.TypeCar = TypeCar;
        }
        public void ShowOrderQueue()
        {
            Console.Write("ID заказа: ");
            Console.WriteLine(order.IdOrder);
            Console.Write("Заказ на(день) : ");
            Console.WriteLine(order.day);
            Console.Write("Заказ на(месяц) : ");
            Console.WriteLine(order.mounth);
            Console.Write("Время(часы) : ");
            Console.WriteLine(order.hour);
            Console.Write("Время(минуты) : ");
            Console.WriteLine(order.min);
            Console.Write("Производитель Т/с: ");
            Console.WriteLine(order.CarManufacturer);
            Console.Write("Модель: ");
            Console.WriteLine(order.CarModel);
            Console.Write("Государственный регистрационный знак: ");
            Console.WriteLine(order.RegistrationNumber);
            //Console.Write("Класс автомобиля: ");
            //Console.WriteLine(order.TypeCar);
            Console.WriteLine("Исполнитель работ по заказу: ");
            Console.Write("ID Сотрудника: ");
            Console.WriteLine(worker.IdWorker);
            Console.Write("Имя Сотрудника: ");
            Console.WriteLine(worker.NameWorker);
            Console.Write("Отчество Сотрудника: ");
            Console.WriteLine(worker.MiddleNameWorker);
            Console.Write("Фамилия Сотрудника: ");
            Console.WriteLine(worker.SurnameWorker);
            Console.Write("Контактный номер телефона Сотрудника: ");
            Console.WriteLine(worker.IdWorker);
        }
    }

    [Serializable]
    public class Worker// Информация  о персонале
    {
        public string NameWorker { get; set; }
        public string MiddleNameWorker { get; set; }
        public string SurnameWorker { get; set; }
        public string PhoneWorker { get; set; }
        public int IdWorker { get; set; }
        public Worker()
        {
            NameWorker = "Null";
            MiddleNameWorker = "Null";
            SurnameWorker = "Null";
            PhoneWorker = "Null";
            IdWorker = 0;
        }
        public Worker(string NameWorker, string MiddleNameWorker, string SurnameWorker, string PhoneWorker, int IdWorker)
        {
            this.NameWorker = NameWorker;
            this.MiddleNameWorker = MiddleNameWorker;
            this.SurnameWorker = SurnameWorker;
            this.PhoneWorker = PhoneWorker;
            this.IdWorker = IdWorker;
        }

        public void ShowWorker()
        {
            Console.Write("Имя Сотрудника: ");
            Console.WriteLine(NameWorker);
            Console.Write("Отчество Сотрудника: ");
            Console.WriteLine(MiddleNameWorker);
            Console.Write("Фамилия Сотрудника: ");
            Console.WriteLine(SurnameWorker);
            Console.Write("Контактный телефон сотрудника: ");
            Console.WriteLine(PhoneWorker);
            Console.Write("ID сотрудника: ");
            Console.WriteLine(IdWorker);
        }
    }

    public class Admission//+
    {
        int day;
        int mounth;
        string incomeItem;
        double price;
        double sum;
        public Admission()
        {
            day = 1;
            mounth = 1;
            incomeItem = "Null";
            price = 0;
            sum = 0;
        }
    }

    public class Consumption//-
    {
        int day;
        int mounth;
        string Articleflow;
        double price;
        double sum;
        public Consumption()
        {
            day = 1;
            mounth = 1;
            Articleflow = "Null";
            price = 0;
            sum = 0;
        }
    }

    public class Material
    {
        int day;
        int mounth;
        string materialName;
        int quantityMaterial;
    }

    public class AdmissionMater:Material//Материалы +
    {

    }
    public class ConsumptionMater: Material//Материалы -
    {

    }

}

