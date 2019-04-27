using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Богатов Максим

//1.	Построить три  класса(базовый и  2  потомка),  описывающих работников  с почасовой  оплатой(один из  потомков)  и фиксированной оплатой(второй потомок) :
//a.Описать в базовом классе абстрактный метод для расчета среднемесячной заработной платы.Для «повременщиков» формула для расчета такова: «среднемесячная заработная плата = 20.8 * 8 * почасовая ставка»; для работников  с фиксированной  оплатой: «среднемесячная заработная плата = фиксированная месячная оплата»;
//b.Создать на базе абстрактного класса массив сотрудников и заполнить его;
//c.  * Реализовать интерфейсы для возможности сортировки массива, используя Array.Sort();
//d.  * Создать класс, содержащий массив сотрудников, и реализовать возможность вывода данных с использованием foreach.


namespace Workers
{
    class Program
    {
        class NameComparer : IComparer<Worker>
        {
            public int Compare(Worker x, Worker y)
            {
                return x.Name.CompareTo(y.Name);
            }
        }

        class SalaryComparer : IComparer<Worker>
        {
            public int Compare(Worker x, Worker y)
            {
                return x.Salary.CompareTo(y.Salary);
            }
        }

        static void Main(string[] args)
        {
            //через массив и перегрузку метода String
            Worker[] workers = new Worker[6];
            workers[0] = new WorkerHourlyRate(1, "Андрей", 400);
            workers[1] = new WorkerHourlyRate(2, "Николай", 300);
            workers[2] = new WorkerHourlyRate(3, "Максим", 500);
            workers[3] = new WorkerFixedRate(4, "Федор", 50000);
            workers[4] = new WorkerFixedRate(5, "Ирина", 30000);
            workers[5] = new WorkerFixedRate(6, "Павел", 45000);

            Array.Sort(workers, new NameComparer());
            Console.WriteLine("Сортировка по имени:");
            foreach (Worker worker in workers)
            {
                Console.WriteLine(worker.ToString());

            }

            Array.Sort(workers, new SalaryComparer());
            Console.WriteLine("\nСортировка по зарплате:");
            foreach (Worker worker in workers)
            {
                Console.WriteLine(worker.ToString());

            }

            //через лист и интерфейсы
            Random r = new Random();
            List<Worker> ListWorkers = new List<Worker>();

            for (int i = 0; i < 5; i++)
            {
                ListWorkers.Add(new WorkerHourlyRate(i, $"Имя_{i}", r.Next(300, 500)));                
            }

            for (int i = 5; i < 10; i++)
            {
                ListWorkers.Add(new WorkerFixedRate(i, $"Имя_{i}", r.Next(30000, 100000)));
            }

            ListWorkers.Sort();

            Department dept = new Department(ListWorkers);
            Console.WriteLine("\nСортировка по зарплате:");
            foreach (var item in dept)
                Console.WriteLine(item);

            Console.ReadKey();
        }
    }
}
