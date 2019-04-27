using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    abstract class Worker:IComparable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rate { get; set; }

        public abstract double CalcMonthWage();

        public abstract double Salary { get; }

        public Worker(int id, string name, double rate)
        {
            Id = id;
            Name = name;
            Rate = rate;
        }

        /// <summary>
        /// Сортировка по оплате
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            var over = obj as Worker;
            return this.Salary < over.Salary ? -1 : 1;
        }

        public override string ToString()
        {
            return string.Format("ID={0,4}, Имя={1,8}, з/п={2,9:f2}", Id, Name, CalcMonthWage());
        }

    }
}
