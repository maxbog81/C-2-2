using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    class WorkerHourlyRate : Worker
    {
        public WorkerHourlyRate(int id, string name, double rate) : base(id, name, rate) { }

        public override double Salary
        {
            get { return 20.8 * 8 * Rate; }
        }

        //Методы
        public override double CalcMonthWage()
        {
            return 20.8 * 8 * Rate;
        }
        public override string ToString()
        {
            return $"Работник с почасовой ставкой {base.ToString()}";
        }
    }
}
